using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Configures a search result button with element and body system labels, and an action to execute on click.
/// </summary>
public class SearchResultButtonConfig : MonoBehaviour
{
    [Header("UI References")] [Tooltip("The TMP_Text component displaying the element name.")] [SerializeField]
    private TMP_Text elementNameLabel;

    [Tooltip("The TMP_Text component displaying the body system name.")] [SerializeField]
    private TMP_Text bodySystemLabel;

    [Tooltip("The Interactable script to handle click events.")] [SerializeField]
    private Interactable interactableScript;

    [Header("Text Settings")] [Tooltip("The text to display as the element name.")] [SerializeField]
    private string elementNameText;

    [Tooltip("The text to display as the body system name.")] [SerializeField]
    private string bodySystemText;

    /// <summary>
    /// Gets or sets the text to display as the element name.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the text to display as the body system name.
    /// </summary>
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

    /// <summary>
    /// Adds a click event listener to the interactable script.
    /// </summary>
    /// <param name="unityAction">The action to execute on click.</param>
    public void OnClick(UnityAction unityAction)
    {
        interactableScript.OnClick.RemoveAllListeners();
        interactableScript.OnClick.AddListener(unityAction);
    }
}