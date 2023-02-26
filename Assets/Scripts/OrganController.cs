using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrganController : MonoBehaviour
{
    public AnimationClip actAnimation;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        SetupAnimation();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SetupAnimation()
    {
        if (actAnimation == null) return;

        animator.Play(actAnimation.name);
        animator.enabled = false;
    }

    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        SetupAnimation();
    }

    public void ToggleAnimation()
    {
        if (actAnimation == null) return;

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            SetupAnimation();
        }

        animator.enabled = !animator.enabled;
    }
}