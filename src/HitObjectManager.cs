using System;
using System.Collections.Generic;
using OsuParsers.Beatmaps;
using OsuParsers.Beatmaps.Objects;

public class HitObjectManager
{
    private readonly Queue<HitObject> _objects;
    private HitObject _lastObject;

    public HitObjectManager(Beatmap beatmap)
    {
        _objects = new Queue<HitObject>(beatmap.HitObjects);
    }

    public void UpdateObjects(Action<HitObject, float> callback, float audioTime)
    {
        if (_objects == null || _objects.Count == 0)
        {
            return;
        }

        while (ShouldAdvance(audioTime))
        {
            HitObject nextObject = _objects.Dequeue();

            float speed = HitObjectUtils.Speed(_lastObject, nextObject);
            callback.Invoke(nextObject, speed);

            _lastObject = nextObject;
        }
    }


    private bool ShouldAdvance(float audioTime)
    {
        if (_objects == null || _objects.Count == 0)
        {
            return false;
        }

        HitObject nextObject = _objects.Peek();

        return (nextObject.StartTime <= audioTime);
    }

    public int NextGap()
    {
        if (_objects.Count > 0)
        {
            return HitObjectUtils.TimeGap(_lastObject, _objects.Peek());
        }
        else
        {
            return -1;
        }
    }
}
