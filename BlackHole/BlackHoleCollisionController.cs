using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCollisionController : MonoBehaviour {

    public float travelDuration;
    public float stasisDuration;

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

        // Disable and put in stasis
        // gameObject.SetActive(false);
        yield return new WaitForSeconds(stasisDuration);

        // Teleport
        // gameObject.transform.position =

        // Become bigger
        StartCoroutine(externalOverTimeSizeChanger.changeSize(0f, localScale, travelDuration));

        // Get colors again
        yield return StartCoroutine(externalSpriteFlash.changeFlash(1f, 0f, travelDuration));

        // Delete scripts
        Destroy(externalSpriteFlash);
        Destroy(externalOverTimeSizeChanger);

        yield return null;
    }
}
