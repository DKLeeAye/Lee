using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour 
{
	public InputField InputField;
	public InputField myName;
	public Text chatText;
	private Text temText;
	int index = 0;

	void Start () 
	{
		//网络模块
		NetManager.AddListener("Enter", OnEnter);
		NetManager.AddListener("List", OnList);
		NetManager.AddListener("Speak", OnSpeak);
		NetManager.Connect("127.0.0.1", 8888);

		string sendStr = "Enter|";
		sendStr += NetManager.GetDesc()+ ",";		
		NetManager.Send(sendStr);
		//NetManager.Send("List|");
	}

	public void SendBtn()
	{
		string sendStr = "Speak|";
		sendStr += myName.text + ",";
		sendStr += InputField.text + ",";
		NetManager.Send(sendStr);
	}

	public void SynBtn()
	{
		string sendStr = "List|";
		NetManager.Send(sendStr);
	}

	void OnSpeak(string msgArgs)
    {
		Debug.Log("OnSpeak" + msgArgs);
		string[] split = msgArgs.Split(',');
		string userName = split[0];
		string message = split[1];
		temText = (Text)Instantiate(chatText);
		temText.GetComponent<Transform>().SetParent(GameObject.FindWithTag("MessageShow").GetComponent<Transform>(), true);
		temText.GetComponent<Transform>().localPosition = new Vector3(200, -250 + index *80, 0);
		temText.text = userName + ":" + message;
		index++;
		
    }

	void Update()
	{
		NetManager.Update();
	}

	void OnEnter (string msgArgs)
	{
		Debug.Log("OnEnter " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		string desc = split[0];
		//是自己
		if(desc == NetManager.GetDesc())
			return;
		Debug.Log("someone is coming");
	}

	void OnList (string msgArgs) 
	{
		//Debug.Log("lalalalalalalala ");
		Debug.Log("OnList " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		string userName = split[0];
		string message = split[1];
		temText = (Text)Instantiate(chatText);
		temText.GetComponent<Transform>().SetParent(GameObject.FindWithTag("MessageShow").GetComponent<Transform>(), true);
		temText.GetComponent<Transform>().localPosition = new Vector3(200, -250 + index * 80, 0);
		temText.text = userName + ":" + message;
		index++;
	}
}
