using UnityEngine;

namespace DanceFloorOsu
{
    public class DanceSquare
    {
        private int _x;
        private int _y;

        public float Hue { get; private set; }
        public float Sat { get; private set; }
        public float Val { get; private set; }

        public float TimeSet { get; private set; }

        public bool ValMode;

        public DanceSquare(int x, int y)
        {
            _x = x;
            _y = y;

            Hue = 0;
            Sat = 0;
            Val = 0.2f;
            ValMode = true;
        }

        public void Update()
        {
            Hue = (Hue + Random.Range(0.03f, 0.05f) * Time.deltaTime) % 1;
            if (ValMode)
            {
                Val = Mathf.MoveTowards(Val, 0.2f, Time.deltaTime);
            }
            else
            {
                Sat = Mathf.MoveTowards(Sat, 1, Time.deltaTime * 2);
            }
        }

        public void RandomHue()
        {
            TimeSet = Time.time;
            Hue = Random.value;
            Sat = Mathf.MoveTowards(Sat, 0, 0.3f);
            Val = 0.9f;
            ValMode = false;
        }

        public void SetHue(float x)
        {
            TimeSet = Time.time;
            Hue = x;
        }
        public void SetSat(float x)
        {
            TimeSet = Time.time;
            Sat = x;
        }
        public void SetVal(float x)
        {
            TimeSet = Time.time;
            Val = x;
        }

        public void SetColor(Color c)
        {
            TimeSet = Time.time;
            float cHue, cSat, cVal;
            Color.RGBToHSV(c, out cHue, out cSat, out cVal);
            Hue = cHue;
            Sat = Mathf.MoveTowards(Sat, 0, 0.3f);
            Val = 0.9f;
            ValMode = false;
        }

        public Color GetColor()
        {
            return Color.HSVToRGB(Hue, Sat, Val);
        }
    }
}
