using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanLayerController : MonoBehaviour
{
    public List<GameObject> humanOrgans = default;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var organ in humanOrgans)
        {
            // organ.SetActive(humanOrgans.First() == organ);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleOrgan(int index)
    {
        GameObject organ = humanOrgans[index];
        organ.SetActive(!organ.activeSelf);
    }

    public void StartAnimation(int index)
    {
        GameObject organ = humanOrgans[index];
        Animator animation = organ.GetComponent<Animator>();
        animation.Play("ArmAnimation");
    }
}