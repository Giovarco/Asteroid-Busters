using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    LevelMaker levelMaker;
    GameSettings gameSettings;
    GameObject asteroidContainer;
    AsteroidFactory asteroidFactory;
    public GameObject levelText;

    // Use this for initialization
    void Start () {
        gameSettings = GetComponent<GameSettings>();
        asteroidContainer = GameObject.Find("Asteroids");
        levelMaker = GetComponent<LevelMaker>();
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();
        StartCoroutine(startGameOrchestration());
    }

    IEnumerator startGameOrchestration()
    {

        while(true)
        {

            // If there are no more asteroid, start a new level
            if (!asteroidsExist())
            {

                // Show the text with a the new level value
                gameSettings.levelNumber++;
                levelText.GetComponent<UnityEngine.UI.Text>().text = "Level " + gameSettings.levelNumber;
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
                levelMaker.generateLevel();
            }

            yield return new WaitForSeconds(0.5f);

        }

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

}
