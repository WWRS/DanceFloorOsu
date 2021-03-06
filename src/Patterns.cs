using System;
using System.Collections.Generic;
using System.Linq;

namespace DanceFloorOsu
{
    public static class Patterns
    {
        public static int[] Random(int num)
        {
            if (num < 0)
            {
                return new int[] { };
            }

            if (num >= 100)
            {
                return Utils.ArrayRange(100);
            }

            HashSet<int> output = new HashSet<int>();
            while (output.Count < num)
            {
                output.Add(UnityEngine.Random.Range(0, 100));
            }

            return output.ToArray();
        }

        public static int[] SmartRandom(int num, DanceSquare[] squares)
        {
            if (num < 0)
            {
                return new int[] { };
            }

            if (num >= 100)
            {
                return Utils.ArrayRange(100);
            }

            //List<int> indices = Utils.ArrayRange(100).ToList();
            //Utils.Shuffle(indices);
            //indices.Sort((a, b) => (squares[a].TimeSet < squares[b].TimeSet ? -1 : 1));
            //return indices.Take(num).ToArray();

            float time = UnityEngine.Time.time;
            float minDt = 100;
            float maxDt = 0;
            for (int i = 0; i < 100; i++)
            {
                if (time - squares[i].TimeSet < minDt)
                {
                    minDt = time - squares[i].TimeSet;
                }
                if (time - squares[i].TimeSet > maxDt)
                {
                    maxDt = time - squares[i].TimeSet;
                }
            }

            List<int> output = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                float val = (time - squares[i].TimeSet - minDt) / (maxDt - minDt);
                if (UnityEngine.Random.Range(0, 2f) < (num / 100f) + val)
                {
                    output.Add(i);
                }
            }

            return output.ToArray();
        }

        public static int[] FullHoriz1(int time)
        {
            int[] output = new int[10];
            for (int i = 0; i < 10; i++)
            {
                output[i] = time * 10 + i;
            }

            return output;
        }

        public static int[] FullHoriz2(int time)
        {
            int[] output = new int[10];
            for (int i = 0; i < 10; i++)
            {
                output[i] = i * 10 + time;
            }

            return output;
        }

        public static int[] HalvesHoriz1(int time)
        {
            int[] output = new int[20];
            for (int i = 0; i < 10; i++)
            {
                output[i] = time * 10 + i;
                output[10 + i] = (9 - time) * 10 + i;
            }

            return output;
        }

        public static int[] HalvesHoriz2(int time)
        {
            int[] output = new int[20];
            for (int i = 0; i < 10; i++)
            {
                output[i] = i * 10 + time;
                output[10 + i] = i * 10 + (9 - time);
            }

            return output;
        }

        public static int[] Droplet(int time)
        {
            List<int> results = new List<int>();

            int b = time * time;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int a = i * i + j * j;
                    if (a <= b && a > b - 10)
                    {
                        results.Add((5 + i) + 10 * (5 + j));
                        results.Add((4 - i) + 10 * (5 + j));
                        results.Add((5 + i) + 10 * (4 - j));
                        results.Add((4 - i) + 10 * (4 - j));
                    }
                }
            }

            return results.ToArray();
        }
    }
}
