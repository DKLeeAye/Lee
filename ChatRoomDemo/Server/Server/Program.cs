using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

    class ClientState   //客户端类  存储客户端的一些信息
    {
        public Socket socket;
        public byte[] readBuff = new byte[1024];
        public string userName = "";
    }
    class Program
    {
        static Socket listenfd;
        
    //客户端Socket及状态信息
        static Dictionary<Socket, ClientState> clients = new Dictionary<Socket, ClientState>();
        public static void Main(string[] args)
        {
            //Socket
            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Bind
            IPAddress ipAdr = IPAddress.Parse("127.0.0.1");//回送地址127.0.0.1 指本地机，一般用于测试
            IPEndPoint ipEp = new IPEndPoint(ipAdr, 8888);//8888端口  指定IP与端口
            listenfd.Bind(ipEp);//给listenfd套接字绑定IP和端口

            //listen
            listenfd.Listen(0);//开启监听，等待客户端连接 参数backlog指定队列中最多可容纳等待接受的连接数 0代表不限制
            Console.WriteLine("服务器启动成功");

            //接受
            listenfd.BeginAccept(AcceptCallback, listenfd);
            //等待
            Console.ReadLine();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Console.WriteLine("服务器Accept");
                Socket listenfd = (Socket)ar.AsyncState;
                Socket clientfd = listenfd.EndAccept(ar);//新客户端的socket
                //clients列表
                ClientState state = new ClientState();
                state.socket = clientfd;
                clients.Add(clientfd, state);

                //接收数据
                clientfd.BeginReceive(state.readBuff, 0, 1024, 0, ReceiveCallback, state);
                //继续Accept
                listenfd.BeginAccept(AcceptCallback, listenfd);
            }
            catch(SocketException ex)
            {
                Console.WriteLine("Socket接收失败" + ex.ToString());
            }
        }

        public static void ReceiveCallback(IAsyncResult ar) 
        {
            try
            {
                ClientState state = (ClientState)ar.AsyncState;
                Socket clientfd = state.socket;
                int count = clientfd.EndReceive(ar);
                //客户端关闭
                if(count == 0 )
                {
                    clientfd.Close();
                    clients.Remove(clientfd);
                    Console.WriteLine("Socket close");
                    return;
                }

                string recvStr = System.Text.Encoding.Default.GetString(state.readBuff, 0, count);
                byte[] sendBytes = System.Text.Encoding.Default.GetBytes(recvStr);
                //clientfd.Send(sendBytes);
                //遍历所有用户，发送信息
                foreach (ClientState s in clients.Values)
                {
                s.socket.Send(sendBytes);//这里暂时未使用异步发送
                }

                clientfd.BeginReceive(state.readBuff, 0, 1024, 0, ReceiveCallback, state);
            }
            catch(SocketException ex)
            {
                Console.WriteLine("Socket接收失败" + ex.ToString());
            }
        }
    }