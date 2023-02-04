using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LightBrasier : MonoBehaviour
{
    private bool brasierOn = false;

    [SerializeField]
    private ParticleSystem fireParticles;

    [SerializeField]
    private UnityEvent onTriggerEvent;

    [SerializeField]
    private AudioClip startSound, repeatSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerBrasier()
    {
        if (brasierOn) return;
        brasierOn = true;

        fireParticles.Play();
        onTriggerEvent.Invoke();

        StartCoroutine(PlaySounds());
    }

    private IEnumerator PlaySounds()
    {
        audioSource.loop = false;
        audioSource.clip = startSound;
        audioSource.Play();

        yield return new WaitUntil(() => !audioSource.isPlaying);

        audioSource.clip = repeatSound;
        audioSource.loop = true;
        audioSource.Play();
    }

}
