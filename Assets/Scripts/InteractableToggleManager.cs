using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class InteractableToggleManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour toggleScript;
    [SerializeField] private Interactable interactable;
    private void Start()
    {
        if (toggleScript is not IToggle)
        {
            Debug.LogError("The toggle script must implement IToggle.");
        }

        if (!interactable)
        {
            interactable = GetComponent<Interactable>();
        }
    }

    private void Update()
    {
        interactable.IsToggled = ((IToggle) toggleScript).IsToggled;
    }
}
