using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class CreateParentObjectsForCollections : MonoBehaviour
{
    [SerializeField] private List<string> collectionNames;
    [SerializeField] private GameObject originalModel;
    [SerializeField] private GameObject filterParent;

    public void UpdateObjects()
    {
        var collectionToObject = new Dictionary<string, GameObject>();

        var children = new List<GameObject>();
        for (var i = 0; i < originalModel.transform.childCount; i++)
        {
            children.Add(originalModel.transform.GetChild(i).gameObject);
        }

        foreach (var child in children)
        {
            var checkCollection = BelongsToCollection(child);
            if (child.name.Contains("combined"))
            {
                checkCollection = child.name;
            }
            else if (checkCollection == string.Empty)
            {
                Debug.LogWarning($"{child.name} doesn't belong to any collection");
                continue;
            }

            Debug.Log($"{child.name} belongs to {checkCollection}");
            if (!collectionToObject.TryGetValue(checkCollection, out var collectionObject))
            {
                collectionObject = new GameObject(checkCollection)
                {
                    transform =
                    {
                        parent = filterParent.transform
                    }
                };
                collectionToObject.Add(checkCollection, collectionObject);
            }

            if (!child.name.Contains("combined"))
            {
                child.name = child.name[(checkCollection.Length + 1)..];

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