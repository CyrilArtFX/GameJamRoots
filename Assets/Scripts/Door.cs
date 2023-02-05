using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private bool isOpen;

    private AudioSource audioSource;
    private Animator animator;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        audioSource.Play();
        animator.SetTrigger("Open");
    }
}
