using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCollisionController : MonoBehaviour {

    public float travelDuration;
    public float stasisDuration;

    GameSettings gameSettings;

    void Awake()
    {
        gameSettings = GameObject.Find("Orchestrator").GetComponent<GameSettings>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;

        // Check if it has the needed scripts
        if (otherGameObject.GetComponent("OverTimeSizeChanger") == null && otherGameObject.GetComponent("SpriteFlash") == null)
        {
            StartCoroutine(teleportProcess(otherGameObject));
        }

    }

    IEnumerator teleportProcess(GameObject otherGameObject)
    {

        // Add needed scripts
        SpriteFlash externalSpriteFlash = otherGameObject.AddComponent<SpriteFlash>();
        OverTimeSizeChanger externalOverTimeSizeChanger = externalOverTimeSizeChanger = otherGameObject.AddComponent<OverTimeSizeChanger>();

        // Become smaller
        float localScale = otherGameObject.transform.localScale.x;
        StartCoroutine(externalOverTimeSizeChanger.changeSize(localScale, 0f, travelDuration));

        // Become white
        yield return StartCoroutine(externalSpriteFlash.changeFlash(0f, 1f, travelDuration));

        // Stasis
        Renderer renderer = otherGameObject.GetComponent<Renderer>();
        renderer.enabled = false;
        yield return new WaitForSeconds(stasisDuration);

        // Teleport
        otherGameObject.transform.position = getRandomPosition();

        // Set velocity to 0 without losing the old velocity
        Rigidbody2D rb = otherGameObject.GetComponent<Rigidbody2D>();
        Vector2 oldVelocity = rb.velocity;
        rb.velocity = new Vector2(0, 0);

        // Active GameObject
        renderer.enabled = true;

        // Become bigger
        StartCoroutine(externalOverTimeSizeChanger.changeSize(0f, localScale, travelDuration));

        // Get colors again
        yield return StartCoroutine(externalSpriteFlash.changeFlash(1f, 0f, travelDuration));

        // Give the old velocity
        rb.velocity = oldVelocity;

        // Delete scripts
        Destroy(externalSpriteFlash);
        Destroy(externalOverTimeSizeChanger);

        yield return null;
    }

    Vector3 getRandomPosition()
    {
        float xPos = UnityEngine.Random.Range(gameSettings.leftEdge, gameSettings.rightEdge);
        float yPos = UnityEngine.Random.Range(gameSettings.lowerEdge, gameSettings.upperEdge);
        return new Vector3(xPos, yPos, 1);
    }
}
