using System;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayQueueTest();
            LinkQueueTest();
        }


        static void ArrayQueueTest()
        {
            ArrayQueue<int> queue = new ArrayQueue<int>(5);
            Console.WriteLine("01初始化队列，当前为空队列");
            Console.WriteLine("Is Empty:{0}", queue.IsEmpty());
            Console.WriteLine("Size:{0}", queue.Size);
            
            Random rand = new Random();
            Console.WriteLine("02随机入队5个1-10内的数");
            for (int i = 0; i < 5; i++)
            {
                int num = rand.Next(1, 10);
                queue.InQueue(num);
                Console.WriteLine("{0}", num);
            }
            Console.WriteLine("Is Empty:{0}", queue.IsEmpty());
            Console.WriteLine("Size:{0}", queue.Size);

            Console.WriteLine("03五个元素依次出队");
            for(int i=0; i<5; i++)
            {
                Console.WriteLine("{0}", queue.OutQueue());
                Console.WriteLine("Is Empty:{0}", queue.IsEmpty());
                Console.WriteLine("Size:{0}", queue.Size);
            }
        }

        static void LinkQueueTest()
        {
            LinkQueue<int> queue = new LinkQueue<int>();
            Console.WriteLine("01初始化队列，当前为空队列");
            Console.WriteLine("Is Empty:{0}", queue.IsEmpty());
            Console.WriteLine("Size:{0}", queue.Size);

            Random rand = new Random();
            Console.WriteLine("02随机入队5个1-10内的数");
            for (int i = 0; i < 5; i++)
            {
                int num = rand.Next(1, 10);
                queue.InQueue(num);
                Console.WriteLine("{0}", num);
            }
            Console.WriteLine("Is Empty:{0}", queue.IsEmpty());
            Console.WriteLine("Size:{0}", queue.Size);

            Console.WriteLine("03五个元素依次出队");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("{0}", queue.OutQueue());
                Console.WriteLine("Is Empty:{0}", queue.IsEmpty());
                Console.WriteLine("Size:{0}", queue.Size);
            }
        }
    }
}
