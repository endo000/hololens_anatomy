using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HumanMenuController : MonoBehaviour
{
    public GameObject mainButtonCollection = default;
    public List<GameObject> menuButtonCollection = default;

    public GameObject buttonReturn = default;
    public GameObject buttonPin = default;

    public GameObject backplateQuad = default;

    private int currentIndex = 0;

    public void HandleMenuButton(int index)
    {
        currentIndex = index;

        mainButtonCollection.SetActive(false);
        menuButtonCollection[index].SetActive(true);
        buttonReturn.SetActive(true);

        AdjustBackplateScale(menuButtonCollection[index]);
    }

    public void HandleReturnMenuButton()
    {
        menuButtonCollection[currentIndex].SetActive(false);
        mainButtonCollection.SetActive(true);
        buttonReturn.SetActive(false);

        AdjustBackplateScale(mainButtonCollection);
    }

    private void AdjustBackplateScale(GameObject buttonCollection)
    {
        var childCount = buttonCollection.transform.childCount;

        var newScale = backplateQuad.transform.localScale;
        newScale.x = 0.032f * (childCount + 1);
        backplateQuad.transform.localScale = newScale;

        GameObject[] sideButtons = { buttonReturn, buttonPin };
        foreach (var button in sideButtons)
        {
            var newButtonPosition = button.transform.position;
            newButtonPosition.x = backplateQuad.transform.position.x + (newScale.x + 0.032f) / 2;
            button.transform.position = newButtonPosition;
        }
    }
}