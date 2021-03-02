using System;
using System.Collections.Generic;
using UnityEngine;

public class DanceFloorUpdater : MonoBehaviour
{
    private static DanceFloorUpdater _instance;
    
    private static readonly SortedList<float, Action> Events = new SortedList<float, Action>(new DuplicateKeyComparer());

    public static void Init()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("DanceFloorUpdater");
            _instance = go.AddComponent<DanceFloorUpdater>();
        }
    }
    
    private void Update()
    {
        while (Events.Count > 0 && Time.time >= Events.Keys[0])
        {
            Events.Values[0].Invoke();
            Events.RemoveAt(0);
        }
    }

    public static void Register(float delay, Action call)
    {
        Events.Add(delay + Time.time, call);
    }
}

internal class DuplicateKeyComparer : IComparer<float>
{
    public int Compare(float x, float y)
    {
        return x < y ? -1 : 1;
    }
}
