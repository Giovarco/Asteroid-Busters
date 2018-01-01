using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChanger : MonoBehaviour {

    [SerializeField]
    float alpha;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        changeAlpha();
    }

    void changeAlpha()
    {
        Color actualColor = spriteRenderer.color;
        actualColor.a = alpha;
        spriteRenderer.color = actualColor;
    }

    void Reset()
    {
        changeAlpha();
    }

}
