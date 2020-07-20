using System;
using System.Collections.Generic;
using System.Text;

namespace Queue
{
    /// <summary>
    /// 队列顺序存储（基于数组）
    /// </summary>
    public class ArrayQueue<T>
    {
        private T[] items;
        private int size;//节点个数
        private int head;//队头指针
        private int tail;//队尾指针

        public ArrayQueue(int capacity)
        {
            this.items = new T[capacity];
            this.size = 0;
            this.head = this.tail = 0;
        }

        //入队
        public void InQueue(T item)
        {
            if (Size == items.Length)
            {
                // 扩大数组容量
                ResizeCapacity(items.Length * 2);
            }

            items[tail] = item;//保存当前队尾元素值
            tail++;

            size++;
        }

        //出队
        public T OutQueue()
        {
            if (Size == 0)
            {
                return default(T);
            }

            T item = items[head];//保留当前队头值
            items[head] = default(T);//出队元素设为对应数据类型的默认值
            head++;

            if (head > 0 && Size == items.Length / 4)
            {
                // 缩小数组容量
                ResizeCapacity(items.Length / 2);
            }

            size--;
            return item;//返回出队元素值
        }

        //重置数组大小
        private void ResizeCapacity(int newCapacity)
        {
            T[] newItems = new T[newCapacity];
            int index = 0;
            if (newCapacity > items.Length)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    newItems[index++] = items[i];
                }
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (!items[i].Equals(default(T)))
                    {
                        newItems[index++] = items[i];
                    }
                }

                head = tail = 0;
            }

            items = newItems;
        }

        //判断栈是否为空
        public bool IsEmpty()
        {
            return this.size == 0;
        }

        //获取栈中的节点个数
        public int Size
        {
            get
            {
                return this.size;
            }
        }
    }


}
