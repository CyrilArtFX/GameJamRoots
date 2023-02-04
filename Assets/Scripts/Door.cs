using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve openingCurve;
    [SerializeField]
    private float openingTime;

    [SerializeField, ReadOnly]
    private Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 positionDelta;

    private bool isOpen, isOpening;
    private float timeSinceOpening;

    void Awake()
    {
        startPosition = transform.position;
        positionDelta = endPosition - startPosition;
    }

    public void RecordEndPosition()
    {
        endPosition = transform.position;
    }

    public void Open()
    {
        if (isOpen || isOpening) return;
        isOpening = true;
    }

    void FixedUpdate()
    {
        if(isOpening)
        {
            transform.position = startPosition + positionDelta * openingCurve.Evaluate(timeSinceOpening / openingTime);

            timeSinceOpening += Time.deltaTime;
        }
    }
}
