using System;
using System.IO;

namespace List
{
    class Program
    {
        static void Main(string[] args)
        {
          Clist mList = new Clist();
            mList.Append(1);
            mList.Append(2);
            mList.Append(3);
            mList.Append(4);//1234

           
           mList.Insert(2,1);//第二个位置插入一个1
           mList.ShowList(mList);
           mList.Delete();//删除最后一位
           mList.ShowList(mList);
           mList.GetElem(4);//获取并显示第四位的元素
            mList.Insert(9);//在当前位置插入一个9
            mList.ShowList(mList);
           mList.Clear();//清空链表
           mList.ShowList(mList);
            
        } 
    }

    public class ListNode    //节点类
    {
        public ListNode(int NewValue)
        {
            Value = NewValue;
        }

        public ListNode Previous;//存储上一个节点的指针
        public ListNode Next;//存储下一个节点的指针
        public int Value;//存储节点本身的数据
    }


    public class Clist
    {
        public Clist()//构造函数
        {
            ListCountValue = 0;
            Head = null;
            Tail = null;
        }

        private ListNode Head;//头节点指针
        private ListNode Tail;//尾节点指针
        private ListNode Current;//当前节点指针
        private int ListCountValue;//链表数据的个数

        public Clist(string str)//创建链表
        {
            char[] separator = { ',', ' ' };
            string[] s = str.Split(separator);
            Clist list = new Clist();//空的链表
            foreach (string i in s)//依次转换成数字加给list尾部
            {
                int j = Convert.ToInt32(i);
                list.Append(j);
            }
            ListCountValue = s.Length;
            Tail = list.Current;
            Head = list.Head;
        }

        public void Append(int DataValue)//单链表尾部添加数据
        {
            ListNode NewNode = new ListNode(DataValue);
            if (IsNull())//为空时添加
            {
                Head = NewNode;
                Tail = NewNode;
            }
            else
            {
                Tail.Next = NewNode;
                NewNode.Previous = Tail;
                Tail = NewNode;
            }
            Current = NewNode;
            ListCountValue += 1;
        }

        public void ShowList(Clist list)//打印显示链表中的数
        {
            Console.WriteLine("当前链表中数据为：");
            list.MoveFrist();
            for (int i = 0; i < list.ListCount; i++)
            {
                Console.Write(list.GetCurrentValue() + "  ");
                list.MoveNext();
            }
        }

        public void MoveNext()//向后移动一个
        {
            if (!IsEof()) Current = Current.Next;
        }

        public void MoveFrist()//移动到第一个
        {
            Current = Head;
        }

        public int GetCurrentValue()//获取当前数据
        {
            return Current.Value;
        }


        public int ListCount//取得链表的数据个数
        {
            get
            {
                return ListCountValue;
            }
        }

        public bool IsNull()//判断是否为空
        {
            if (ListCountValue == 0)
                return true;
            return false;
        }

        public bool IsEof()//判断是否到尾
        {
            if (Current == Tail)
                return true;
            return false;
        }

        public bool IsBof()//判断是否到头
        {
            if (Current == Head)
                return true;
            return false;
        }

        

        public int GetElem(int i)//获取第i个元素
        {
            int DataValue = 0;
            ListNode NewNode = new ListNode(DataValue);
            NewNode.Next = Head.Next;
            NewNode.Previous = Head.Previous;
            NewNode.Value = Head.Value;
            int j = 1;
            while (NewNode != null && j < i)//从1开始遍历
            {
                NewNode = NewNode.Next;
                ++j;
            }
            if (NewNode==null || j > i)
            {
                System.Console.WriteLine("第" + i + "个元素" + "不存在");
                return 0;
            }
                
           
            DataValue = NewNode.Value;//取第i个元素数据
            System.Console.WriteLine("第" + i + "个元素为" + DataValue);
            return DataValue;
            
        }

        public void Insert(int DataValue)//当前位置插入数据
        {
            ListNode NewNode = new ListNode(DataValue);
            if (IsNull())
            {
                //为空表，则添加
                Append(DataValue);
                return;
            }
            if (IsBof())
            {
                //为头部插入
                NewNode.Next = Head;
                Head.Previous = NewNode;
                Head = NewNode;
                Current = Head;
                ListCountValue += 1;
                return;
            }
            //中间插入
            NewNode.Next = Current;
            NewNode.Previous = Current.Previous;
            Current.Previous.Next = NewNode;
            Current.Previous = NewNode;
            Current = NewNode;
            ListCountValue += 1;
        }

        public bool Insert(int i, int DataValue)//在第i个元素处插入新的数据元素
        {
            ListNode InsertNode = new ListNode(DataValue);
            ListNode Node = new ListNode(0);
            Node.Next = Head.Next;
            Node.Previous = Head.Previous;
            Node.Value = Head.Value;
            int j = 1;
            while (Node != null && j < i)//从1开始遍历
            {
                Node = Node.Next;
                ++j;
            }
            if (Node == null || j > i)
            {
                System.Console.WriteLine("第" + i + "个元素" + "不存在");
                return false;
            }

            InsertNode.Next = Node;
            Node.Previous.Next = InsertNode;
            ListCountValue += 1;
            return true;

        }

        public void Clear()//清空链表
        {
            MoveFrist();
            while (!IsNull())
            {
                //若不为空链表,从尾部删除  
                Delete();
            }
        }

        public void Delete()//单链表删除当前数据
        {
            if (!IsNull())
            {
                if (IsBof())
                {
                    Head = Current.Next;
                    Current = Head;
                    ListCountValue -= 1;
                    return;
                }
                if (IsEof())
                {
                    Tail = Current.Previous;
                    Current = Tail;
                    ListCountValue -= 1;
                    return;
                }
                Current.Previous.Next = Current.Next;
                Current = Current.Previous;
                ListCountValue -= 1;
                return;
            }
        }
    

}
}
