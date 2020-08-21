using JetBrains.Annotations;
using System.Collections.Generic;

public class MsgChat : MsgBase
{
    public MsgChat()
    {
        protoName = "MsgChat";    
    }

    public string userName = "";
    public string chatMessage = "";
}

public class MsgSyn : MsgBase
{
    public MsgSyn()
    {
        protoName = "MsgSyn";
    }
}