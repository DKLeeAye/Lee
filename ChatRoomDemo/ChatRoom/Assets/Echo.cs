using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;//Socket编程的API位于此命名空间中
using UnityEngine.UI;

public class Echo : MonoBehaviour
{
	Socket sokect;
	public InputField inputField;
	public Text text;

	public void Connection()
    {
		//Socket 网络连接的端点
		sokect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket对象，三个参数代表地址族、套接字类型、协议
		sokect.Connect("127.0.0.1", 8888);//连接服务端  Connect是一个阻塞方法，程序会卡住直到服务端回应（接收，拒绝，超时）
    }

	public void Send()
    {
		//发送
		string sendStr = inputField.text;
		byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);//字符串转换为byte[]数组
		sokect.Send(sendBytes);//发送数据  阻塞办法  返回值指明发送数据的长度

		//接受
		byte[] readBuff = new byte[1024];
		int count = sokect.Receive(readBuff);//接收消息 阻塞方法 其中byte[]类型的储存接收到的数据  返回值指明接收数据的长度
		string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);//byte[]转换成字符串

		text.text = recvStr;

		//关闭
		sokect.Close();//关闭连接
    }
}
