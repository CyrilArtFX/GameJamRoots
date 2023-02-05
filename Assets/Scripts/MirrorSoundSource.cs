using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MirrorSoundSource : MonoBehaviour
{
    [SerializeField]
    private AudioClip moveStartSound, moveRepeatSound, moveStopSound;

    private AudioSource audioSource;
    private Vector3 lastFramePosition;

    private MoveState moveState;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        lastFramePosition = transform.position;
        moveState = MoveState.Stopped;
    }

    void FixedUpdate()
    {
        switch (moveState)
        {
            case MoveState.Stopped:
                if (transform.position != lastFramePosition)
                {
                    moveState = MoveState.Starting;
                    audioSource.clip = moveStartSound;
                    audioSource.loop = false;
                    audioSource.Play();
                }
                break;

            case MoveState.Moving:
                if (transform.position == lastFramePosition)
                {
                    moveState = MoveState.Stopping;
                    audioSource.clip = moveStopSound;
                    audioSource.loop = false;
                    audioSource.Play();
                }
                break;

            case MoveState.Starting:
                if (transform.position == lastFramePosition)
                {
                    moveState = MoveState.Stopping;
                    audioSource.clip = moveStopSound;
                    audioSource.loop = false;
                    audioSource.Play();
                }
                else
                {
                    if (!audioSource.isPlaying)
                    {
                        moveState = MoveState.Moving;
                        audioSource.clip = moveRepeatSound;
                        audioSource.loop = true;
                        audioSource.Play();
                    }
                }
                break;

            case MoveState.Stopping:
                if (transform.position != lastFramePosition)
                {
                    moveState = MoveState.Starting;
                    audioSource.clip = moveStartSound;
                    audioSource.loop = false;
                    audioSource.Play();
                }
                break;
        }

        lastFramePosition = transform.position;
    }

    enum MoveState
    {
        Stopped,
        Moving,
        Starting,
        Stopping
    }
}
