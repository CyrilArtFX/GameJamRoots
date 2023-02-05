using StarterAssets;
using UnityEngine;

public class Rotable : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed = 20.0f;

    private AudioSource audioSource;
    private Quaternion rotationLastFrame;
    private bool playingSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rotationLastFrame = transform.rotation;
        playingSound = false;
    }

    public void Rotate(float dir)
    {
        float amount = dir * turnSpeed;

        Vector3 old_rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(old_rotation.x, old_rotation.y + amount, old_rotation.z);
    }

    void FixedUpdate()
    {
        if (transform.rotation == rotationLastFrame)
        {
            if(playingSound)
            {
                audioSource.Stop();
                playingSound = false;
            }
        }
        else
        {
            if(!playingSound)
            {
                audioSource.Play();
                playingSound = true;
            }
        }
        rotationLastFrame = transform.rotation;
    }
}
