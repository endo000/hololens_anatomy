using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using UnityEngine;

public class ManipulationToggler : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private BoundsControl boundsControl;
    [SerializeField] private ObjectManipulator objectManipulator;
    [SerializeField] private NearInteractionGrabbable nearInteractionGrabbable;

    private void OnEnable()
    {
        boxCollider.enabled = true;
        boundsControl.enabled = true;
        objectManipulator.enabled = true;
        nearInteractionGrabbable.enabled = true;
    }

    private void OnDisable()
    {
        boxCollider.enabled = false;
        boundsControl.enabled = false;
        objectManipulator.enabled = false;
        nearInteractionGrabbable.enabled = false;
    }
}
