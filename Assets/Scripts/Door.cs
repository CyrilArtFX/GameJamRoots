using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve openingCurve;
    [SerializeField]
    private float openingTime;

    [SerializeField, ReadOnly]
    private Vector3 endRotation;
    private Vector3 startRotation;
    private Vector3 rotationDelta;

    private bool isOpen, isOpening;
    private float timeSinceOpening;

    private AudioSource audioSource;

    void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
        rotationDelta = endRotation - startRotation;
        audioSource = GetComponent<AudioSource>();
    }

    public void RecordEndRotation()
    {
        endRotation = transform.rotation.eulerAngles;
        endRotation.x -= endRotation.x > 180.0f ? 360.0f : 0.0f;
        endRotation.y -= endRotation.y > 180.0f ? 360.0f : 0.0f;
        endRotation.z -= endRotation.z > 180.0f ? 360.0f : 0.0f;
    }

    public void Open()
    {
        if (isOpen || isOpening) return;
        isOpening = true;
        audioSource.Play();
    }

    void FixedUpdate()
    {
        if(isOpening)
        {
            transform.rotation = Quaternion.Euler(startRotation + rotationDelta * openingCurve.Evaluate(timeSinceOpening / openingTime));

            timeSinceOpening += Time.deltaTime;
        }
    }
}
