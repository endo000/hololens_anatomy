using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CreateParentObjectsForCollections : MonoBehaviour
{
    [SerializeField] private List<string> collectionNames;

    public void UpdateObjects()
    {
        var collectionToObject = new Dictionary<string, GameObject>();
        var filteredObjects = new GameObject("Filtered");

        var children = new List<GameObject>();
        for (var i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }

        foreach (var child in children)
        {
            var checkCollection = BelongsToCollection(child);
            if (checkCollection != string.Empty)
            {
                Debug.Log($"{child.name} belongs to {checkCollection}");
                if (!collectionToObject.TryGetValue(checkCollection, out var collectionObject))
                {
                    collectionObject = new GameObject(checkCollection)
                    {
                        transform =
                        {
                            parent = filteredObjects.transform
                        }
                    };
                    collectionToObject.Add(checkCollection, collectionObject);
                }

                child.transform.parent = collectionObject.transform;
            }
            else
            {
                Debug.LogWarning($"{child.name} doesn't belong to any collection");
            }
        }
    }

    private string BelongsToCollection(GameObject gameObject)
    {
        foreach (var collectionName in collectionNames)
        {
            if (gameObject.name.StartsWith(collectionName))
            {
                return collectionName;
            }
        }

        return string.Empty;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CreateParentObjectsForCollections))]
public class CreateParentObjectsForCollections_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (CreateParentObjectsForCollections)target;
        if (GUILayout.Button("Move to parent objects"))
        {
            script.UpdateObjects();
        }
    }
}
#endif