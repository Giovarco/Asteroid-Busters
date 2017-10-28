using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour {

    [SerializeField]
    Material defaultColorFlash;

    Renderer rnd;

    void Awake()
    {
        rnd = gameObject.GetComponent<Renderer>();
        defaultColorFlash = Resources.Load("Materials/DefaultColorFlash") as Material;
        rnd.material = defaultColorFlash;
    }

    public IEnumerator changeFlash(float startingValue, float finalValue, float changeDuration, Color color)
    {
        // Prepare shader
        rnd.material.SetColor("_Color", color);
        rnd.material.SetColor("_FlashColor", color);

        // Body
        float addend;

        if (startingValue <= finalValue)
        {
            addend = (finalValue - startingValue) * Time.deltaTime / changeDuration;

            for (float actualValue = startingValue; actualValue < finalValue; actualValue += addend)
            {
                rnd.material.SetFloat("_FlashAmount", actualValue);
                yield return null;
            }
        }
        else
        {
            addend = (startingValue - finalValue) * Time.deltaTime / changeDuration;
            for (float actualValue = startingValue; actualValue > finalValue; actualValue -= addend)
            {
                rnd.material.SetFloat("_FlashAmount", actualValue);
                yield return null;
            }
        }

    }

}
