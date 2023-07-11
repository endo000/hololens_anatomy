using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SearchResultButtonConfig : MonoBehaviour
{
    [SerializeField] private TMP_Text elementNameLabel;

    [SerializeField] private TMP_Text bodySystemLabel;

    [SerializeField] private Interactable interactableScript;

    [SerializeField] private string elementNameText;

    [SerializeField] private string bodySystemText;

    public string ElementNameText
    {
        get => elementNameText;
        set
        {
            elementNameText = value;
            if (elementNameLabel)
                elementNameLabel.text = value;
        }
    }

    public string BodySystemText
    {
        get => bodySystemText;
        set
        {
            bodySystemText = value;
            if (bodySystemLabel)
                bodySystemLabel.text = value;
        }
    }

    public void OnClick(UnityAction unityAction)
    {
        interactableScript.OnClick.AddListener(unityAction);
    }
}