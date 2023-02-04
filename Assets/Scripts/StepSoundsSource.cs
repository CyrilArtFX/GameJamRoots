using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepSoundsSource : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] leftStepSounds;
    [SerializeField]
    private AudioClip[] rightStepSounds;

    [SerializeField]
    private float distanceBetweenTwoSteps;

    private AudioSource audioSource;
    private bool wasLastStepRight;
    private Vector3 lastFramePosition;
    private float distanceSinceLastStep;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        distanceSinceLastStep = 0.0f;
        lastFramePosition = transform.position;
    }

    void FixedUpdate()
    {
        distanceSinceLastStep += Vector3.Distance(lastFramePosition, transform.position);

        if (distanceSinceLastStep >= distanceBetweenTwoSteps)
        {
            if (wasLastStepRight)
            {
                int rdm = Random.Range(0, leftStepSounds.Length);
                audioSource.PlayOneShot(leftStepSounds[rdm]);
            }
            else
            {
                int rdm = Random.Range(0, rightStepSounds.Length);
                audioSource.PlayOneShot(rightStepSounds[rdm]);
            }

            wasLastStepRight = !wasLastStepRight;
            distanceSinceLastStep = 0.0f;
        }

        lastFramePosition = transform.position;
    }
}
