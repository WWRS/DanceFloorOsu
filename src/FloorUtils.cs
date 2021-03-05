using System;
using OsuParsers.Beatmaps.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DanceFloorOsu
{
    public static class FloorUtils
    {
        private static int _prevPattern = 0;

        private static int NextPattern(int max)
        {
            return Random.Range(0, max);
        }

        private static int ReversePattern()
        {
            _prevPattern ^= 1;
            return _prevPattern;
        }

        public static void PlayPattern(DanceFloor floor, Func<int, int[]> pattern, int[] ts, float time,
            float offset = 0, Action<int[]> call = null)
        {
            call ??= floor.ChangeFloor;

            float dTime = time / ts.Length;
            for (int i = 0; i < ts.Length; i++)
            {
                int t = ts[i];
                DanceFloorUpdater.Register(i * dTime + offset, () => call.Invoke(pattern.Invoke(t)));
            }
        }

        public static void PlayPattern(DanceFloor floor, Func<int, int[]> pattern, int tCount, float time,
            float offset = 0, Action<int[]> call = null)
        {
            int[] ts = new int[tCount];
            for (int i = 0; i < tCount; i++)
            {
                ts[i] = i;
            }

            PlayPattern(floor, pattern, ts, time, offset, call);
        }

        public static void SmartPattern(DanceFloor floor, HitObject obj, float beatLength)
        {
            if (HitObjectUtils.Length(obj) >= beatLength * 1.5f - 0.002f)
            {
                _prevPattern ^= 2;

                float beats = HitObjectUtils.Length(obj) / beatLength;
                float ms = Mathf.Ceil(beats) * beatLength;
                float len = ms / 1000f;
                //float patternLen = beatLength / (beats % 2 == 0 ? 500f : 1000f);
                float patternLen = beatLength / 500f;
                for (float f = 0; f < len - 0.002f; f += patternLen)
                {
                    FullPattern(floor, patternLen, f, ReversePattern);
                }
            }
            else if (HitObjectUtils.Length(obj) >= beatLength - 0.002f)
            {
                float len = HitObjectUtils.Length(obj) / 1000f;
                FullPattern(floor, len);
            }
            else if (HitObjectUtils.Length(obj) >= beatLength / 2 - 0.002f)
            {
                float len = HitObjectUtils.Length(obj) / 1000f;
                HalfPattern(floor, len);
            }
        }

        private static void FullPattern(DanceFloor floor, float len, float offset = 0, Func<int> next = null)
        {
            next ??= () => NextPattern(4);

            Action play = new Action[]
            {
                () => PlayPattern(floor, Patterns.FullHoriz1, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}, len, offset),
                () => PlayPattern(floor, Patterns.FullHoriz1, new[] {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}, len, offset),
                () => PlayPattern(floor, Patterns.FullHoriz2, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}, len, offset),
                () => PlayPattern(floor, Patterns.FullHoriz2, new[] {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}, len, offset)
            }[next.Invoke()];
            play.Invoke();
        }

        private static void HalfPattern(DanceFloor floor, float len, float offset = 0)
        {
            Action play = new Action[]
            {
                () => PlayPattern(floor, Patterns.HalvesHoriz1, new[] {0, 1, 2, 3, 4}, len, offset),
                () => PlayPattern(floor, Patterns.HalvesHoriz1, new[] {4, 3, 2, 1, 0}, len, offset),
                () => PlayPattern(floor, Patterns.HalvesHoriz2, new[] {0, 1, 2, 3, 4}, len, offset),
                () => PlayPattern(floor, Patterns.HalvesHoriz2, new[] {4, 3, 2, 1, 0}, len, offset)
            }[NextPattern(4)];
            play.Invoke();
        }
    }
}
