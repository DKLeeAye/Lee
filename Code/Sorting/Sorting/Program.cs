using System;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------冒泡排序---------");
            int[] Datas1 = { 3, 6, 5, 9, 7, 1, 8, 2, 4 };
            SortingHelper<int>.BubbleSort(Datas1);
            PrintData(Datas1);

            Console.WriteLine("------直接插入排序-------");
            int[] Datas2 = { 5, 4, 45, 69, 23, 52, 109, 33 };
            SortingHelper<int>.StraightInsertSort(Datas2);
            PrintData(Datas2);

            Console.WriteLine("------快速排序-------");
            int[] Datas3 = { 15, 34, 45, 87, 213, 22, 109, 33 };
            SortingHelper<int>.QuickSort(Datas3,0,Datas3.Length-1);
            PrintData(Datas3);

            Console.WriteLine("------简单选择排序-------");
            int[] Datas4 = { 55, 34, 5, 87, 23, 22, 19, 38 };
            SortingHelper<int>.SimpleSelectSort(Datas4);
            PrintData(Datas4);

            Console.WriteLine("------堆排序-------");
            int[] Datas5 = { 5, 3, 25, 7, 223, 72, 109, 83 };
            SortingHelper<int>.HeapSort(Datas5);
            PrintData(Datas5);

            Console.WriteLine("------二路归并排序-------");
            int[] Datas6 = { 65, 73, 2, 7, 71, 49, 9, 81 };
            SortingHelper<int>.MergeSort(Datas6,0, (Datas6.Length -1));
            PrintData(Datas6);


        }


        //打印
        static void PrintData(int[] arr)
        {
            Console.WriteLine("----------------------------------------------------\r\n");
            for (int i = 0, count = 1; i < arr.Length; i++, count++)
            {
                if (count % 10 == 0)
                {
                    count = 1;
                    Console.Write(arr[i]);
                    Console.WriteLine();
                }
                else
                {
                    count++;
                    Console.Write(arr[i] + " ");
                }
            }
            Console.WriteLine("\r\n----------------------------------------------------");
        }



    }

   
}
