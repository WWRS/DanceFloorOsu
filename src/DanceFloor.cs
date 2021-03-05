using UnityEngine;
using OsuParsers.Beatmaps;
using OsuParsers.Beatmaps.Objects;

public class DanceFloor
{
    private readonly AudioSource _musicSource;

    private TimingManager _timingManager;
    private HitObjectManager _hitObjectManager;

    private readonly DanceSquare[] _squares = new DanceSquare[100];

    public DanceFloor(AudioSource musicSource)
    {
        _musicSource = musicSource;

        DanceFloorUpdater.Init();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                _squares[10 * i + j] = new DanceSquare(i, j);
            }
        }
    }

    public void Load(Beatmap beatmap)
    {
        _timingManager = new TimingManager(beatmap);
        _hitObjectManager = new HitObjectManager(beatmap);
    }

    public void Update()
    {
        int audioTime = Mathf.RoundToInt(_musicSource.time * 1000);
        _timingManager?.UpdateTiming(audioTime);
        _hitObjectManager?.UpdateObjects(ObjectResponse, audioTime);
        UpdateSquares();
    }

    public Color[,] ColorGrid()
    {
        Color[,] output = new Color[10, 10];
        
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                output[i, j] = _squares[i * 10 + j].GetColor();
            }
        }

        return output;
    }

    public Color[] ColorArray()
    {
        Color[] output = new Color[100];

        for (int i = 0; i < 100; i++)
        {
            output[i] = _squares[i].GetColor();
        }

        return output;
    }

    private void UpdateSquares()
    {
        foreach (DanceSquare square in _squares)
        {
            square.Update();
        }
    }

    public void ObjectResponse(HitObject nextObject, float speed)
    {
        float beatLength = _timingManager.BeatLength();
        
        if (HitObjectUtils.Length(nextObject) >= beatLength / 2 - 0.002f)
        {
            FloorUtils.SmartPattern(this, nextObject, beatLength);
        }
        else if (_hitObjectManager.NextGap() >= beatLength * 3 - 0.002f)
        {
            FloorUtils.PlayPattern(this, Patterns.Droplet, 15, beatLength / 500f, 0,
                a => { foreach(int t in a) _squares[t].Sat = 0; });
        }
        else
        {
            int ct = (int) Mathf.Clamp(speed * 25, 10, 100);
            int[] changes = Patterns.Random(ct);
            ChangeFloor(changes);
        }
    }
    
    public void ChangeFloor(int[] indices)
    {
        Color c = _hitObjectManager.ColorManager.CurrentColor();
        foreach (int index in indices)
        {
            _squares[index].SetColor(c);
        }
    }
}
