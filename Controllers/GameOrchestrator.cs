using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject levelText;

    GameObject asteroidContainer;
    AsteroidFactory asteroidFactory;
    BlackHoleData blackHoleData;
    BlackHoleFactory blackHoleFactory;
    LevelGenerator levelGenerator;

    void Start()
    {
        // Get references
        blackHoleData = GetComponent<AssetReferences>().blackHoleData;
        asteroidContainer = GameObject.Find("Asteroids");
        levelGenerator = GetComponent<LevelGenerator>();
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();
        blackHoleFactory = GameObject.Find("Factories").GetComponent<BlackHoleFactory>();

        // Start coroutines
        StartCoroutine(startGameOrchestration());
        StartCoroutine(spawnBlackHole());
    }

    bool asteroidsExist()
    {
        int childCount = asteroidContainer.transform.childCount;

        if (childCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator spawnBlackHole()
    {
        while (true)
        {
            yield return new WaitForSeconds(blackHoleData.spawnFrequency);
            blackHoleFactory.instantiate("BlackHole");
        }
    }

    
    IEnumerator startGameOrchestration()
    {

        while(true)
        {

            // If there are no more asteroid, start a new level
            if (!asteroidsExist())
            {

                // Show the text with a the new level value
                levelGenerator.currentLevel++;
                levelText.GetComponent<UnityEngine.UI.Text>().text = "Level " + levelGenerator.currentLevel;
                levelText.SetActive(true);

                // Set the new asteroid sprite
                asteroidFactory.asteroidSpritesIndex++;
                if (asteroidFactory.asteroidSpritesIndex >= asteroidFactory.getAsteroidSpritesLength())
                {
                    asteroidFactory.asteroidSpritesIndex = 0;
                }

                // Wait some seconds
                yield return new WaitForSeconds(4f);

                // Deactive the level text
                levelText.SetActive(false);

                // Wait a bunch of other seconds
                yield return new WaitForSeconds(1.5f);

                // Generate first level
                levelGenerator.generateLevel();
            }

            yield return new WaitForSeconds(0.5f);

        }

    }
}
