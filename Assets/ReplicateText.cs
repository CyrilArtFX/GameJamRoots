using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ReplicateText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro sourceTMP;

    private TextMeshPro tmp;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if ( sourceTMP == null ) return;

        //  link text
        tmp.text = sourceTMP.text;
        tmp.fontSize = sourceTMP.fontSize;

        //  link color
        Color color = tmp.color;
        color.a = sourceTMP.color.a;
        tmp.color = color;
    }
}
