using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This script manages the visibility and interaction of human body systems.
/// It allows toggling the visibility of a specific system and hides other systems when one is active.
/// </summary>
public class BodySystemToggler : MonoBehaviour
{
    [Tooltip("The GameObject representing the specific human body system.")] [SerializeField]
    private GameObject system;

    [Tooltip("The parent GameObject that holds all human body systems.")] [SerializeField]
    private GameObject systemsParent;

    [Tooltip("The Interactable script that allows toggling the visibility of the system.")] [SerializeField]
    private Interactable interactable;

    private void Update()
    {
        // Update the Interactable's IsToggled property based on the system's active state
        interactable.IsToggled = system.activeSelf;
    }

    /// <summary>
    /// Toggles the visibility of the human body system and updates the Interactable's IsToggled property.
    /// </summary>
    public void ToggleSystem()
    {
        var isActive = !system.activeSelf;
        system.SetActive(isActive);
        interactable.IsToggled = isActive;

        // Uncomment the following line to hide other systems when one is active
        // HideOtherSystems();
    }

    /// <summary>
    /// Hides other human body systems that are not part of the allowed systems for the currently active host system.
    /// </summary>
    private void HideOtherSystems()
    {
        var parentTransform = systemsParent.transform;
        for (var i = 0; i < parentTransform.childCount; i++)
        {
            parentTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
}