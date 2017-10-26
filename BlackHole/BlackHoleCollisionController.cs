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
        foreach (Transform asteroidTransform in asteroidContainer.transform)
        {

            GameObject asteroid = asteroidTransform.gameObject;
            AsteroidProperties asteroidProperties = asteroid.GetComponent<AsteroidProperties>();

            if(asteroidProperties.status != GameSettings.Status.Teleporting)
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
        print("TELEPORTING");

        //
        AsteroidProperties asteroidProperties = otherGameObject.GetComponent<AsteroidProperties>();
        asteroidProperties.status = GameSettings.Status.Teleporting;

        //
        Collider2D asteroidCollider = otherGameObject.GetComponent<Collider2D>();
        asteroidCollider.enabled = false;
        print("COLLIDER DISABLED");

        // These variables have to be not null
        OverTimeSizeChanger externalOverTimeSizeChanger = null;
        SpriteFlash externalSpriteFlash = null;

        // Add needed scripts
        if (otherGameObject.GetComponent("OverTimeSizeChanger") == null)
        {
            externalOverTimeSizeChanger = otherGameObject.AddComponent<OverTimeSizeChanger>();
        }

        if(otherGameObject.GetComponent("SpriteFlash") == null)
        {
            externalSpriteFlash = otherGameObject.AddComponent<SpriteFlash>();
        }

        print("externalOverTimeSizeChanger="+ externalOverTimeSizeChanger);
        print("externalSpriteFlash="+ externalSpriteFlash);

        if(externalOverTimeSizeChanger != null && externalSpriteFlash != null)
        {
            print("SCRIPTS ADDED TO THE ASTEROID. TIME TO ROCK!");

            // Set velocity to 0 without losing the old velocity
            Rigidbody2D rb = otherGameObject.GetComponent<Rigidbody2D>();
            Vector2 oldVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            // Become smaller
            float localScale = otherGameObject.transform.localScale.x;
            StartCoroutine(externalOverTimeSizeChanger.changeSize(localScale, 0f, wayUpTravelDuration));

            // Become black
            yield return StartCoroutine(externalSpriteFlash.changeFlash(0f, 1f, wayUpTravelDuration, Color.black));

            // Stasis
            Renderer renderer = otherGameObject.GetComponent<Renderer>();
            renderer.enabled = false;
            yield return new WaitForSeconds(stasisDuration);

            // Teleport
            otherGameObject.transform.position = getRandomPosition();

            // Active GameObject
            renderer.enabled = true;

            // Become bigger
            StartCoroutine(externalOverTimeSizeChanger.changeSize(0f, localScale, wayBackTravelDuration));

            // Get colors again
            yield return StartCoroutine(externalSpriteFlash.changeFlash(1f, 0f, wayBackTravelDuration, Color.white));

            // Give the old velocity
            rb.isKinematic = false;
            rb.velocity = oldVelocity;

            // Delete scripts
            Destroy(externalSpriteFlash);
            Destroy(externalOverTimeSizeChanger);

            asteroidCollider.enabled = true;
            print("COLLIDER ENABLED AGAIN");

            //
            asteroidProperties.status = GameSettings.Status.Ok;
        } else
        {
            print("ONE OF THE TWO SCRIPS DOES NOT EXIST");
        }



        print("--------------------------------");

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
