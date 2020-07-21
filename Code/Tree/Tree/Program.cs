using System;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBinaryTreeBasicTest();

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("点击任意键查看线索二叉树测试结果");
            Console.ReadKey();

            //线索二叉树测试
            Console.WriteLine("建立线索二叉树，中序遍历,有排序的效果");
            Console.WriteLine("除了第一个数字作为线索二叉树的开头节点外");
            int[] datal = { 0, 10, 20, 30, 100, 399, 453, 43, 237, 373, 655 };
            ThreadBinaryTree<int> tree = new ThreadBinaryTree<int>(datal);
            tree.MidOrder();
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
            Console.WriteLine("二叉树目前的深度是: {0}", bTree.GetDepth(bTree.Root));




            // 前序遍历
            Console.WriteLine("---------前序遍历---------");
            bTree.PreOrder(bTree.Root);
            // 中序遍历
            Console.WriteLine();
            Console.WriteLine("---------中序遍历---------");
            bTree.MidOrder(bTree.Root);
            // 后序遍历
            Console.WriteLine();
            Console.WriteLine("---------后续遍历---------");
            bTree.PostOrder(bTree.Root);
            Console.WriteLine();



            // 前序遍历（非递归）
            Console.WriteLine("---------前序遍历（非递归）---------");
            bTree.PreOrderNoRecurise(bTree.Root);
            // 中序遍历（非递归）
            Console.WriteLine();
            Console.WriteLine("---------中序遍历（非递归）---------");
            bTree.MidOrderNoRecurise(bTree.Root);
            // 后序遍历（非递归）
            Console.WriteLine();
            Console.WriteLine("---------后序遍历（非递归）---------");
            bTree.PostOrderNoRecurise(bTree.Root);
            Console.WriteLine();
            
        }


       

       
    }
}
