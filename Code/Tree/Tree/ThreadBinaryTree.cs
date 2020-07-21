using System;
using System.Collections.Generic;
using System.Text;

namespace Tree
{
   public class ThreadNode
    {
        public int data;
        public int lthread;
        public int rthread;
        public ThreadNode lchild;
        public ThreadNode rchild;

        //线索树构造函数
        public ThreadNode()
        {
        }
        public ThreadNode(int data)
        {
            this.data = data;
        }

        public ThreadNode(int data,ThreadNode lchild,ThreadNode rchild)
        {
            this.data = data;
            this.lchild = lchild;
            this.rchild = rchild;
        }


    }
    
    public class ThreadBinaryTree<T>
    {
        public ThreadNode root;//线索二叉树根节点
        public ThreadNode Root
        {
            get
            {
                return this.root;
            }
        }

        public ThreadBinaryTree()
        {
            root = null;
        }

        public ThreadBinaryTree(int[] data)
        {
            for (int i =0; i < data.Length; i++)
            {
                AddNodeToTree(data[i]); 
            }
        }

        public void AddNodeToTree(int value)
        {
            ThreadNode newnode = new ThreadNode(value);
            ThreadNode current;
            ThreadNode parent;
            ThreadNode previous = new ThreadNode(value);
            int pos;

            //设置线索二叉树开头节点
            if(root == null)
            {
                root = newnode;
                root.lchild = root;
                root.rchild = null;
                root.lthread = 0;
                root.rthread = 1;
                return;
            }

            //设置开头节点所指的节点
            current = root.rchild;
            if(current == null)
            {
                root.rchild = newnode;
                newnode.rchild = root;
                newnode.lchild = root;
                return;
            }
            parent = root;
            pos = 0;
            while(current != null)
            {
                if(current.data > value)
                {
                    if (pos != -1)
                    {
                        pos = -1;
                        previous = parent;
                    }
                    parent = current;
                    if (current.lthread == 1)
                        current = current.lchild;
                    else
                        current = null;
                }
              else
                {
                    if (pos != 1)
                    {
                        pos = 1;
                        previous = parent;
                    }
                    parent = current;
                    if (current.rthread == 1)
                        current = current.rchild;
                    else
                        current = null;
                }
            }
            if(parent.data > value)
            {
                parent.lthread = 1;
                parent.lchild = newnode;
                newnode.lchild = previous;
                newnode.rchild = parent;
            }
            else
            {
                parent.rthread = 1;
                parent.rchild = newnode;
                newnode.rchild = previous;
                newnode.lchild = parent;
            }
            return;
        }

        //线索二叉树中序遍历
        public void MidOrder()
        {
            ThreadNode temNode;
            temNode = root;
            do
            {
                if (temNode.rthread == 0)//节点右线索字段是否为0  指针为线索
                {
                    temNode = temNode.rchild;
                }
                else
                {
                    temNode = temNode.rchild;
                    while (temNode.lthread != 0)
                    {
                        temNode = temNode.lchild;
                    }
                }
                if (temNode != root)
                    Console.WriteLine("[" + temNode.data + "]");
            } while (temNode != root);
        }
    }


}
