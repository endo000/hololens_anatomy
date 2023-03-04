using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class OrganButtonController : MonoBehaviour
{
    public GameObject organ;
    public AnimationClip animationClip;

    private Interactable interactable;
    private Animator animator;

    private bool isAnimation;

    // Start is called before the first frame update
    void Start()
    {
        interactable = gameObject.GetComponent<Interactable>();
        isAnimation = animationClip != null;

        if (isAnimation) animator = organ.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimation)
        {
            interactable.IsToggled =
                animator.enabled && animator.GetCurrentAnimatorStateInfo(0).IsName(animationClip.name);
        }
        else
        {
            interactable.IsToggled = organ.activeSelf;
        }
    }
}