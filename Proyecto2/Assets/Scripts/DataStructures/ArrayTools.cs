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
    }
}