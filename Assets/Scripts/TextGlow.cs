using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TextGlow : MonoBehaviour
{
    [SerializeField]
    private Gradient glowColor;
    [SerializeField, Tooltip("The time it takes to do a repetition of the gradient")]
    private float glowTime;

    [SerializeField]
    private TextMeshPro text;

    private float timer;


    void FixedUpdate()
    {
        text.color = glowColor.Evaluate(timer / glowTime);
        timer = (timer + Time.deltaTime) % glowTime;
    }
}
