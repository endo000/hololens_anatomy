using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using UnityEngine;

/// <summary>
/// Script that toggles the enabling/disabling of various manipulation components when the GameObject is enabled or disabled.
/// </summary>
public class ManipulationToggler : MonoBehaviour
{
    [Header("Components to Toggle")]
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private BoundsControl boundsControl;
    [SerializeField] private ObjectManipulator objectManipulator;
    [SerializeField] private NearInteractionGrabbable nearInteractionGrabbable;

    private void OnEnable()
    {
        EnableManipulationComponents(true);
    }

    private void OnDisable()
    {
        EnableManipulationComponents(false);
    }

    /// <summary>
    /// Enables or disables the manipulation components based on the input flag.
    /// </summary>
    /// <param name="enable">True to enable the components, false to disable them.</param>
    private void EnableManipulationComponents(bool enable)
    {
        boxCollider.enabled = enable;
        boundsControl.enabled = enable;
        objectManipulator.enabled = enable;
        nearInteractionGrabbable.enabled = enable;
    }
}
