using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    LevelMaker levelMaker;
    GameSettings gameSettings;
    public float delayBetweenLevels;
    GameObject asteroidContainer;

    // Use this for initialization
    void Start () {
        gameSettings = GetComponent<GameSettings>();
        asteroidContainer = GameObject.Find("Asteroids");
        levelMaker = GetComponent<LevelMaker>();
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

                yield return new WaitForSeconds(4f);

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
