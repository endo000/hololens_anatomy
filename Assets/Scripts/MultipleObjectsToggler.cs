using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class MultipleObjectsToggler : MonoBehaviour
{
    [SerializeField] private bool visibility;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private Interactable interactable;

    private void Start()
    {
        SetObjects(visibility);
    }

    private void Update()
    {
        interactable.IsToggled = visibility;
    }

    public void ToggleObjects()
    {
        visibility = !visibility;
        SetObjects(visibility);
    }

    private void SetObjects(bool value)
    {
        foreach (var o in objects)
        {
            o.SetActive(value);
        }
    }
}
