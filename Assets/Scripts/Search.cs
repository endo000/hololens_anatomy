using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

public class Search : MonoBehaviour
{
    [SerializeField] private TMP_Text searchText;
    [SerializeField] private List<GameObject> systemsToSearch;
    [SerializeField] private int searchResultLimit;

    [SerializeField] private bool testSearch;
    [SerializeField] private string testSearchText;

    [SerializeField] private GameObject searchResultButtonPrefab;
    [SerializeField] private GameObject searchResultCollection;

    private TouchScreenKeyboard keyboard;
    private Dictionary<string, List<GameObject>> systemsAndElements;

    private GridObjectCollection gridObjectCollection;
    private Vector3 prefabStartPosition;

    private string searchQuery;
    private bool runSearch;

    private void Start()
    {
        gridObjectCollection = searchResultCollection.GetComponent<GridObjectCollection>();

        prefabStartPosition = searchResultButtonPrefab.transform.position;

        systemsAndElements = new Dictionary<string, List<GameObject>>();
        foreach (var system in systemsToSearch)
        {
            systemsAndElements.Add(system.name, new List<GameObject>());
            var systemTransform = system.transform;
            for (var i = 0; i < systemTransform.childCount; ++i)
            {
                var child = systemTransform.GetChild(i);
                systemsAndElements[system.name].Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if (testSearch && searchQuery != testSearchText)
        {
            searchQuery = testSearchText;
            searchText.text = searchQuery;
            runSearch = true;
        }

        if (keyboard != null && searchQuery != keyboard.text)
        {
            searchQuery = keyboard.text;
            searchText.text = searchQuery;
            runSearch = true;
        }

        if (runSearch)
        {
            var searchResult = ProcessSearch();
            FillCollection(searchResult);
            runSearch = false;
        }
    }

    public void OnClick()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    private List<GameObject> ProcessSearch()
    {
        var result = new List<GameObject>();
        if (searchQuery == string.Empty && searchResultLimit <= 0)
        {
            return result;
        }

        foreach (var entry in systemsAndElements)
        {
            if (CompareStrings(entry.Key, searchQuery))
            {
                result.AddRange(entry.Value.GetRange(0, Math.Min(entry.Value.Count, searchResultLimit)));
                if (result.Count >= searchResultLimit)
                {
                    return result.Distinct().ToList();
                }
            }
        }

        foreach (var entry in systemsAndElements)
        {
            foreach (var element in entry.Value)
            {
                if (CompareStrings(element.name, searchQuery))
                {
                    result.Add(element);
                    if (result.Count >= searchResultLimit)
                    {
                        return result.Distinct().ToList();
                    }
                }
            }
        }

        return result.Distinct().ToList();
    }

    private void FillCollection(List<GameObject> searchResult)
    {
        var collectionCount = gridObjectCollection.transform.childCount;

        for (var i = 0; i < searchResult.Count; i++)
        {
            var searchEntry = searchResult[i];
            GameObject searchButton;
            if (i < collectionCount)
            {
                var child = gridObjectCollection.transform.GetChild(i);
                searchButton = child.gameObject;
                UpdateSearchButtonConfig(searchButton, searchEntry);
            }
            else
            {
                var newPosition = prefabStartPosition;
                newPosition.y = -1 * (i + 0.5f) * gridObjectCollection.CellHeight;

                searchButton = Instantiate(searchResultButtonPrefab, newPosition, Quaternion.identity,
                    searchResultCollection.transform);

                UpdateSearchButtonConfig(searchButton, searchEntry);
            }

            searchButton.SetActive(true);
        }

        for (var i = 0; i < collectionCount - searchResult.Count; i++)
        {
            gridObjectCollection.transform.GetChild(i + searchResult.Count).gameObject.SetActive(false);
        }
    }

    private bool CompareStrings(string str1, string str2)
    {
        var compareStr1 = str1.Replace("_", " ").ToLower();
        var compareStr2 = str2.Replace("_", " ").ToLower();
        return compareStr1.Contains(compareStr2) || compareStr2.Contains(compareStr1);
    }

    private string PrettyString(string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower().Replace("_", " "));
    }

    private void UpdateSearchButtonConfig(GameObject objectToConfigure, GameObject searchObject)
    {
        var config = objectToConfigure.GetComponent<SearchResultButtonConfig>();
        config.ElementNameText = PrettyString(searchObject.name);
        config.BodySystemText = PrettyString(searchObject.transform.parent.name);
    }
}