using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchResultButtonConfig : MonoBehaviour
{
    [SerializeField] private TMP_Text elementNameLabel;

    [SerializeField] private TMP_Text bodySystemLabel;

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

    [SerializeField] private string elementNameText;

    [SerializeField] private string bodySystemText;
}