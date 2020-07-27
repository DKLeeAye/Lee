using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //socket
            Socket listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Bind
            IPAddress ipAdr = IPAddress.Parse("127.0.0.1");//回送地址127.0.0.1 指本地机，一般用于测试
            IPEndPoint ipEp = new IPEndPoint(ipAdr, 8888);//8888端口  指定IP与端口
            listenfd.Bind(ipEp);//给listenfd套接字绑定IP和端口

            //listen
            listenfd.Listen(0);//开启监听，等待客户端连接 参数backlog指定队列中最多可容纳等待接受的连接数 0代表不限制
            Console.WriteLine("服务器启动成功");

            while (true)
            {
                //接受
                Socket connfd = listenfd.Accept();//接受客户端连接  阻塞办法 连接前都会卡死 返回一个新客户端的socket对象connfd用来处理该客户端的数据
                Console.WriteLine("服务器接受");

                //接收
                byte[] readBuff = new byte[1024];
                int count = connfd.Receive(readBuff);
                string readStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
                Console.WriteLine("服务器接收" + readStr);

                //发送
                byte[] sendBytes = System.Text.Encoding.Default.GetBytes(readStr);
                connfd.Send(sendBytes);
            }

        }
    }
}
