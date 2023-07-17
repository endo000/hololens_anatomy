#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

public class AlignMeshes : MonoBehaviour
{
    [SerializeField] private GameObject pointToPlace;
    [SerializeField] private List<GameObject> parentObjectsToMove;
    [SerializeField] private string type;

    public void MoveChildren()
    {
        foreach (var parentObjectToMove in parentObjectsToMove)
        {
            for (var i = 0; i < parentObjectToMove.transform.childCount; i++)
            {
                MoveObject(parentObjectToMove.transform.GetChild(i).gameObject);
            }
        }
    }

    private void MoveObject(GameObject objectToMove)
    {
        var bounds = objectToMove.GetComponent<MeshFilter>().sharedMesh.bounds;

        var transformPoint = type switch
        {
            "min" => bounds.min,
            "max" => bounds.max,
            "size" => bounds.size,
            "center" => bounds.center,
            "max_center" => bounds.max,
            _ => new Vector3()
        };

        transformPoint.x -= bounds.extents.x;
        // transformPoint.z -= bounds.extents.z;

        var offset = objectToMove.transform.position - objectToMove.transform.TransformPoint(transformPoint);
        var newPosition = pointToPlace.transform.position + offset;
        // newPosition.z = bounds.min.z;
        var oldRotation = objectToMove.transform.rotation;
        oldRotation.y = 0;
        // oldRotation.x = 180;
        objectToMove.transform.rotation = oldRotation;
        objectToMove.transform.position = newPosition;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AlignMeshes))]
public class AlignMeshes_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (AlignMeshes)target;
        if (GUILayout.Button("Move to parent objects"))
        {
            script.MoveChildren();
        }
    }
}
#endif