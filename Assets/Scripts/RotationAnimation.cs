using System;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [Range(-20, 20)] [SerializeField] private float animationSpeed = 1;

    private bool isToggled;

    private void Update()
    {
        if (isToggled)
        {
            PlayAnimation(Time.deltaTime);
        }
    }

    public void Toggle()
    {
        isToggled = !isToggled;
    }

    private void PlayAnimation(float deltaTime)
    {
        var deltaAngle = animationSpeed * deltaTime * 360;
        
        var newAngle = transform.rotation.eulerAngles.y + deltaAngle;

        transform.rotation = Quaternion.Euler(0, newAngle, 0);
    }
}