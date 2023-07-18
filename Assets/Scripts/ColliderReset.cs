using UnityEngine;

/// <summary>
/// This script allows resetting the size of a BoxCollider to zero.
/// </summary>
public class ColliderReset : MonoBehaviour
{
    [Header("Collider Settings")]
    [Tooltip("Reference to the BoxCollider component to reset its size.")]
    [SerializeField]
    private BoxCollider boxCollider;

    /// <summary>
    /// Sets the size of the BoxCollider to zero.
    /// </summary>
    public void SetColliderSizeToZero()
    {
        boxCollider.size = Vector3.zero;
    }
}