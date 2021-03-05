using System;
using System.Collections.Generic;
using System.Linq;
using OsuParsers.Beatmaps;
using UnityEngine;

namespace DanceFloorOsu
{
    public class ColorManager
    {
        private readonly List<Color> _colors;
        private int _index = 0;

        public ColorManager(Beatmap beatmap)
        {
            _colors = beatmap.ColoursSection.ComboColours
                .Select(sysColor => new Color(sysColor.R, sysColor.G, sysColor.B)).ToList();
        }

        public void Advance(int n = 0)
        {
            if (n < 0)
                throw new ArgumentException("n must be non-negative");
            _index = (_index + n + 1) % _colors.Count;
        }

        public Color CurrentColor()
        {
            return _colors[_index];
        }
    }
}
