using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour 
{

	public InputField myName;
	public InputField myMessage;
	public UIScroller Scroller;

	// Use this for initialization
	void Start () 
	{
		//事件监听
		NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
		NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, OnConnectFail);
		NetManager.AddEventListener(NetManager.NetEvent.Close, OnConnectClose);		
		//消息监听
		NetManager.AddMsgListener("MsgChat", OnMsgChat);
		NetManager.AddMsgListener("MsgSyn", OnMsgSyn);
	}


	//收到MsgChat协议
	public void OnMsgChat(MsgBase msgBase)
    {
		MsgChat msg = (MsgChat)msgBase;
		MessageShow(msg);
	}

	public void OnMsgSyn(MsgBase msgBase)
    {
		MsgChat msg = (MsgChat)msgBase;

		MessageShow(msg);
	}

	private void MessageShow(MsgChat msg)
	{
		Scroller.AddItem(msg);
	}
	//发送MsgChat协议
	public void OnChatClick()
    {
		MsgChat msg = new MsgChat();
		msg.userName = myName.text;
		msg.chatMessage = myMessage.text;
		NetManager.Send(msg);
    }

	//发送同步消息协议
	public void OnSynClick()
    {
		MsgSyn msg = new MsgSyn();
		NetManager.Send(msg);

    }

	//玩家点击连接按钮
	public void OnConnectClick()
	{
		NetManager.Connect("127.0.0.1", 8888);
	}

	//主动关闭
	public void OnCloseClick()
	{
		NetManager.Close();
	}

	//连接成功回调
	void OnConnectSucc(string err)
	{
		Debug.Log("OnConnectSucc");
	}

	//连接失败回调
	void OnConnectFail(string err)
	{
		Debug.Log("OnConnectFail " + err);
	}

	//关闭连接
	void OnConnectClose(string err)
	{
		Debug.Log("OnConnectClose");
	}

	// Update is called once per frame
	void Update ()
	{
		NetManager.Update();
	}
}
