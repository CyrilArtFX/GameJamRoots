using StarterAssets;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float fastRotationMultiplier = 2.0f;
    [SerializeField]
    private float accelerationTime = 1.0f;
    [SerializeField]
    private float accelerationSpeed = 5.0f;

    private float currentAccelerationTime = 0.0f;
    private float currentSpeedMultiplier = 1.0f;

    private Grabber grabber;
    private FirstPersonController controller;

    void Awake()
    {
        grabber = GetComponent<Grabber>();
        controller = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        float rotation = controller.Input.rotation;
        if ( !grabber.Grabbable || !grabber.Grabbable.Rotable || !grabber.IsCurrentlyGrabbing || rotation == 0.0f ) 
        {
            currentAccelerationTime = 0.0f;
            currentSpeedMultiplier = 1.0f;
            return;
        }

        //  accelerate while holding for long time
        if ( ( currentAccelerationTime += Time.deltaTime ) >= accelerationTime )
        {
            currentSpeedMultiplier = Mathf.Lerp(currentSpeedMultiplier, fastRotationMultiplier, Time.deltaTime * accelerationSpeed);
        }

        grabber.Grabbable.Rotable.Rotate( rotation * currentSpeedMultiplier * Time.deltaTime );
    }
}
