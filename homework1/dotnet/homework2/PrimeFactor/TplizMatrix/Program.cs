using System;

namespace TplizMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = { { 1, 2, 3, 4 }, { 5, 1, 2, 3 }, { 9, 5, 1, 2 } };
            Console.WriteLine(isTplzMatrix(array)?"true":"false");
        }

        private static bool isTplzMatrix(int[,] array)
        {
            bool TplzFlag = true;
            int i = array.GetLength(0)-1, j=0;
            int idx1, idx2;//temp var to iter in matrix
            while (!(i == 0 && j == array.GetLength(1)-1))//iterate from left lowest to right upperest
            {
                idx1 = i; idx2 = j;
                while (idx1 != array.GetLength(0) && idx2 != array.GetLength(1))//didn't range out, then judge if is the same with (i, j)
                {
                    if (array[idx1,idx2]!=array[i,j])
                    {
                        return false;
                    }                        
                    ++idx1;++idx2;
                }
                switchpair(ref i, ref j);
            }

            return TplzFlag;
        }

        private static void switchpair(ref int i, ref int j)
        {
            if(i != 0)
            {
                i -= 1;
                return;
            }
            j += 1;
            return;
        }
    }
}
