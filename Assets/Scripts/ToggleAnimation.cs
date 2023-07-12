using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ToggleAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private Interactable interactable;

    private AnimationClip startClip;
    private bool isToggled;

    private bool IsToggled
    {
        get => isToggled;
        set
        {
            if (isToggled == value) return;

            if (value)
            {
                animator.enabled = true;
                animator.Play(animationClip.name, 0, 0);
            }
            else
            {
                animator.Play(startClip.name, 0);
                animator.enabled = false;
            }
        }
    }

    private void Start()
    {
        startClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
    }

    private void Update()
    {
        var isAnimationOn = IsAnimationOn();
        interactable.IsToggled = isAnimationOn;
        isToggled = isAnimationOn;
    }

    public void Toggle()
    {
        IsToggled = !IsToggled;
    }

    private bool IsAnimationOn()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animator.enabled && stateInfo.IsName(animationClip.name) && stateInfo.normalizedTime < 1.0f;
    }
}