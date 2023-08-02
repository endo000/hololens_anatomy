using UnityEngine;

public class InspectObjectToggler : MonoBehaviour
{
    private GameObject activeObject;

    public void ShowObject(GameObject objectToShow)
    {
        if (activeObject)
        {
            activeObject.SetActive(false);
        }

        objectToShow.SetActive(true);
        activeObject = objectToShow;
    }
}