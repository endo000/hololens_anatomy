using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animationClip;

    private AnimationClip startClip;

    private void Start()
    {
        startClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
    }

    public void Toggle()
    {
        if (animator.enabled && animator.GetCurrentAnimatorStateInfo(0).IsName(animationClip.name))
        {
            // animator.enabled = false;
            animator.Play(startClip.name);
            return;
        }

        // animator.enabled = true;
        animator.Play(animationClip.name);
    }
}