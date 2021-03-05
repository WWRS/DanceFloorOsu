using UnityEngine;

namespace DanceFloorOsu
{
    public class DanceSquare
    {
        private int _x;
        private int _y;

        public float Hue { get; private set; }
        public float Sat { get; private set; }

        public float TimeSet { get; private set; }

        public DanceSquare(int x, int y)
        {
            _x = x;
            _y = y;

            Hue = Random.value;
            Sat = 1;
        }

        public void Update()
        {
            Hue = (Hue + Random.Range(0.05f, 0.1f) * Time.deltaTime) % 1;
            Sat = Mathf.MoveTowards(Sat, 1, Time.deltaTime);
        }

        public void RandomHue()
        {
            TimeSet = Time.time;
            Hue = Random.value;
            Sat = Mathf.MoveTowards(Sat, 0, 0.3f);
        }

        public void SetSat(float x)
        {
            TimeSet = Time.time;
            Sat = x;
        }

        public void SetHue(float x)
        {
            TimeSet = Time.time;
            Hue = x;
        }

        public void SetColor(Color c)
        {
            TimeSet = Time.time;
            float cHue, cSat, cVal;
            Color.RGBToHSV(c, out cHue, out cSat, out cVal);
            Hue = cHue;
            Sat = Mathf.MoveTowards(Sat, 0, 0.3f);
        }

        public Color GetColor()
        {
            return Color.HSVToRGB(Hue, Sat, 0.8f);
        }
    }
}
