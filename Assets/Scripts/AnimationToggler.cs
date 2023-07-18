using System.Collections;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

/// <summary>
/// Provides functionality to toggle the play and stop of an animation using the Animator component.
/// </summary>
public class AnimationToggler : MonoBehaviour
{
    [Header("Animator and Animation")]
    [Tooltip("The Animator component responsible for playing the animation.")]
    [SerializeField]
    private Animator animator;

    [Tooltip("The AnimationClip to play when the animation is toggled.")] [SerializeField]
    private AnimationClip animationClip;

    [Header("Interactable")] [Tooltip("The Interactable component that represents the toggle state.")] [SerializeField]
    private Interactable interactable;

    private AnimationClip startClip;

    private void Start()
    {
        // Cache the starting clip to revert after stopping the animation
        startClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
    }

    /// <summary>
    /// Toggles the play and stop of the animation.
    /// </summary>
    public void Toggle()
    {
        if (IsAnimationPlaying())
        {
            StopAnimation();
            return;
        }

        StartCoroutine(PlayAnimation());
    }

    private bool IsAnimationPlaying()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animator.enabled && stateInfo.IsName(animationClip.name) && stateInfo.normalizedTime < 1.0f;
    }

    private void StopAnimation()
    {
        animator.Play(startClip.name, 0);
        StartCoroutine(EnsureClipChanged());
    }

    private IEnumerator EnsureClipChanged()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorClipInfo(0)[0].clip == startClip);
        animator.enabled = false;
        interactable.IsToggled = false;
    }

    private IEnumerator PlayAnimation()
    {
        animator.enabled = true;
        interactable.IsToggled = true;
        animator.Play(animationClip.name, 0, 0);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        StopAnimation();
    }
}