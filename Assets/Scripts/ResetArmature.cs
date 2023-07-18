using System;
using System.Collections.Generic;
using UnityEngine;

public class ResetArmature : MonoBehaviour
{
    private readonly Dictionary<Transform, Tuple<Vector3, Quaternion>> originalPositions = new();

    private void Start()
    {
        GetChildren(transform);
    }

    private void GetChildren(Transform objectTransform)
    {
        foreach (Transform child in objectTransform)
        {
            originalPositions.Add(child, new Tuple<Vector3, Quaternion>(child.position, child.rotation));
            GetChildren(child);
        }
    }

    public void DoReset()
    {
        foreach (var entry in originalPositions)
        {
            entry.Key.position = entry.Value.Item1;
            entry.Key.rotation = entry.Value.Item2;
        }
    }
}