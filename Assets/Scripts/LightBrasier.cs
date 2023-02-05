using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LightBrasier : MonoBehaviour
{
    private bool brasierOn, triggered;

    [SerializeField]
    private ParticleSystem fireParticles;
    [SerializeField]
    private ParticleSystem ignitionParticles;

    [SerializeField]
    private UnityEvent onTriggerEvent;

    [SerializeField]
    private float timeForLight;

    [SerializeField]
    private AudioClip startSound, repeatSound;

    private AudioSource audioSource;

    private float timer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        brasierOn = triggered = false;
    }

    public void TriggerBrasier()
    {
        if (brasierOn) return;
        triggered = true;
    }

    void FixedUpdate()
    {
        if (brasierOn) return;
        if (triggered)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= timeForLight)
            {
                EnlightBrasier();
            }

            if (!ignitionParticles.isPlaying) ignitionParticles.Play();
        }
        else
        {
            timer = 0.0f;
            ignitionParticles.Stop();
        }

        triggered = false;
    }

    private void EnlightBrasier()
    {
        brasierOn = true;
        triggered = false;

        ignitionParticles.Stop();
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
