using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMenuController : MonoBehaviour
{
    public GameObject mainButtonCollection = default;
    public GameObject bodyButtonCollection = default;
    public GameObject heartButtonCollection = default;
    public GameObject armButtonCollection = default;

    public GameObject buttonReturn = default;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void handleBodyMenuButton()
    {
        mainButtonCollection.SetActive(false);
        bodyButtonCollection.SetActive(true);
        buttonReturn.SetActive(true);
    }

    public void handleHeartMenuButton()
    {
        mainButtonCollection.SetActive(false);
        heartButtonCollection.SetActive(true);
        buttonReturn.SetActive(true);
    }

    public void handleArmMenuButton()
    {
        mainButtonCollection.SetActive(false);
        armButtonCollection.SetActive(true);
        buttonReturn.SetActive(true);
    }

    public void handleReturnMenuButton()
    {
        if (bodyButtonCollection != null)
            bodyButtonCollection.SetActive(false);
        if (heartButtonCollection != null)
            heartButtonCollection.SetActive(false);
        if (armButtonCollection != null)
            armButtonCollection.SetActive(false);
        
        mainButtonCollection.SetActive(true);
        buttonReturn.SetActive(false);
    }
}