#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Manages the alignment of child objects based on a reference point.
/// </summary>
public class MeshAlignmentManager : MonoBehaviour
{
    /// <summary>
    /// The types of alignment available for child objects.
    /// </summary>
    private enum AlignmentType
    {
        Min,
        Max,
        Size,
        Center,
        MaxCenter
    }

    [Header("Alignment Settings")] [Tooltip("The reference point to align child objects.")] [SerializeField]
    private GameObject alignmentReferencePoint;

    [Tooltip("The list of parent objects whose children will be aligned.")] [SerializeField]
    private List<GameObject> objectsToAlign;

    [Tooltip("The type of alignment to be applied to child objects.")] [SerializeField]
    private AlignmentType alignmentType;

    /// <summary>
    /// Aligns the child objects of the specified parent objects.
    /// </summary>
    public void AlignChildren()
    {
        foreach (var parentObjectToMove in objectsToAlign)
        {
            for (var i = 0; i < parentObjectToMove.transform.childCount; i++)
            {
                AlignObject(parentObjectToMove.transform.GetChild(i).gameObject);
            }
        }
    }

    /// <summary>
    /// Aligns the specified object based on the chosen alignment type.
    /// </summary>
    /// <param name="objectToAlign">The object to be aligned.</param>
    private void AlignObject(GameObject objectToAlign)
    {
        var bounds = objectToAlign.GetComponent<MeshFilter>().sharedMesh.bounds;

        var transformPoint = alignmentType switch
        {
            AlignmentType.Min => bounds.min,
            AlignmentType.Max => bounds.max,
            AlignmentType.Size => bounds.size,
            AlignmentType.Center => bounds.center,
            AlignmentType.MaxCenter => bounds.max,
            _ => new Vector3()
        };

        transformPoint.x -= bounds.extents.x;
        // transformPoint.z -= bounds.extents.z;

        var offset = objectToAlign.transform.position - objectToAlign.transform.TransformPoint(transformPoint);
        var newPosition = alignmentReferencePoint.transform.position + offset;
        // newPosition.z = bounds.min.z;
        var newRotation = objectToAlign.transform.rotation;
        newRotation.y = 0;
        // oldRotation.x = 180;
        objectToAlign.transform.rotation = newRotation;
        objectToAlign.transform.position = newPosition;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MeshAlignmentManager))]
public class MeshAlignmentManager_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (MeshAlignmentManager)target;
        if (GUILayout.Button("Align Children Objects"))
        {
            script.AlignChildren();
        }
    }
}
#endif