using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour {

    Material defaultColorFlash;
    Material oldMaterial;
    Renderer rnd;

    void Awake()
    {

        // Get renderer
        rnd = gameObject.GetComponent<Renderer>();

        // Save current material
        oldMaterial = rnd.material;

        // Find a DefaultColorFlash material 
        defaultColorFlash = Resources.Load("Materials/DefaultColorFlash") as Material;

        // Set the new material
        rnd.material = defaultColorFlash;

    }

    IEnumerator changeFlash(float startingValue, float finalValue, float changeDuration, Color color)
    {

        // Prepare shader
        rnd.material.SetColor("_Color", color);
        rnd.material.SetColor("_FlashColor", color);

        // Get the actual time
        float startingTime = Time.time;

        // Change color over time
        while (Time.time - startingTime < changeDuration)
        {

            // Mathf.Lerp needs 0 <= t <= 1
            float t = (Time.time - startingTime) / changeDuration;

            // Calculate the actual value based on t
            float actualValue = Mathf.Lerp(startingValue, finalValue, t);

            // Set the flash amount
            rnd.material.SetFloat("_FlashAmount", actualValue);

            // Skip the current frame
            yield return null;

        }

    }

    void changeFlash(float value, Color color)
    {

        // Prepare shader
        rnd.material.SetColor("_Color", color);
        rnd.material.SetColor("_FlashColor", color);

        // Set the flash amount
        rnd.material.SetFloat("_FlashAmount", value);

    }

    public float getFlashAmount()
    {
        return rnd.material.GetFloat("_FlashAmount");
    }

    public IEnumerator execute(float startingValue, float finalValue, float changeDuration, Color color)
    {
        // Change sprite flash over time
        yield return StartCoroutine( changeFlash(startingValue, finalValue, changeDuration, color) );

    }

    public void execute(float value, Color color)
    {
        changeFlash(value, color);
    }

    public void restoreOldMaterial()
    {

        // Reset sprite material to its old value
        rnd.material = oldMaterial;

    }

}
