using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour {

    [SerializeField]
    Material defaultColorFlash;

    Renderer renderer;

    void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        defaultColorFlash = Resources.Load("Materials/DefaultColorFlash") as Material;
        renderer.material = defaultColorFlash;
    }

    public IEnumerator changeFlash(float startingValue, float finalValue, float changeDuration, Color color)
    {
        // Prepare shader
        renderer.material.SetColor("_Color", color);
        renderer.material.SetColor("_FlashColor", color);

        // Body
        float addend;

        if (startingValue <= finalValue)
        {
            addend = (finalValue - startingValue) * Time.deltaTime / changeDuration;

            for (float actualValue = startingValue; actualValue < finalValue; actualValue += addend)
            {
                renderer.material.SetFloat("_FlashAmount", actualValue);
                yield return null;
            }
        }
        else
        {
            addend = (startingValue - finalValue) * Time.deltaTime / changeDuration;
            for (float actualValue = startingValue; actualValue > finalValue; actualValue -= addend)
            {
                renderer.material.SetFloat("_FlashAmount", actualValue);
                yield return null;
            }
        }

    }

}
