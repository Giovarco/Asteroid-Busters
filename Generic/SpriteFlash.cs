using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour {

    public Material defaultColorFlash;

    Renderer renderer;

    void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        renderer.material = new Material(defaultColorFlash);
    }

    public IEnumerator changeFlash(float startingValue, float finalValue, float changeDuration)
    {

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
            print(addend);
            for (float actualValue = startingValue; actualValue > finalValue; actualValue -= addend)
            {
                renderer.material.SetFloat("_FlashAmount", actualValue);
                yield return null;
            }
        }

    }

}
