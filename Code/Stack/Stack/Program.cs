using System;
using List;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            CStack stack = new CStack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.ShowStack();
            stack.Pop();
            stack.ShowStack();
            stack.Clear();
            stack.ShowStack();
          
        }
    }

    public class CStack
    {
        private Clist m_List;
        public CStack()
        {
            m_List = new Clist();
        }
        //压栈
        public void Push(int PushValue)
        {
            m_List.Append(PushValue);
        }
        //弹出
        public int Pop()
        {
            int PopValue;
            if (!IsNullStack())
            {
                MoveTop();
                PopValue = m_List.GetCurrentValue();
                m_List.Delete();
                return PopValue;
            }
            return 2147483647;
        }

        //判断是否为空栈
        public bool IsNullStack()
        {
            if (m_List.IsNull())
                return true;
            return false;
        }

        //堆栈个数
        public int StackListCount
        {
            get
            {
                return m_List.ListCount;
            }
        }

        //移动到栈底
        public void MoveBottom()
        {
            m_List.MoveFrist();
        }
        //移动到栈顶
       public void MoveTop()
        {
            m_List.MoveLast();
        }

        //向上
        public void MoveUp()
        {
            m_List.MoveNext();
        }

        //向下
        public void MoveDown()
        {
            m_List.MovePrevious();
        }

        //获得当前的值
        public int GetCurrentValue()
        {
            return m_List.GetCurrentValue();
        }
        //删除当前的节点
        public void Delete()
        {
            m_List.Delete();
        }
        //清空堆栈
        public void Clear()
        {
            m_List.Clear();
        }

        public void ShowStack()
        {
            m_List.ShowList(m_List);
        }
        
    }
}
