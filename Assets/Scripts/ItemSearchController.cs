using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script handles the search functionality
/// It allows users to search for specific items or elements within predefined systems and display the search results.
/// </summary>
public class ItemSearchController : MonoBehaviour
{
    [Header("Search Settings")] [Tooltip("The TextMeshPro text component used for search input.")] [SerializeField]
    private TMP_Text searchTextBox;

    [Tooltip("The list of GameObjects representing the systems that can be searched.")] [SerializeField]
    private List<GameObject> searchableSystems;

    [Range(1, 20)] [Tooltip("The maximum number of items to display in the search results.")] [SerializeField]
    private int maxDisplayedItems;

    [Header("Simulate search")]
    [Tooltip("Enables test search mode to use the test search query for debugging.")]
    [SerializeField]
    private bool enableTestSearch;

    [Tooltip("The test search query used for debugging when test search is enabled.")] [SerializeField]
    private string testSearchQuery;

    [Header("UI Elements")]
    [Tooltip("The prefab used to represent a found item in the search results.")]
    [SerializeField]
    private GameObject foundItemPrefab;

    [Tooltip("The collection where found item buttons will be displayed.")] [SerializeField]
    private GameObject foundItemCollection;

    [Header("Events")] [Tooltip("Event invoked when a search result item is selected.")] [SerializeField]
    private UnityEvent onItemSelected;

    [Tooltip("Script to toggle or show only one object.")] [SerializeField]
    private InspectObjectToggler inspectObjectToggler;

    private TouchScreenKeyboard touchScreenKeyboard;
    private Dictionary<string, List<GameObject>> systemElementsDictionary;

    private GridObjectCollection gridObjectCollection;
    private Vector3 itemPrefabStartPosition;

    private string currentSearchQuery;
    private bool isPerformingSearch;

    private void Start()
    {
        gridObjectCollection = foundItemCollection.GetComponent<GridObjectCollection>();

        itemPrefabStartPosition = foundItemPrefab.transform.position;

        systemElementsDictionary = new Dictionary<string, List<GameObject>>();

        // Populate the dictionary with systems and their child elements
        foreach (var system in searchableSystems)
        {
            systemElementsDictionary.Add(system.name, new List<GameObject>());
            var systemTransform = system.transform;
            for (var i = 0; i < systemTransform.childCount; ++i)
            {
                var child = systemTransform.GetChild(i);
                systemElementsDictionary[system.name].Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        // Check for test search query changes and trigger search
        if (enableTestSearch && currentSearchQuery != testSearchQuery)
        {
            currentSearchQuery = testSearchQuery;
            searchTextBox.text = currentSearchQuery;
            isPerformingSearch = true;
        }

        // Check for on-screen keyboard input changes and trigger search
        if (touchScreenKeyboard != null && currentSearchQuery != touchScreenKeyboard.text)
        {
            currentSearchQuery = touchScreenKeyboard.text;
            searchTextBox.text = currentSearchQuery;
            isPerformingSearch = true;
        }

        // Perform the search if needed
        if (isPerformingSearch)
        {
            var searchResult = FindMatchingItems();
            DisplayFoundItems(searchResult);
            isPerformingSearch = false;
        }
    }

    /// <summary>
    /// Opens the on-screen keyboard for search input.
    /// </summary>
    public void OpenScreenKeyboard()
    {
        touchScreenKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    /// <summary>
    /// Finds items that match the current search query in the defined systems.
    /// </summary>
    /// <returns>A list of GameObjects representing the search results.</returns>
    private List<GameObject> FindMatchingItems()
    {
        var result = new List<GameObject>();
        if (currentSearchQuery == string.Empty && maxDisplayedItems <= 0)
        {
            return result;
        }

        // Search for matching systems
        foreach (var entry in systemElementsDictionary)
        {
            if (CompareStrings(entry.Key, currentSearchQuery))
            {
                result.AddRange(entry.Value.GetRange(0, Math.Min(entry.Value.Count, maxDisplayedItems)));
                if (result.Count >= maxDisplayedItems)
                {
                    return result.Distinct().ToList();
                }
            }
        }

        // Search for matching items by their names
        foreach (var entry in systemElementsDictionary)
        {
            foreach (var element in entry.Value)
            {
                if (CompareStrings(element.name, currentSearchQuery))
                {
                    result.Add(element);
                    if (result.Count >= maxDisplayedItems)
                    {
                        return result.Distinct().ToList();
                    }
                }
            }
        }

        return result.Distinct().ToList();
    }

    /// <summary>
    /// Displays the found items in the UI and hides the rest of the items.
    /// </summary>
    /// <param name="searchResult">The list of GameObjects representing the search results.</param>
    private void DisplayFoundItems(List<GameObject> searchResult)
    {
        var collectionCount = gridObjectCollection.transform.childCount;

        // Display found items
        for (var i = 0; i < searchResult.Count; i++)
        {
            var foundItem = searchResult[i];
            GameObject searchButton;
            // Reuse existing UI element or create a new one for the found item
            if (i < collectionCount)
            {
                var child = gridObjectCollection.transform.GetChild(i);
                searchButton = child.gameObject;
                ConfigureItemDisplay(searchButton, foundItem);
            }
            else
            {
                var itemPosition = itemPrefabStartPosition;
                itemPosition.y = -1 * (i + 0.5f) * gridObjectCollection.CellHeight;

                searchButton = Instantiate(foundItemPrefab, itemPosition, Quaternion.identity,
                    foundItemCollection.transform);

                ConfigureItemDisplay(searchButton, foundItem);
            }

            searchButton.SetActive(true);
        }

        // Hide unused UI elements
        for (var i = 0; i < collectionCount - searchResult.Count; i++)
        {
            gridObjectCollection.transform.GetChild(i + searchResult.Count).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Compares two strings while ignoring case and underscores, for case-insensitive and more flexible matching.
    /// </summary>
    /// <param name="str1">The first string to compare.</param>
    /// <param name="str2">The second string to compare.</param>
    /// <returns>True if the strings match; otherwise, false.</returns>
    private bool CompareStrings(string str1, string str2)
    {
        var compareStr1 = str1.Replace("_", " ").ToLower();
        var compareStr2 = str2.Replace("_", " ").ToLower();
        return compareStr1.Contains(compareStr2) || compareStr2.Contains(compareStr1);
    }

    /// <summary>
    /// Formats a string for display by converting it to title case and replacing underscores with spaces.
    /// </summary>
    /// <param name="input">The input string to format.</param>
    /// <returns>The formatted string.</returns>
    private string FormatStringForDisplay(string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower().Replace("_", " "));
    }

    /// <summary>
    /// Configures the display of a UI item for a found object.
    /// </summary>
    /// <param name="itemToDisplay">The UI item to configure.</param>
    /// <param name="foundItemObject">The found object to display.</param>
    private void ConfigureItemDisplay(GameObject itemToDisplay, GameObject foundItemObject)
    {
        var config = itemToDisplay.GetComponent<SearchResultButtonConfig>();
        config.ElementNameText = FormatStringForDisplay(foundItemObject.name);
        config.BodySystemText = FormatStringForDisplay(foundItemObject.transform.parent.name);
        config.OnClick(() =>
        {
            inspectObjectToggler.ShowObject(foundItemObject);
            onItemSelected.Invoke();
        });
    }
}