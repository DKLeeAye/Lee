﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;
using System;
//using NUnit.Framework;
using System.Linq;

public static class NetManager
{
	//定义套接字
	static Socket socket;
	//接收缓冲区
	static ByteArray readBuff;
	//写入队列
	static Queue<ByteArray> writeQueue;
	//是否正在连接
	static bool isConnecting = false;
	//是否正在关闭
	static bool isClosing = false;
	//消息列表
	static List<MsgBase> msgList = new List<MsgBase>();
	//消息列表长度
	static int msgCount = 0;
	//每次刷新处理的消息量
	readonly static int MAX_MESSAGE_FIRE = 10;

	//事件
	public enum NetEvent
	{
		ConnectSucc = 1,
		ConnectFail = 2,
		Close = 3,
	}
	//事件委托类型
	public delegate void EventListener(string err);
	//事件监听列表
	public static Dictionary<NetEvent, EventListener> eventListener = new Dictionary<NetEvent, EventListener>();
	//添加事件监听
	public static void AddEventListener(NetEvent netEvent, EventListener listener)
	{
		if (eventListener.ContainsKey(netEvent))
		{
			eventListener[netEvent] += listener;
		}
		else
		{
			eventListener[netEvent] = listener;
		}
	}
	//删除事件监听
	public static void RemoveEventListener(NetEvent netEvent, EventListener listener)
	{
		if (eventListener.ContainsKey(netEvent))
		{
			eventListener[netEvent] -= listener;
		}
	}
	//分发事件
	private static void DistributeEvent(NetEvent netEvent, string err)
	{
		if (eventListener.ContainsKey(netEvent))
		{
			eventListener[netEvent](err);
		}
	}


	//消息委托类型
	public delegate void MsgListener(MsgBase msgBase);
	//消息监听列表
	private static Dictionary<string, MsgListener> msgListeners = new Dictionary<string, MsgListener>();
	//添加消息监听
	public static void AddMsgListener(string msgName, MsgListener listener)
	{
		//添加
		if (msgListeners.ContainsKey(msgName))
		{
			msgListeners[msgName] += listener;
		}
		//新增
		else
		{
			msgListeners[msgName] = listener;
		}
	}
	//删除消息监听
	public static void RemoveMsgListener(string msgName, MsgListener listener)
	{
		if (msgListeners.ContainsKey(msgName))
		{
			msgListeners[msgName] -= listener;
		}
	}
	//分发消息
	private static void DistributeMsg(string msgName, MsgBase msgBase)
	{
		if (msgListeners.ContainsKey(msgName))
		{
			msgListeners[msgName](msgBase);
		}
	}


	//连接
	public static void Connect(string ip, int port)
	{
		if (socket != null && socket.Connected)
		{
			Debug.Log("Connect fail,already Connected");
			return;
		}
		if (isConnecting)
		{
			Debug.Log("Connect fail,isConnecting");
			return;
		}
		//初始化成员
		InitState();
		//参数设置
		socket.NoDelay = true;
		//连接
		isConnecting = true;
		socket.BeginConnect(ip, port, ConnectCallback, socket);
	}

	//Connect回调
	public static void ConnectCallback(IAsyncResult ar)
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;
			socket.EndConnect(ar);
			Debug.Log("Socket Connect Succ");
			DistributeEvent(NetEvent.ConnectSucc, "");
			isConnecting = false;

