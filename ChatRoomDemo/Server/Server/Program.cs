using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            NetManager.StartLoop(8888);
        }
    }
}
