namespace Shipstone.UtilitiesTest
{
    internal static class Internals
    {
        internal static int[] CreateArray(int count)
        {
            int[] array = new int[count];

            for (int i = 0; i < count; i ++)
            {
                array[i] = i + 1;
            }

            return array;
        }
    }
}
