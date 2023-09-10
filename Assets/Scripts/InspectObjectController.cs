using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using TMPro;
using UnityEngine;

public class InspectObjectController : MonoBehaviour
{
    [SerializeField] private ColliderReset colliderReset;
    [SerializeField] private BoundsControl boundsControl;
    
    [SerializeField] private GameObject descriptionObject;
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Language defaultLanguage;
    
    private GameObject activeObject;
    private ItemDescription activeItemDescription;

    private void SetDescriptionVisibility(bool value)
    {
        descriptionObject.SetActive(value);
    }

    public void ChangeLanguage(string language)
    {
        if (Language.TryParse(language, out Language newLanguage))
        {
            SetDescriptionText(newLanguage);
        }
    }
    
    private void SetDescriptionText(Language language)
    {
        if (activeItemDescription)
        {
            descriptionText.text = activeItemDescription.descriptions[language];
        }
        SetDescriptionVisibility(activeItemDescription);
    }
    
    public void ShowObject(GameObject objectToShow)
    {
        if (activeObject)
        {
            activeObject.SetActive(false);
        }
        
        activeItemDescription = objectToShow.GetComponent<ItemDescription>();
        SetDescriptionText(defaultLanguage);

        objectToShow.SetActive(true);
        activeObject = objectToShow;
        
        colliderReset.SetColliderSizeToZero();
        boundsControl.UpdateBounds();
    }
}