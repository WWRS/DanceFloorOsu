using System.Numerics;
using OsuParsers.Beatmaps.Objects;

public static class HitObjectUtils
{
    public static float Distance(HitObject first, HitObject second)
    {
        return Vector2.Distance(first.Position, second.Position);
    }
    
    public static int TimeGap(HitObject first, HitObject second)
    {
        return  second.StartTime - first.EndTime;
    }

    public static int Length(HitObject obj)
    {
        return obj.EndTime - obj.StartTime;
    }

    public static float Speed(HitObject first, HitObject second)
    {
        if (first != null && second != null)
        {
            return Distance(first, second) / TimeGap(first, second);
        }
        else
        {
            return 0.5f;
        }
    }
}
