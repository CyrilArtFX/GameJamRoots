using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

enum FadeStates
{
    FadeIn,
    StayBlack,
    FadeOut,
    Off
}

public enum FadeType
{
    OnlyFadeIn,
    OnlyFadeOut,
    BothFadesWithRestore
}


public class BlackFade : MonoBehaviour
{
    [Header("Black Fade")]
    [SerializeField]
    Image blackFadeImage = default;
    [SerializeField]
    float blackFadeHalfTime = 1.0f, fullBlackTime = 0.5f;
    [SerializeField]
    AnimationCurve blackFadeCurve = default;

    float timeSinceBlackFadeStarted = 0.0f;

    [SerializeField]
    bool fadeInAwake = true;
    FadeStates blackFadeState = FadeStates.Off;
    [SerializeField]
    FadeType blackFadeType = FadeType.OnlyFadeOut;

    [SerializeField]
    bool shouldToggleCommands = true;

    [HideInInspector]
    public UnityEvent eventEndOfFadeIn = default;

    public static BlackFade instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //  start scene-defined fade
        if (fadeInAwake)
        {
            StartFade(blackFadeType, blackFadeImage.color, shouldToggleCommands);
        }
    }

    void Update()
    {
        switch (blackFadeState)
        {
            case FadeStates.Off:
                return;

            case FadeStates.FadeIn:

                timeSinceBlackFadeStarted += Time.unscaledDeltaTime;
                if (timeSinceBlackFadeStarted >= blackFadeHalfTime)
                {
                    timeSinceBlackFadeStarted = 0.0f; 


                    Color color = blackFadeImage.color;
                    color.a = 1.0f;
                    blackFadeImage.color = color;

                    if (blackFadeType == FadeType.BothFadesWithRestore)
                    {
                        blackFadeState = FadeStates.StayBlack;

                        eventEndOfFadeIn.Invoke();
                    }
                    else
                    {
                        blackFadeState = FadeStates.Off;
                    }
                }
                else
                {
                    float transition_fraction = timeSinceBlackFadeStarted / blackFadeHalfTime;

                    Color color = blackFadeImage.color;
                    color.a = blackFadeCurve.Evaluate(transition_fraction);
                    blackFadeImage.color = color;
                }

                break;

            case FadeStates.StayBlack:

                timeSinceBlackFadeStarted += Time.unscaledDeltaTime;
                if (timeSinceBlackFadeStarted >= fullBlackTime)
                {
                    timeSinceBlackFadeStarted = 0.0f;
                    blackFadeState = FadeStates.FadeOut;
                }

                break;

            case FadeStates.FadeOut:

                timeSinceBlackFadeStarted += Time.unscaledDeltaTime;
                if (timeSinceBlackFadeStarted >= blackFadeHalfTime)
                {
                    timeSinceBlackFadeStarted = 0.0f;
                    blackFadeState = FadeStates.Off;
                    Color color = blackFadeImage.color;
                    color.a = 0.0f;
                    blackFadeImage.color = color;

                    if (shouldToggleCommands)
                    {
                        //Player.instance.disableCommands = false;
                    }
                }
                else
                {
                    float transition_fraction = 1 - (timeSinceBlackFadeStarted / blackFadeHalfTime);
                    Color color = blackFadeImage.color;
                    color.a = blackFadeCurve.Evaluate(transition_fraction);
                    blackFadeImage.color = color;
                }

                break;
        }
    }

    public void StartFade(FadeType fadeType, Color color, bool toggle_commands = true)
    {
        shouldToggleCommands = toggle_commands;
        if (shouldToggleCommands)
        {
            //Player.instance.disableCommands = true;
        }

        timeSinceBlackFadeStarted = 0.0f;
        blackFadeType = fadeType;
        if (fadeType == FadeType.OnlyFadeOut)
        {
            blackFadeState = FadeStates.FadeOut;
            blackFadeImage.color = color;
        }
        else
        {
            blackFadeState = FadeStates.FadeIn;
            blackFadeImage.color = new Color(color.r, color.g, color.b, 0.0f);
        }
    }
}
