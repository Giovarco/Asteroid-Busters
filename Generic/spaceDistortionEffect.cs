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

        Destroy(this);

    }

    Vector2 getVectorTowardsPlayer(float velocityMagnitude)
    {
        // Get initial final vector
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 finalVector = direction * velocityMagnitude;

        return finalVector;
    }
}
