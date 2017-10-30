using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

    public BlackHoleData blackHoleData;
    public float startingSize;
    public float finalSize;
    public float sizeChangeDuration;

    OverTimeSizeChanger sizeChanger;
    GameObject asteroidContainer;

    void Awake()
    {
        sizeChanger = GetComponent<OverTimeSizeChanger>();
    }

	void Start () {
        asteroidContainer = GameObject.Find("Asteroids");
        StartCoroutine(blackHoleLife());
	}

    IEnumerator blackHoleLife()
    {
        // Become bigger
        yield return StartCoroutine(sizeChanger.changeSize(startingSize, finalSize, sizeChangeDuration));

        // Persist for a while
        yield return new WaitForSeconds(blackHoleData.duration);

        // Disable collision detection and attraction
        GetComponent<BlackHoleCollisionController>().enabled = false;

        // Disable attraction force
        GameObject aura = transform.GetChild(0).gameObject;
        aura.GetComponent<PointEffector2D>().enabled = false;

        // Become smaller
        yield return StartCoroutine(sizeChanger.changeSize(finalSize, startingSize, sizeChangeDuration));

        // Destroy itself when all asteroids respawn
        yield return StartCoroutine(destroy());
    }

    IEnumerator destroy()
    {
        while(areAsteroidsTeleporting())
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    bool areAsteroidsTeleporting()
    {
        // Get all asteroids transforms
        foreach (Transform asteroidTransform in asteroidContainer.transform)
        {

            GameObject asteroid = asteroidTransform.gameObject;
            AsteroidProperties asteroidProperties = asteroid.GetComponent<AsteroidProperties>();

            if(asteroidProperties.status == Status.Teleporting)
            {
                return true;
            }

        }

        return false;
    }
}
