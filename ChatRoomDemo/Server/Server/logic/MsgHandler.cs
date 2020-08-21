using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class MsgHandler
{
     public static List<MsgChat> msgData = new List<MsgChat>();
    public static void MsgChat(ClientState c,MsgChat msgBase)
    {
        MsgChat msgChat = (MsgChat)msgBase;
        Console.WriteLine("接收到"+msgChat.userName+"用户消息"+msgChat.chatMessage);
        foreach(ClientState clientState in NetManager.clients.Values)
        {
            NetManager.Send(clientState,msgChat);
        }
        msgData.Add(msgChat);
    }

    public static void MsgSyn(ClientState c, MsgChat msgSyn)
    {
       if(msgData.Count > 50)
        {
            for (int i = 0; i < 50; i++)
            {
                NetManager.Send(c, msgData[(msgData.Count - 1 - i)]);
            }
            Console.WriteLine("消息已同步到客户端");
        }
        else
        {
            for (int i = 0; i < msgData.Count; i++)
            {
                NetManager.Send(c, msgData[i]);
            }
            Console.WriteLine("消息已同步到客户端");
        }       
    }
}

