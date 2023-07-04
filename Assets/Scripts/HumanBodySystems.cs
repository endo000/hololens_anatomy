using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class HumanBodySystems : MonoBehaviour
{
    [SerializeField] private GameObject system;
    [SerializeField] private GameObject systemsParent;
    [SerializeField] private Interactable interactableScript;

    private void Update()
    {
        interactableScript.IsToggled = system.activeSelf;
    }

    public void ToggleSystem()
    {
        HideOtherSystems();
        var isActive = !system.activeSelf;
        system.SetActive(isActive);
        interactableScript.IsToggled = isActive;
    }

    private void HideOtherSystems()
    {
        var parentTransform = systemsParent.transform;
        for (var i = 0; i < parentTransform.childCount; i++)
        {
            parentTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
}