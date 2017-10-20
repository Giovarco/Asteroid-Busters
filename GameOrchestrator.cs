using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    LevelMaker levelMaker;
    GameSettings gameSettings;
    public float delayBetweenLevels;
    GameObject asteroidContainer;
    Factory factory;

    // Use this for initialization
    void Start () {
        gameSettings = GetComponent<GameSettings>();
        asteroidContainer = GameObject.Find("Asteroids");
        levelMaker = GetComponent<LevelMaker>();
        factory = GetComponent<Factory>();
        StartCoroutine(startGameOrchestration());
    }

    IEnumerator startGameOrchestration()
    {

        while(true)
        {
            if(!asteroidsExist())
            {

                // If there are no more asteroid, start a new level
                gameSettings.levelNumber++;
                print("LEVEL " + gameSettings.levelNumber);

                factory.asteroidSpritesIndex++;
                if (factory.asteroidSpritesIndex >= factory.getAsteroidSpritesLength())
                {
                    factory.asteroidSpritesIndex = 0;
                }

                yield return new WaitForSeconds(delayBetweenLevels);

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
