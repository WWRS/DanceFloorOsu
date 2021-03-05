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
    }
}
