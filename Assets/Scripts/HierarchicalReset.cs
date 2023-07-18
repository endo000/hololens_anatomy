using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allows for hierarchical resetting of child objects to their original positions and rotations.
/// </summary>
public class HierarchicalReset : MonoBehaviour
{
    private readonly Dictionary<Transform, Tuple<Vector3, Quaternion>> originalTransforms = new();

    private void Start()
    {
        SaveOriginalTransformsRecursively(transform);
    }

    /// <summary>
    /// Saves the original positions and rotations of child objects recursively.
    /// </summary>
    /// <param name="objectTransform">The transform of the parent object whose children will have their transforms saved.</param>
    private void SaveOriginalTransformsRecursively(Transform objectTransform)
    {
        foreach (Transform child in objectTransform)
        {
            originalTransforms.Add(child, new Tuple<Vector3, Quaternion>(child.position, child.rotation));
            SaveOriginalTransformsRecursively(child);
        }
    }

    /// <summary>
    /// Resets child objects to their original positions and rotations.
    /// </summary>
    public void DoReset()
    {
        foreach (var entry in originalTransforms)
        {
            entry.Key.position = entry.Value.Item1;
            entry.Key.rotation = entry.Value.Item2;
        }
    }
}