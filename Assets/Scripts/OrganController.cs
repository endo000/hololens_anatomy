using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrganController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToggleAnimation(AnimationClip animationClip)
    {
        gameObject.SetActive(true);
        
        if (animator.enabled && animator.GetCurrentAnimatorStateInfo(0).IsName(animationClip.name))
        {
            animator.enabled = false;
            return;
        }

        animator.enabled = true;
        animator.Play(animationClip.name);
    }
}