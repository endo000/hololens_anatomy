using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RemoveSystemNameFromElement : MonoBehaviour
{
    [SerializeField] private List<GameObject> systems;

    public void UpdateNames()
    {
        foreach (var system in systems)
        {
            var systemTransform = system.transform;
            for (var i = 0; i < systemTransform.childCount; ++i)
            {
                var child = systemTransform.GetChild(i);
                var childName = child.name;
                var prefix = $"{system.name}_";
                if (childName.StartsWith(prefix))
                {
                    child.name = childName[prefix.Length..];
                }
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RemoveSystemNameFromElement))]
public class RemoveSystemNameFromElement_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (RemoveSystemNameFromElement)target;
        if (GUILayout.Button("Remove system names from start"))
        {
            script.UpdateNames();
        }
    }
}
#endif
