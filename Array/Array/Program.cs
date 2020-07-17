using System;
using System.Diagnostics;
using System.IO;

namespace Array
{
    class Program
    {
        static void Main()
        {
            int Index = 3;
            int Value = 0;

            int[] array = new int[5];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            foreach (int item in array)
                System.Console.WriteLine(item);

            array=ArrayInsert(array,Index,Value);
            System.Console.WriteLine("插入元素后\n");
            foreach (int item in array)
                System.Console.WriteLine(item);
            array = ArrayDelete(array, Index, Value);
            System.Console.WriteLine("删除元素后\n");
            foreach (int item in array)
                System.Console.WriteLine(item);

        }

        public static int[] ArrayInsert(int[] ArrayBorn,int Index,int Value)
        {
            if (Index >= (ArrayBorn.Length))//是否超出原数组长度
                System.Console.WriteLine("ERROR");
            int[] NewArray = new int[ArrayBorn.Length + 1];//声明一个新数组
            for (int i = 0; i < NewArray.Length; i++)
            {
                if (Index >= 0)
                {
                    if (i < (Index -1))
                        NewArray[i] = ArrayBorn[i];
                    else if (i == (Index-1))
                        NewArray[i] = Value;
                    else
                        NewArray[i] = ArrayBorn[i - 1];
                }
                else
                    System.Console.WriteLine("ERROR");
            }
            return NewArray;

        }

        public static int[] ArrayDelete(int[] ArrayBorn, int Index, int Value)
        {
            if (Index >= (ArrayBorn.Length))//是否超出原数组长度
                System.Console.WriteLine("ERROR");
            int[] NewArray = new int[ArrayBorn.Length - 1];//声明一个新数组
            for (int i = 0; i < NewArray.Length; i++)
            {
                if (Index >= 0)
                {
                    if (i < (Index - 1))
                        NewArray[i] = ArrayBorn[i];
                    else
                        NewArray[i] = ArrayBorn[i + 1];
                }
                else
                    System.Console.WriteLine("ERROR");

            }
            return NewArray;

        }
    }
         
    
    





}
