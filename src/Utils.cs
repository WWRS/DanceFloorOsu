using System.Collections.Generic;

namespace DanceFloorOsu
{
    public static class Utils
    {
        public static int[] ArrayRange(int low, int high)
        {
            int[] all = new int[high - low];
            for (int i = low; i < high; i++)
            {
                all[i] = i;
            }

            return all;
        }
        
        public static int[] ArrayRange(int high)
        {
            return ArrayRange(0, high);
        }

        public static void Shuffle<T>(List<T> list)
        {
            int listLen = list.Count;
            for (int i = 0; i < listLen; i++)
            {
                int j = UnityEngine.Random.Range(i, listLen);
                T temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
        }
    }
}
