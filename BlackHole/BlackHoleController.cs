using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

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
        yield return StartCoroutine(sizeChanger.changeSize(startingSize, finalSize, sizeChangeDuration));
        yield return new WaitForSeconds(sizeChangeDuration);
        yield return StartCoroutine(sizeChanger.changeSize(finalSize, startingSize, sizeChangeDuration));
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

            if(asteroidProperties.status == GameSettings.Status.Teleporting)
            {
                return true;
            }

        }

        return false;
    }
}
