using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Not used anymore", true)]
public class SpaceDistortionEffectOlds : MonoBehaviour {

    [SerializeField]
    float angleRotation = 90;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator changeDirection(float changeDuration)
    {
        print("Changing position");
        Vector2 startingValue = rb.velocity;
        Vector2 finalValue = Quaternion.AngleAxis(angleRotation, Vector3.forward) * startingValue;
        float startingTime = Time.time;
        
        while(Time.time - startingTime < changeDuration)
        {
            rb.velocity = Vector2.Lerp(startingValue, finalValue, Time.time - startingTime);
            yield return null;
        }

        Destroy(this);
        
    }

}
