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

        // Get the actual time
        float startingTime = Time.time;

        // Change color over time
        while(Time.time - startingTime < changeDuration)
        {
            float t = (Time.time - startingTime) / changeDuration;
            float actualValue = Mathf.Lerp(startingValue, finalValue, t);
            rnd.material.SetFloat("_FlashAmount", actualValue);
            yield return null;
        }

    }

}
