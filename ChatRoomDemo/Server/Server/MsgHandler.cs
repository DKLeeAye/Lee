using System;


public class MsgHandler
{
	public static void MsgSpeak(ClientState c, string msgArgs)
    {
		//解析参数
		string[] split = msgArgs.Split(',');
		c.userName = split[0];
		string message = split[1];
		string sendStr = "Speak|" + msgArgs;
		MainClass.message.Add(msgArgs);
		foreach (ClientState cs in MainClass.clients.Values)
        {
			MainClass.Send(cs, sendStr);
        }
    }
	
	public static void MsgEnter(ClientState c, string msgArgs)
	{
		//解析参数
		string[] split = msgArgs.Split(',');
		string desc = split[0];

		//广播
		string sendStr = "Enter|" + msgArgs;
		foreach (ClientState cs in MainClass.clients.Values)
		{
			MainClass.Send(cs, sendStr);
		}
	}

	public static void MsgList(ClientState c, string msgArgs)
	{
		string sendStr = "List|";

		//遍历发送
		for(int i= 0; i<MainClass.message.Count; i++)
        {
			MainClass.Send(c, sendStr + MainClass.message[i]);
			Console.WriteLine(MainClass.message[i]);
		}
	}

	
}


