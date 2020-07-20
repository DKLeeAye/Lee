using System;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBinaryTreeBasicTest();
          
           

            Console.ReadKey();
        } 
        static void MyBinaryTreeBasicTest()
        {
            // 构造一颗二叉树，根节点为"A"
            BinaryTree<string> bTree = new BinaryTree<string>("A");
            Node<string> rootNode = bTree.Root;
            // 向根节点"A"插入左孩子节点"B"和右孩子节点"C"
            bTree.InsertLeft(rootNode, "B");
            bTree.InsertRight(rootNode, "C");
            // 向节点"B"插入左孩子节点"D"和右孩子节点"E"
            Node<string> nodeB = rootNode.lchild;
            bTree.InsertLeft(nodeB, "D");
            bTree.InsertRight(nodeB, "E");
            // 向节点"C"插入右孩子节点"F"
            Node<string> nodeC = rootNode.rchild;
            bTree.InsertRight(nodeC, "F");
            // 计算二叉树目前的深度
            Console.WriteLine("The depth of the tree : {0}", bTree.GetDepth(bTree.Root));

            // 前序遍历
            Console.WriteLine("---------PreOrder---------");
            bTree.PreOrder(bTree.Root);
            // 中序遍历
            Console.WriteLine();
            Console.WriteLine("---------MidOrder---------");
            bTree.MidOrder(bTree.Root);
            // 后序遍历
            Console.WriteLine();
            Console.WriteLine("---------PostOrder---------");
            bTree.PostOrder(bTree.Root);
            Console.WriteLine();
            // 前序遍历（非递归）
            Console.WriteLine("---------PreOrderNoRecurise---------");
            bTree.PreOrderNoRecurise(bTree.Root);
            // 中序遍历（非递归）
            Console.WriteLine();
            Console.WriteLine("---------MidOrderNoRecurise---------");
            bTree.MidOrderNoRecurise(bTree.Root);
            // 后序遍历（非递归）
            Console.WriteLine();
            Console.WriteLine("---------PostOrderNoRecurise---------");
            bTree.PostOrderNoRecurise(bTree.Root);
            Console.WriteLine();
            // 层次遍历
            Console.WriteLine("---------LevelOrderNoRecurise---------");
            bTree.LevelOrder(bTree.Root);
        }


       

       
    }
}
