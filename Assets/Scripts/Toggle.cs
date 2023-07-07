using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable;
    [SerializeField] private List<MonoBehaviour> scriptsToEnable;

    public void ToggleObjects()
    {
        foreach (var o in objectsToEnable)
        {
            o.SetActive(!o.activeSelf);
        }

        foreach (var script in scriptsToEnable)
        {
            script.enabled = !script.enabled;
        }
    }
}