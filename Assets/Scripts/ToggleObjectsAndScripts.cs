using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Provides functionality to toggle the active state of GameObjects and enable/disable MonoBehaviour scripts.
/// </summary>
public class ToggleObjectsAndScripts : MonoBehaviour
{
    [Tooltip("The list of GameObjects to enable/disable on toggle.")]
    [SerializeField]
    private List<GameObject> objectsToToggle;

    [Tooltip("The list of MonoBehaviour scripts to enable/disable on toggle.")]
    [SerializeField]
    private List<MonoBehaviour> scriptsToToggle;

    /// <summary>
    /// Toggles the active state of GameObjects and enables/disables MonoBehaviour scripts.
    /// </summary>
    public void Toggle()
    {
        // Toggle GameObjects
        foreach (var obj in objectsToToggle)
        {
            obj.SetActive(!obj.activeSelf);
        }

        // Toggle MonoBehaviour scripts
        foreach (var script in scriptsToToggle)
        {
            script.enabled = !script.enabled;
        }
    }
}