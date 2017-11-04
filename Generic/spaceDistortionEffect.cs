using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceDistortionEffect : MonoBehaviour {

    [SerializeField]
    float angleRotation;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(changeDirection(2.5f));
    }

    public IEnumerator changeDirection(float changeDuration)
    {

        Vector2 startingValue = rb.velocity;
        Vector2 finalValue = Quaternion.AngleAxis(angleRotation, Vector3.forward) * startingValue;
        float startingTime = Time.time;
        
        while(Time.time - startingTime < changeDuration)
        {
            rb.velocity = Vector2.Lerp(startingValue, finalValue, Time.time - startingTime);
            yield return null;
        }
        
    }

}
