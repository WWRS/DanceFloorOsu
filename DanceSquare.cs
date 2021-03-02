using UnityEngine;

public class DanceSquare
{
    private int _x;
    private int _y;

    public float Hue;
    public float Sat;

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
        Hue = Random.value;
        Sat -= 0.2f;
    }

    public Color GetColor()
    {
        return Color.HSVToRGB(Hue, Sat, 0.9f);
    }
}
