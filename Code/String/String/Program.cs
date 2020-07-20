using System;
using System.Text;
using System.Linq;

namespace String
{
    public class String
    {
        public int Index(string S, string T, int pos)
        {
            int n, m, i;
            string sub;
            if(pos > 0)
            {
                n = S.Length;
                m = T.Length;
                i = pos;
                while (i <= n - m + 1)
                {
                    sub = S.Substring(i, m);
                    if (!(sub.Equals(T)))
                        ++i;
                    else
                        return i;
                }
               
            }
            return 0;

        }

        private static int KmpMatch(char[] str, char[] model, int pos)
        {
            int loc = -1;
            if (pos < 1 || pos > str.Length)
            {
                return loc;
            }

            int i = pos - 1;
            int j = 0;
            int[] next = GetNext(model);

            while (i < str.Length && j < model.Length)
            {
                if (j == -1 || str[i] == model[j])
                {
                    i++;
                    j++;
                }
                else
                    j = next[j];
            }
            if (j >= model.Length)
                loc = i - model.Length;

            return loc;
        }

        private static int[] GetNext(char[] T)
        {
            int[] next = new int[T.Length];
            next[0] = -1;


            int i = 0, j = -1;

            while (i < T.Length - 1)
            {
                if (j == -1 || T[i] == T[j])
                {
                    ++i; ++j;
                    if (T[i] != T[j])
                        next[i] = j;
                    else
                        next[i] = next[j];
                }
                else
                    j = next[j];
            }

            return next;
        }

    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
