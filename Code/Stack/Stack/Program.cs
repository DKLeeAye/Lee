using System;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            // 基于链表的栈
            LinkStackTest();
            // 基于数组的栈
            ArrayStackTest();
        }


       /// <summary>
       /// 栈（链表）测试
       /// </summary>
        static void LinkStackTest()
        {
            LinkStack<int> stack = new LinkStack<int>();
            Console.WriteLine("01初始化堆栈，当前为空栈");
            Console.WriteLine("IsEmpty:{0}", stack.IsEmpty());
            

            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(rand.Next(1, 10));//随机生成1—10压栈
            }
            Console.WriteLine("02随机压入1-10内10个数据");
            Console.WriteLine("IsEmpty:{0}", stack.IsEmpty());
            Console.WriteLine("Size:{0}", stack.Size);

            Console.WriteLine("03弹出栈内10个元素，依次打印他们的值");
            for (int i = 0; i < 10; i++)
            {
                int node = stack.Pop();
                Console.Write(node + " ");//依次打印弹出元素，用“ ”隔开
            }
            Console.WriteLine();
            Console.WriteLine("IsEmpty:{0}", stack.IsEmpty());
            Console.WriteLine("Size:{0}", stack.Size);
        }

        /// <summary>
        /// 栈（数组）测试
        /// </summary>
        static void ArrayStackTest()
        {
            ArrayStack<int> stack = new ArrayStack<int>(10);
            Console.WriteLine("01初始化堆栈，当前为空栈");
            Console.WriteLine("IsEmpty:{0}", stack.IsEmpty());

            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(rand.Next(1, 10));//随机生成1—10压栈
            }
            Console.WriteLine("02随机压入1-10内10个数据");
            Console.WriteLine("IsEmpty:{0}", stack.IsEmpty());
            Console.WriteLine("Size:{0}", stack.Size);

            Console.WriteLine("03弹出栈内10个元素，依次打印他们的值");
            for (int i = 0; i < 10; i++)
            {
                int node = stack.Pop();
                Console.Write(node + " ");//依次打印弹出元素，用“ ”隔开
            }
            Console.WriteLine();
            Console.WriteLine("IsEmpty:{0}", stack.IsEmpty());
            Console.WriteLine("Size:{0}", stack.Size);
        }

    }
}
