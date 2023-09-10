using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Language
{
    Slovenian,
    English
}


public class ItemDescription : MonoBehaviour
{
    [Serializable]
    public class DescriptionText
    {
        public Language language;
        public TextAsset textAsset;
    }

    [SerializeField] private List<DescriptionText> descriptionTexts;

    private bool isDescriptionCached;
    private Dictionary<Language, string> cachedDescriptions;
    
    public Dictionary<Language, string> descriptions
    {
        get
        {
            // if (isDescriptionCached)
            // {
                // return cachedDescriptions;
            // }
            
            cachedDescriptions = descriptionTexts.ToDictionary(text => text.language, text => text.textAsset.text);
            isDescriptionCached = true;

            return cachedDescriptions;
        }
    }
}