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

        var oldRotation = transform.rotation.eulerAngles;

        var newAngle = oldRotation.y + deltaAngle;

        transform.rotation = Quaternion.Euler(oldRotation.x, newAngle, oldRotation.z);
    }
}