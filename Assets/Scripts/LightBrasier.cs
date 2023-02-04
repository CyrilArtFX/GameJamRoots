using UnityEngine;
using UnityEngine.Events;

public class LightBrasier : MonoBehaviour
{
    private bool brasierOn = false;

    [SerializeField]
    private ParticleSystem fireParticles;

    [SerializeField]
    private UnityEvent onTriggerEvent;

    public void TriggerBrasier()
    {
        if (brasierOn) return;
        brasierOn = true;

        fireParticles.Play();
        onTriggerEvent.Invoke();
    }
}
