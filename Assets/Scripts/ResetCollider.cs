using UnityEngine;

public class ResetCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    public void SizeToZero()
    {
        boxCollider.size = Vector3.zero;
    }
}