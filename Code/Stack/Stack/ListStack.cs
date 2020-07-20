using System;
using System.Collections.Generic;
using System.Text;

namespace Stack
{
        ///<summary>
        ///栈的链式存储结构（链表）
        /// </summary>

    public class Node<T>
        {
            public T Item { get; set; }
            public Node<T> Next { get; set; }

            public Node(T Item)
            {
                this.Item = Item;
            }

            public Node()//默认的构造方法无参构造方法
        {

            }

        }

        public class LinkStack<T>
        {
            private Node<T> first;
            private int index;

            public LinkStack()
            {
                this.first = null;
                this.index = 0;
            }
           
            // 入栈
            public void Push(T item)
            {
                Node<T> oldNode = first;//保存原来的栈顶元素
                first = new Node<T>();//新建一个栈顶元素
                first.Item = item;
                first.Next = oldNode;//指向原本的栈顶元素

                index++;
            }

           // 出栈
            public T Pop()
            {
                T item = first.Item;//保存一下将弹出的元素的值
                first = first.Next;//将栈顶元素设为下一个
                index--;

                return item;
            }

           //判断是否为空栈
            public bool IsEmpty()
            {
                return this.index == 0;
            }

            //栈中节点数目
            public int Size
            {
                get
                {
                    return this.index;
                }
            }
        }
    
}
