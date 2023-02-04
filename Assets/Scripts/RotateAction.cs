using StarterAssets;
using UnityEngine;

public class RotateAction : MonoBehaviour
{
    [SerializeField]
    private Grabber grabber;

    [SerializeField]
    private float rotationMultiplier;

    private FirstPersonController controller;

    void Awake()
    {
        controller = GetComponent<FirstPersonController>();
    }

    void FixedUpdate()
    {
        /*if (!grabber.IsCurrentlyGrabbing) return;
        if (!grabber.CurrentGrabbable.AssociatedRotator) return;

        grabber.CurrentGrabbable.AssociatedRotator.Rotate(controller.Input.rotation * rotationMultiplier);*/
    }
}
