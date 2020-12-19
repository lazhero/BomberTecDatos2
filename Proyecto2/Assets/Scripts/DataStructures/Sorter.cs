using System.Collections.Generic;

namespace DataStructures
{
    public class Sorter 
    {
        /// <summary>
        /// sorts a list
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] BubbleSort<T>(T[] list)
        {
            bool KeepIterating = true;
            while (KeepIterating)
            {
                KeepIterating = false;
                for (int i = 0; i < list.Length-1; i++)
                {
                    T x = list[i];
                    T y = list[i + 1];
                    if ( Comparer<T>.Default.Compare(x,y)>0)
                    {
                        list[i] = y;
                        list[i + 1] = x;
                        KeepIterating = true;

                    }                    
                }
            }

            return list;
        }
    }
}