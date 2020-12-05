using System.Collections.Generic;
namespace DataStructures
{
    public class ArrayTools<T>
    {
        public static void initArray(T[] array, T data)
        {
            if (array == null) return;
            if (array.Length <= 0) return;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = data;
            }
        }

        public static T[] getArrayFromStack(Stack<T> stack)
        {
            T[] array=new T[stack.Count];
            for (int i = 0; i < stack.Count; i++)
            {
                array[i] = stack.Pop();
            }
            return array;
        }
    }
    
}