using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;//Socket编程的API位于此命名空间中
using UnityEngine.UI;
using System;

public class Echo : MonoBehaviour
{
	Socket socket;
	public InputField inputField;
	public Text text;
	public string userName;

	//接收缓冲区
	byte[] readBuff = new byte[1024];
	string recvStr = "";



	public void Connection()
	{
		//Socket 网络连接的端点
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket对象，三个参数代表地址族、套接字类型、协议

		socket.BeginConnect("127.0.0.1", 8888, ConnectCallback, socket);//连接服务端  Connect是一个阻塞方法，程序会卡住直到服务端回应（接收，拒绝，超时）
	}

	//Connect回调
	public void ConnectCallback(IAsyncResult ar) //异步委托类ar
	{
		try
		{
			Socket socket = (Socket)ar.AsyncState;//ar.AsyncState获取BeginConnect传入的最后一个表示连接状态的参数
			socket.EndConnect(ar);
			Debug.Log("Socket连接成功");
			socket.BeginReceive(readBuff, 0, 1024, 0, ReceiveCallback, socket);
		}
		catch (SocketException ex)//异常发生时执行Catch中的代码，打印ex中附带的异常信息
		{
			Debug.Log("Socket连接失败" + ex.ToString());
		}
	}

	//Receive回调
	public void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
			Socket socket = (Socket)ar.AsyncState;
			int count = socket.EndReceive(ar);
			//recvStr = System.Text.Encoding.Default.GetString(readBuff,0,count);
			//接收历史消息
			string s = System.Text.Encoding.Default.GetString(readBuff, 0, count);
			recvStr = recvStr + "\n" + s ;

			socket.BeginReceive(readBuff, 0, 1024, 0, ReceiveCallback, socket);
        }
		catch(SocketException ex)
        {
			Debug.Log("Socket 接收失败" + ex.ToString());
        }
    }

	public void Send()
	{
		//发送
		string sendStr = userName +": " + inputField.text;
		byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);//字符串转换为byte[]数组
		socket.BeginSend(sendBytes,0,sendBytes.Length,0,SendCallback,socket);
	}

	//Send 回调
	public void SendCallback(IAsyncResult ar)
    {
        try
        {
			Socket socket = (Socket)ar.AsyncState;
			int count = socket.EndSend(ar);
			Debug.Log("Socket发送成功" + count);
        }
        catch (SocketException ex)
        {
			Debug.Log("Socket发送失败" + ex.ToString());
        }
    }

	public void Update()
    {
		text.text = recvStr;		
	}
}
