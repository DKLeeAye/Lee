using System;
using System.Collections.Generic;
using System.Text;

namespace Queue
{
    /// <summary>
    /// 队列链式存储（链队列)
    /// </summary>
   public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }
        public Node(T item)
        {
            this.Item = item;
        }
        public Node()
        {

        }
    }
    public class LinkQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int size;

        public LinkQueue()
        {
            this.head = null;
            this.tail = null;
            this.size = 0;
        }

        //入队
        public void InQueue(T item)
        {
            Node<T> oldLastNode = tail;
            tail = new Node<T>();
            tail.Item = item;
            if (IsEmpty())
            {
                head = tail;
            }
            else
            {
                oldLastNode.Next = tail;
            }
            size++;
        }

        //出队
        public T OutQueue()
        {
            T result = head.Item;
            head = head.Next;
            size--;

            if (IsEmpty())
            {
                tail = null;
            }
            return result;
        }

        //判断队列是否为空
        public bool IsEmpty()
        {
            return this.size == 0;
        }

        //队列的节点数目
        public int Size
        {
            get
            {
                return this.size;
            }
        }
    }
}
