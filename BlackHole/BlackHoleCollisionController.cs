using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCollisionController : MonoBehaviour {

    public float wayUpTravelDuration;
    public float wayBackTravelDuration;
    public float stasisDuration;
    public float minDistanceToTeleport;

    GameSettings gameSettings;

    GameObject asteroidContainer;

    void Awake()
    {
        gameSettings = GameObject.Find("Orchestrator").GetComponent<GameSettings>();
    }

    void Start()
    {
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
            if(asteroidProperties.status != Status.Teleporting)
            {
                float distance = Vector2.Distance(transform.position, asteroidTransform.position);

                if (distance < minDistanceToTeleport)
                {
                    StartCoroutine(teleportProcess(asteroid));
                }
            }


        }

    }

    IEnumerator teleportProcess(GameObject otherGameObject)
    {

        // Set the current game object status to "teleporting"
        AsteroidProperties asteroidProperties = otherGameObject.GetComponent<AsteroidProperties>();
        asteroidProperties.status = Status.Teleporting;

        // Disable game object collider to avoid unwanted interactions
        Collider2D asteroidCollider = otherGameObject.GetComponent<Collider2D>();
        asteroidCollider.enabled = false;

        // Add two scripts to the game object for fancy graphic
        OverTimeSizeChanger externalOverTimeSizeChanger = null;
        SpriteFlash externalSpriteFlash = null;

        if (otherGameObject.GetComponent("OverTimeSizeChanger") == null)
        {
            externalOverTimeSizeChanger = otherGameObject.AddComponent<OverTimeSizeChanger>();
        }

        if(otherGameObject.GetComponent("SpriteFlash") == null)
        {
            externalSpriteFlash = otherGameObject.AddComponent<SpriteFlash>();
        }

        // Freeze completly the game object
        Rigidbody2D rb = otherGameObject.GetComponent<Rigidbody2D>();
        Vector2 oldVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // Game object: become smaller
        float localScale = otherGameObject.transform.localScale.x;
        StartCoroutine(externalOverTimeSizeChanger.changeSize(localScale, 0f, wayUpTravelDuration));

        // Game object: become black
        yield return StartCoroutine(externalSpriteFlash.changeFlash(0f, 1f, wayUpTravelDuration, Color.black));

        // Make game object invisible
        Renderer renderer = otherGameObject.GetComponent<Renderer>();
        renderer.enabled = false;

        // Put game object in stasis
        yield return new WaitForSeconds(stasisDuration);

        // Teleport to another position
        otherGameObject.transform.position = getRandomPosition();

        // Make game object visible again
        renderer.enabled = true;

        // Game object: become bigger
        StartCoroutine(externalOverTimeSizeChanger.changeSize(0f, localScale, wayBackTravelDuration));

        // Game object: get colors again
        yield return StartCoroutine(externalSpriteFlash.changeFlash(1f, 0f, wayBackTravelDuration, Color.white));

        // Unfreeze game object
        rb.isKinematic = false;
        rb.velocity = oldVelocity;

        // Delete useless scripts on game object
        Destroy(externalSpriteFlash);
        Destroy(externalOverTimeSizeChanger);

        // Enable game object collider again 
        asteroidCollider.enabled = true;

        // Game object is set to a non-teleporting status
        asteroidProperties.status = Status.Ok;

    }

    Vector3 getRandomPosition()
    {
        float xPos = UnityEngine.Random.Range(gameSettings.leftEdge, gameSettings.rightEdge);
        float yPos = UnityEngine.Random.Range(gameSettings.lowerEdge, gameSettings.upperEdge);
        return new Vector3(xPos, yPos, 1);
    }

    bool FullyContains(Collider2D resident)
    {
        Collider2D zone = GetComponent<Collider2D>();
        return zone.bounds.Contains(resident.bounds.max) && zone.bounds.Contains(resident.bounds.min);
        // return zone.bounds.Contains(resident.bounds.max);
    }
}
