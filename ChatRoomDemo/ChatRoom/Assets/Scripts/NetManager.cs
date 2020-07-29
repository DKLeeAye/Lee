using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;
using System;

public static class NetManager
{
	//定义套接字
	static Socket socket;
	//接收缓冲区
	static byte[] readBuff = new byte[1024];
	//委托类型
	public delegate void MsgListener(String str);
	//监听列表
	private static Dictionary<string, MsgListener> listeners = new Dictionary<string, MsgListener>();
	//消息列表
	static List<String> msgList = new List<string>();

	//添加监听
	public static void AddListener(string msgName, MsgListener listener)
	{
		listeners[msgName] = listener;
	}

	//获取描述
	public static string GetDesc()
	{
		if (socket == null) return "";
		if (!socket.Connected) return "";
		return socket.LocalEndPoint.ToString();
	}

	//连接
	public static void Connect(string ip, int port)
	{
		//Socket
		socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
		//Connect
		socket.BeginConnect(ip, port, ConnectCallback, socket);
		//BeginReceive
		socket.BeginReceive(readBuff, 0, 1024, 0,ReceiveCallback, socket);
	}

	//Connect回调
	public static void ConnectCallback(IAsyncResult ar) //异步委托类ar
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;//ar.AsyncState获取BeginConnect传入的最后一个表示连接状态的参数
			socket.EndConnect(ar);
			Debug.Log("Socket Connect Success");
			socket.BeginReceive(readBuff, 0, 1024, 0, ReceiveCallback, socket);
		}
		catch (SocketException ex)//异常发生时执行Catch中的代码，打印ex中附带的异常信息
		{
			Debug.Log("Socket Connect fail" + ex.ToString());
		}
	}


	//Receive回调
	private static void ReceiveCallback(IAsyncResult ar)
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;
			int count = socket.EndReceive(ar);
			string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
			msgList.Add(recvStr);
			socket.BeginReceive(readBuff, 0, 1024, 0,ReceiveCallback, socket);
		}
		catch (SocketException ex)
		{
			Debug.Log("Socket Receive fail" + ex.ToString());
		}
	}

	//点击发送按钮
	public static void Send(string sendStr)
	{
		if (socket == null) return;
		if (!socket.Connected) return;

		byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
		socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socket);
	}

	//Send回调
	private static void SendCallback(IAsyncResult ar)
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;
			//int count = socket.EndSend(ar);
		}
		catch (SocketException ex)
		{
			Debug.Log("Socket Send fail" + ex.ToString());
		}
	}

	//Update
	public static void Update()
	{
		if (msgList.Count <= 0)
			return;
		String msgStr = msgList[0];
		msgList.RemoveAt(0);
		string[] split = msgStr.Split('|');
		string msgName = split[0];
		string msg = split[1];
		//监听回调;
		if (listeners.ContainsKey(msgName))
		{
			listeners[msgName](msg);
		}
	}
}
