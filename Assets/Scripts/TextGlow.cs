using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TextGlow : MonoBehaviour
{
    [SerializeField]
    private Gradient glowColor;
    [SerializeField, Tooltip("The time it takes to do a repetition of the gradient")]
    private float glowTime;
    [SerializeField, Tooltip("The time it takes to fade in and out")]
    private float fadeTime;

    [SerializeField]
    private new SpriteRenderer renderer;

    private State state;
    private float timer, fadeTimer;

    void Start()
    {
        state = State.Off;
        renderer.enabled = false;
    }


    void FixedUpdate()
    {
        if (state != State.Off && state != State.Disabled)
        {
            renderer.color = glowColor.Evaluate(timer / glowTime);
            timer = (timer + Time.deltaTime) % glowTime;
        }
        if (state == State.In)
        {
            renderer.material.SetFloat("_Fade", fadeTimer / fadeTime);
            fadeTimer += Time.deltaTime;
            if(fadeTimer >= fadeTime)
            {
                state = State.On;
            }
        }
        if (state == State.Out)
        {
            renderer.material.SetFloat("_Fade", 1 - (fadeTimer / fadeTime));
            fadeTimer += Time.deltaTime;
            if(fadeTimer >= fadeTime)
            {
                state = State.Disabled;
                renderer.enabled = false;
            }
        }
    }

    public void ShowText()
    {
        if (state == State.Off)
        {
            renderer.enabled = true;
            renderer.material.SetFloat("_Fade", 0.0f);
            state = State.In;
            fadeTimer = 0.0f;
        }
    }

    public void HideText()
    {
        if (state == State.On)
        {
            state = State.Out;
            fadeTimer = 0.0f;
        }
    }
}

enum State
{
    On,
    Off,
    In,
    Out,
    Disabled
}
