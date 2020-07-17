using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace ListInsert
{
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
 


    public class Clist   //单链表中的节点操作
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

        public int Get(int i)//单链表的读取
        {
            int DataValue = 0;
            ListNode NewNode = new ListNode(DataValue);
            NewNode = Head.Next;
            int j = 1;
            while (j < i)//从1开始遍历
            {
                NewNode = NewNode.Next;
                ++j;
            }
            if (j > i)
                System.Console.WriteLine("第i个元素不存在");
            return DataValue;
        }

        public  void  Insert(List<int> mList, int i, int data)//插入新的数据元素
        {
            int j = 1;
            int DataValue = 0;
            ListNode NewNode = new ListNode(DataValue);
            ListNode ExNode = new ListNode(DataValue);
            NewNode = Head.Next;
            while (j < i)
            {
                NewNode = NewNode.Next;
                ++j;
            }
            if (j > i)
                System.Console.WriteLine("第i个元素不存在");
            ExNode.Value = data;
            ExNode.Next = NewNode.Next;
            NewNode.Next = ExNode;
            
        }
        public void Append(int DataValue)//单链表尾部添加数据
        {
            ListNode NewNode = new ListNode(DataValue);
            if(Head.Next == null)
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

       
        
        public void Delete()//单链表删除当前数据
        {
            if (Head.Next == null)
            {
                if (Current.Previous == null)
                {
                    Head = Current.Next;
                    Current = Head;
                    ListCountValue -= 1;
                    return;
                }
                if (Current.Next == null)
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

    class program
    {
        static void Main(string[] args)
        {
           
           
        }

      
    }
   


}
