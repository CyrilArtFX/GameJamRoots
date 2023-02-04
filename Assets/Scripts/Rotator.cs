using StarterAssets;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Grabber grabber;
    private FirstPersonController controller;

    void Awake()
    {
        grabber = GetComponent<Grabber>();
        controller = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if ( !grabber.Grabbable || !grabber.Grabbable.Rotable || !grabber.IsCurrentlyGrabbing ) return;

        float rotation = controller.Input.rotation;
        if ( rotation == 0.0f ) return;

        grabber.Grabbable.Rotable.Rotate( rotation * Time.deltaTime );
    }
}
