using System.Collections.Generic;
using OsuParsers.Beatmaps;
using OsuParsers.Beatmaps.Objects;

namespace DanceFloorOsu
{
#warning If OsuParsers version is before 28 Feb 2021, TimingPoint#Inherited will be inverted. Current version of TimingManager uses the bug. If OsuParsers updates, fix this file!
    public class TimingManager
    {
        private Queue<TimingPoint> _timings;

        private TimingPoint _activeRed;
        private TimingPoint _activeGreen;

        public TimingManager(Beatmap beatmap)
        {
            _timings = new Queue<TimingPoint>(beatmap.TimingPoints);
            _activeRed = beatmap.TimingPoints.Find(h => h.Inherited);
        }

        public void UpdateTiming(int audioTime)
        {
            if (_timings == null || _timings.Count == 0)
            {
                return;
            }

            TimingPoint nextPoint = _timings.Peek();
            while (nextPoint.Offset <= audioTime)
            {
                _timings.Dequeue(); // current

                if (!nextPoint.Inherited)
                {
                    _activeGreen = nextPoint;
                }
                else
                {
                    _activeRed = nextPoint;
                    if (_activeGreen != null && _activeGreen.Offset < _activeRed.Offset)
                    {
                        _activeGreen = null;
                    }
                }

                if (_timings.Count == 0)
                {
                    return;
                }

                nextPoint = _timings.Peek();
            }
        }

        public float GetSV()
        {
            if (_activeGreen == null || _activeGreen.Offset < _activeRed.Offset)
            {
                return 1;
            }
            else
            {
                return (float) (-100 / _activeGreen.BeatLength);
            }
        }

        public float BeatLength()
        {
            return (float) _activeRed.BeatLength;
        }
    }
}
