using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerDetector : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggerEvent;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out StarterAssets.FirstPersonController player))
        {
            onTriggerEvent.Invoke();
        }
    }
}
