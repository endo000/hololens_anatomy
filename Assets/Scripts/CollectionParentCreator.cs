using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/// <summary>
/// This script is responsible for creating parent objects for collections of objects based on their names.
/// It groups child objects under parent objects according to specified collection names and adjusts their names for better organization.
/// </summary>
public class CollectionParentCreator : MonoBehaviour
{
    [Tooltip("The names of the collections that will be created as parent objects.")] [SerializeField]
    private List<string> collectionNames;

    [Tooltip("The original model containing the child objects to be organized into collections.")] [SerializeField]
    private GameObject originalModel;

    [Tooltip("The parent object under which the new collection parent objects will be created.")] [SerializeField]
    private GameObject filterParent;

    /// <summary>
    /// Creates parent objects for the collections and groups the child objects under them.
    /// </summary>
    public void CreateParentObjects()
    {
        var collectionToObject = new Dictionary<string, GameObject>();

        var children = new List<GameObject>();

        // Get the list of child objects from the original model
        for (var i = 0; i < originalModel.transform.childCount; i++)
        {
            children.Add(originalModel.transform.GetChild(i).gameObject);
        }

        foreach (var child in children)
        {
            // Determine the collection name to which the child object belongs
            var collectionName = GetCollectionName(child);
            if (child.name.Contains("combined"))
            {
                // If the child is a combined object, use its name as the collection name
                collectionName = child.name;
            }
            else if (collectionName == string.Empty)
            {
                // If the child doesn't belong to any collection, skip it and log a warning
                Debug.LogWarning($"{child.name} doesn't belong to any collection");
                continue;
            }

            Debug.Log($"{child.name} belongs to {collectionName}");

            // Create or retrieve the collection parent object
            if (!collectionToObject.TryGetValue(collectionName, out var collectionObject))
            {
                collectionObject = new GameObject(collectionName)
                {
                    transform =
                    {
                        parent = filterParent.transform
                    }
                };
                collectionToObject.Add(collectionName, collectionObject);
            }

            // Update the child object's name and parent it to the collection object
            if (!child.name.Contains("combined"))
            {
                child.name = child.name[(collectionName.Length + 1)..];

                var words = child.name.Split("_");
                for (var i = 0; i < words.Length; i++)
                {
                    words[i] = words[i] switch
                    {
                        "L" => "left",
                        "R" => "right",
                        _ => words[i]
                    };
                }

                child.name = string.Join("_", words);
            }

            child.transform.parent = collectionObject.transform;
        }
    }

    /// <summary>
    /// Gets the collection name to which the given game object belongs.
    /// </summary>
    /// <param name="objectToCheck">The game object to check.</param>
    /// <returns>The name of the collection if the object belongs to one, otherwise an empty string.</returns>
    private string GetCollectionName(GameObject objectToCheck)
    {
        foreach (var collectionName in collectionNames)
        {
            if (objectToCheck.name.StartsWith(collectionName))
            {
                return collectionName;
            }
        }

        return string.Empty;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CollectionParentCreator))]
public class CreateParentObjectsForCollections_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (CollectionParentCreator)target;
        if (GUILayout.Button("Move to parent objects"))
        {
            script.CreateParentObjects();
        }
    }
}
#endif