			//开始接收
			socket.BeginReceive(readBuff.bytes, readBuff.writeIdx, readBuff.remain, 0, ReceiveCallback, socket);

		}
		catch (SocketException ex)
		{
			Debug.Log("Socket Connect fail" + ex.ToString());
			DistributeEvent(NetEvent.ConnectFail, ex.ToString());
			isConnecting = false;
		}
	}

	private static void InitState()
	{
		//Socket
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		//接收缓冲区
		readBuff = new ByteArray();
		//写入队列
		writeQueue = new Queue<ByteArray>();

		isConnecting = false;

		isClosing = false;
		//消息列表
		msgList = new List<MsgBase>();
		msgCount = 0;
	}

	//关闭连接
	public static void Close()
	{
		if (socket == null || !socket.Connected)
		{
			return;
		}
		if (isConnecting)
		{
			return;
		}
		if (writeQueue.Count > 0)
		{
			isClosing = true;
		}
		else
		{
			socket.Close();
			DistributeEvent(NetEvent.Close, "");
		}
	}


	//发送数据
	public static void Send(MsgBase msg)
	{
		if (socket == null || !socket.Connected)
		{
			return;
		}
		if (isConnecting)
		{
			return;
		}
		if (isClosing)
		{
			return;
		}
		//数据编码
		byte[] nameBytes = MsgBase.EncodeName(msg);//名字
		byte[] bodyBytes = MsgBase.Encode(msg);//消息体
		int len = nameBytes.Length + bodyBytes.Length;
		byte[] sendBytes = new byte[2 + len];
		//组装长度
		sendBytes[0] = (byte)(len % 256);
		sendBytes[1] = (byte)(len / 256);
		//组装名字
		Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
		//组装消息体
		Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);
		//写入队列
		ByteArray ba = new ByteArray(sendBytes);
		int count = 0;
		lock (writeQueue)//lock  避免线程冲突
		{
			writeQueue.Enqueue(ba);
			count = writeQueue.Count;
		}
		//send
		if (count == 1)
		{
			socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socket);
		}
	}

	private static void SendCallback(IAsyncResult ar)
	{
		//获取state.EndSend的处理
		Socket socket = (Socket)ar.AsyncState;
		//状态判断
		if (socket == null || !socket.Connected)
        {
			return;
        }
		//EndSend
		int count = socket.EndSend(ar);
		//获取写入队列第一条数据            
		ByteArray ba;
		lock (writeQueue)
		{
			ba = writeQueue.First();
		}
		//完整发送
		ba.readIdx += count;
		if (ba.length == 0)
		{
			lock (writeQueue)
			{
				writeQueue.Dequeue();
				ba = writeQueue.First();
			}
		}
		//继续发送
		if (ba != null)
		{
			socket.BeginSend(ba.bytes, ba.readIdx, ba.length,
				0, SendCallback, socket);
		}
		//正在关闭
		else if (isClosing)
		{
			socket.Close();
		}
	}

	//Receive回调
	public static void ReceiveCallback(IAsyncResult ar)
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;
			//获取接收数据长度
			int count = socket.EndReceive(ar);
			readBuff.writeIdx += count;
			//处理二进制消息
			OnReceiveData();
			//继续接收数据
			if (readBuff.remain < 8)
			{
				readBuff.MoveBytes();
				readBuff.ReSize(readBuff.length * 2);
			}
			socket.BeginReceive(readBuff.bytes, readBuff.writeIdx,
					readBuff.remain, 0, ReceiveCallback, socket);
		}
		catch (SocketException ex)
		{
			Debug.Log("Socket Receive fail" + ex.ToString());
		}
	}

	//数据处理
	public static void OnReceiveData()
	{
		//消息长度
		if (readBuff.length <= 2)
		{
			return;
		}
		//获取消息体长度
		int readIdx = readBuff.readIdx;
		byte[] bytes = readBuff.bytes;
		Int16 bodyLength = (Int16)((bytes[readIdx + 1] << 8) | bytes[readIdx]);
		if (readBuff.length < bodyLength)
			return;
		readBuff.readIdx += 2;
		//解析协议名
		int nameCount = 0;
		string protoName = MsgBase.DecodeName(readBuff.bytes, readBuff.readIdx, out nameCount);
		if (protoName == "")
		{
			Debug.Log("OnReceiveData MsgBase.DecodeName fail");
			return;
		}
		readBuff.readIdx += nameCount;
		//解析协议体
		int bodyCount = bodyLength - nameCount;
		MsgBase msgBase = MsgBase.Decode(protoName, readBuff.bytes, readBuff.readIdx, bodyCount);
		readBuff.readIdx += bodyCount;
		readBuff.CheckAndMoveBytes();
		//添加到消息队列
		lock (msgList)
		{
			msgList.Add(msgBase);
			msgCount++;
		}
		//继续读取消息
		if (readBuff.length > 2)
		{
			OnReceiveData();
		}
	}


	//Update
	public static void Update()
	{
		MsgUpdate();
	}


	//更新消息
	public static void MsgUpdate()
	{
		//初步判断，提升效率
		if (msgCount == 0)
		{
			return;
		}
		//重复处理消息
		for (int i = 0; i < MAX_MESSAGE_FIRE; i++)
		{
			//获取第一条消息
			MsgBase msgBase = null;
			lock (msgList)
			{
				if (msgList.Count > 0)
				{
					msgBase = msgList[0];
					msgList.RemoveAt(0);
					msgCount--;
				}
			}
			//分发消息
			if (msgBase != null)
			{
				DistributeMsg(msgBase.protoName, msgBase);
			}
			//没有消息了
			else
			{
				break;
			}
		}
	}
}
