using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class OrganButtonController : MonoBehaviour
{
    public GameObject organ;
    public bool isAnimation;

    private Interactable interactable;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        interactable = gameObject.GetComponent<Interactable>();

        if (isAnimation) animator = organ.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        interactable.IsToggled = !isAnimation ? organ.activeSelf : animator.enabled;
    }
}