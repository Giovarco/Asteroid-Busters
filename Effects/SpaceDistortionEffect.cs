using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDistortionEffect : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    SpaceDistortionEffectData spaceDistortionEffectData;

    void Awake()
    {
        // Get references
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PlayerShip");
        spaceDistortionEffectData = AssetReferences.spaceDistortionEffectData; // Cannot stay in Start()
    }
    
    public IEnumerator execute(float changeDuration)
    {
        // Attach components
        SpriteFlash spriteFlash = null;
        if (gameObject.GetComponent<SpriteFlash>() == null)
        {
            spriteFlash = gameObject.AddComponent<SpriteFlash>();
        } else
        {
            spriteFlash = gameObject.GetComponent<SpriteFlash>();
        }
        

        // The GO lights up
        yield return StartCoroutine( spriteFlash.execute(0f, 1f, spaceDistortionEffectData.flashDuration, Color.white) );

        // When the GO is white, change direction over time
        yield return StartCoroutine( changeDirection(changeDuration) );

        // The GO gets back to its normal colors
        yield return StartCoroutine( spriteFlash.execute(1f, 0f, spaceDistortionEffectData.flashDuration, Color.white) );

        // Restore the old material
        spriteFlash.restoreOldMaterial();

        // Detach components
        Destroy(spriteFlash);

        // Self destroy
        Destroy(this);

    }

    IEnumerator changeDirection(float changeDuration)
    {

        // Get starting velocity vector and its magnitude
        Vector2 startingVelocityVector = rb.velocity;
        float velocityMagnitude = startingVelocityVector.magnitude;

        // This vector will have the same magnitude of startingVelocityVector, but it will look at the player ship
        Vector2 finalVelocityVector;

        // Starting time
        float startingTime = Time.time;

        // Change duration over time
        while (Time.time - startingTime < changeDuration)
        {

            // Update the final vector every frame, since the player ship moves
            finalVelocityVector = getVectorTowardsPlayer(velocityMagnitude);

            // Update the velocity vector every frame
            rb.velocity = Vector2.Lerp(startingVelocityVector, finalVelocityVector, Time.time - startingTime);

            // Skip frame
            yield return null;

        }

    }

    Vector2 getVectorTowardsPlayer(float velocityMagnitude)
    {
        // Get unit vector towards the player ship
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Get final vector
        Vector2 finalVector = direction * velocityMagnitude;

        return finalVector;
    }
}
