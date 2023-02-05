using UnityEngine;

public class BrasierDeco : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fireParticles;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool startFired;

    private bool fire = false;

    public void Start()
    {
        if(startFired)
        {
            fireParticles.Play();
            audioSource.Play();
            fire = true;
        }
    }

    public void FireBrasier()
    {
        if (fire) return;
        fireParticles.Play();
        audioSource.Play();
        fire = true;
    }
}
