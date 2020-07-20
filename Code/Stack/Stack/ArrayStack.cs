using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Stack
{
    /// <summary>
    /// 栈的顺序存储结构（数组）（动态扩容）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    
    public class ArrayStack<T>
    {
        private T[] nodes;
        private int index;

        public ArrayStack(int capacity)
        {
            this.nodes = new T[capacity];
            this.index = 0;
        }

        //入栈
        public void Push(T node)
        {
            if (index == nodes.Length)
            {
                ResizeCapacity(nodes.Length * 2);//容量增大
            }

            nodes[index] = node;
            index++;//指向下一个即将入栈的位置
        }

        //出栈
        public T Pop()
        {
            if(index == 0)
            {
                return default;
            }
            T node = nodes[index - 1];//出栈元素
            index--;
            nodes[index] = default(T);//出栈的元素设置为该数据类型的默认值

            if(index >0 && index == nodes.Length / 4)
            {
                ResizeCapacity(nodes.Length /2);//容量收缩
            }
            return node;//返回出栈的元素
        }

        //改变数组大小
        private void ResizeCapacity(int newCapacity)
        {
            T[] newNodes = new T[newCapacity];//新数组
            if (newCapacity > nodes.Length)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    newNodes[i] = nodes[i];
                }
            }
            else
            {
                for (int i = 0; i < newCapacity; i++)
                {
                    newNodes[i] = nodes[i];
                }
            }

            nodes = newNodes;
        }

        //判断栈是否为空
        public bool IsEmpty()
        {
            return this.index == 0;
        }

        //栈的节点数目
        public int Size
        {
            get
            {
                return this.index;
            }
        }
    }
}
