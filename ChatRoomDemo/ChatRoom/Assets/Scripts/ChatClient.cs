using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;//Socket编程的API位于此命名空间中
using UnityEngine.UI;
using System;

public class ChatClient : MonoBehaviour
{
	public InputField inputField;
	public Text text;
	public string userName;

	void Start()
	{
		NetManager.AddListener("Enter", OnEnter);
		NetManager.AddListener("SendMsg", OnSendMsg);
	}

	void OnEnter(string msg)
	{
		Debug.Log("OnEnter" + msg);
	}

	void OnSendMsg(string msg)
	{
		Debug.Log("OnSendMsg" + msg);
	}


	public void Send()
    {
		NetManager.Send("Enter|DK,"+ inputField.text);
    }

	public void Connect()
    {
		NetManager.Connect("127.0.0.1", 8888);
    }

	public void Update()
    {
		NetManager.Update();
	}
}
