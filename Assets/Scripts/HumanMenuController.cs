using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMenuController : MonoBehaviour
{
    public GameObject mainButtonCollection = default;
    public List<GameObject> menuButtonCollection = default;

    public GameObject buttonReturn = default;
    
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleMenuButton(int index)
    {
        currentIndex = index;
        
        mainButtonCollection.SetActive(false);
        menuButtonCollection[index].SetActive(true);
        buttonReturn.SetActive(true);
    }
    
    public void HandleReturnMenuButton()
    {
        menuButtonCollection[currentIndex].SetActive(false);
        mainButtonCollection.SetActive(true);
        buttonReturn.SetActive(false);
    }
}