using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDistortionEffect : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PlayerShip");
    }

    public IEnumerator changeDirection(float changeDuration)
    {

        // Attach the sprite flash component
        SpriteFlash spriteFlash = gameObject.AddComponent<SpriteFlash>();

        // L'oggetto si illumina di bianco
        yield return StartCoroutine(spriteFlash.changeFlash(0f, 1f, 0.5f, Color.white));

        // Quando l'oggetto è bianco, ha appena cambiato direzione
        yield return StartCoroutine(_changeDirection(changeDuration));

        // Ritorna al colore normale
        yield return StartCoroutine(spriteFlash.changeFlash(1f, 0f, 0.5f, Color.white));

        Destroy(spriteFlash);
        Destroy(this);

    }

    IEnumerator _changeDirection(float changeDuration)
    {

        // Get starting velocity vector and its magnitude
        Vector2 startingVelocityVector = rb.velocity;
        float velocityMagnitude = startingVelocityVector.magnitude;

        // This vector has the same magnitude of startingVelocityVector, but it looks at the player ship
        Vector2 finalVector;

        // Starting Duration
        float startingTime = Time.time;

        // Execute for changeDuration time
        while (Time.time - startingTime < changeDuration)
        {

            // Update the final vector every frame, since the player ship moves
            finalVector = getVectorTowardsPlayer(velocityMagnitude);

            // Update the velocity vector every frame
            rb.velocity = Vector2.Lerp(startingVelocityVector, finalVector, Time.time - startingTime);

            yield return null;
        }

    }

    Vector2 getVectorTowardsPlayer(float velocityMagnitude)
    {
        // Get initial final vector
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 finalVector = direction * velocityMagnitude;

        return finalVector;
    }
}
