using System;
using DefaultNamespace;
using UnityEngine;

public class RotationAnimation : MonoBehaviour, IToggle
{
    [Range(-20, 20)] [SerializeField] private float animationSpeed = 1;

    public bool IsToggled { get; set; }

    private void Update()
    {
        if (IsToggled)
        {
            PlayAnimation(Time.deltaTime);
        }
    }

    public void Toggle()
    {
        IsToggled = !IsToggled;
    }

    public void TurnOff()
    {
        IsToggled = false;
    }

    private void PlayAnimation(float deltaTime)
    {
        var deltaAngle = animationSpeed * deltaTime * 360;

        var oldRotation = transform.rotation.eulerAngles;

        var newAngle = oldRotation.y + deltaAngle;

        transform.rotation = Quaternion.Euler(oldRotation.x, newAngle, oldRotation.z);
    }
}