using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCollisionController : MonoBehaviour {

    public float minDistanceToTeleport;
    public float stasisDuration;
    public float wayBackTravelDuration;
    public float wayUpTravelDuration;

    GameObject asteroidContainer;
    ScreenInformation screenInfo;

    void Awake()
    {
        screenInfo = GameObject.Find("Main Camera").GetComponent<ScreenInformation>();
        asteroidContainer = GameObject.Find("Asteroids");
    }

    void Update()
    {

        // Get all asteroids transforms
        foreach (Transform asteroidTransform in asteroidContainer.transform)
        {

            GameObject asteroid = asteroidTransform.gameObject;
            AsteroidProperties asteroidProperties = asteroid.GetComponent<AsteroidProperties>();

            // Check if the asteroid is already teleporting
            if (asteroidProperties.status != Status.Teleporting)
            {
                float distance = Vector2.Distance(transform.position, asteroidTransform.position);

                if (distance < minDistanceToTeleport)
                {
                    StartCoroutine( teleportProcess(asteroid) );
                }
            }


        }

    }

    Vector2 freezeGameObject(GameObject go)
    {

        // Get rigidbody
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        // Save old velocity
        Vector2 oldVelocity = rb.velocity;

        // Stop the object from moving
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        return oldVelocity;

    }

    OverTimeSizeChanger getOverTimeSizeChanger(GameObject go)
    {

        if (go.GetComponent("OverTimeSizeChanger") == null)
        {
            return go.AddComponent<OverTimeSizeChanger>();
        }
        else
        {
            return go.GetComponent<OverTimeSizeChanger>();
        }

    }

    Vector3 getRandomPosition()
    {
        float xPos = UnityEngine.Random.Range(screenInfo.leftEdge, screenInfo.rightEdge);
        float yPos = UnityEngine.Random.Range(screenInfo.lowerEdge, screenInfo.upperEdge);
        return new Vector3(xPos, yPos, 1);
    }

    SpriteFlash getSpriteFlash(GameObject go)
    {

        if (go.GetComponent("SpriteFlash") == null)
        {
            return go.AddComponent<SpriteFlash>();
        }
        else
        {
            return go.GetComponent<SpriteFlash>();
        }

    }

    IEnumerator teleportProcess(GameObject go)
    {

        // Set the current game object status to "teleporting"
        AsteroidProperties asteroidProperties = go.GetComponent<AsteroidProperties>();
        asteroidProperties.status = Status.Teleporting;

        // Disable game object collider to avoid unwanted interactions
        Collider2D asteroidCollider = go.GetComponent<Collider2D>();
        asteroidCollider.enabled = false;

        // Add two scripts to the game object for fancy graphic (no duplicates)
        OverTimeSizeChanger externalOverTimeSizeChanger = getOverTimeSizeChanger(go);
        SpriteFlash externalSpriteFlash = getSpriteFlash(go);

        // Freeze completly the game object
        Vector2 oldVelocity = freezeGameObject(go);

        // Game object: become smaller
        float localScale = go.transform.localScale.x;
        StartCoroutine( externalOverTimeSizeChanger.execute(localScale, 0f, wayUpTravelDuration) );

        // Game object: become black
        yield return StartCoroutine( externalSpriteFlash.execute(0f, 1f, wayUpTravelDuration, Color.black) );

        // Make game object invisible
        Renderer renderer = go.GetComponent<Renderer>();
        renderer.enabled = false;

        // Put game object in stasis
        yield return new WaitForSeconds(stasisDuration);

        // Teleport to another position
        go.transform.position = getRandomPosition();

        // Make game object visible again
        renderer.enabled = true;

        // Game object: become bigger
        StartCoroutine( externalOverTimeSizeChanger.execute(0f, localScale, wayBackTravelDuration) );

        // Game object: get colors again
        yield return StartCoroutine(externalSpriteFlash.execute(1f, 0f, wayBackTravelDuration, Color.white));

        // Unfreeze game object
        unfreezeGameObject(go, oldVelocity);

        // Delete useless scripts on game object
        Destroy(externalSpriteFlash);
        Destroy(externalOverTimeSizeChanger);

        // Enable game object collider again 
        asteroidCollider.enabled = true;

        // Game object is set to a non-teleporting status
        asteroidProperties.status = Status.Ok;

    }

    private void unfreezeGameObject(GameObject go, Vector2 oldVelocity)
    {
        // Get rigidbody
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        // Restore the abilty to move and old velocity
        rb.isKinematic = false;
        rb.velocity = oldVelocity;

    }


}
