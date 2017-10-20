using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour {

    // Private
    GameSettings gameSettings;
    Factory factory;
    GameObject asteroidContainer;

    void Awake()
    {
        // Get Factory
        factory = GetComponent<Factory>();

        // Get game settings
        gameSettings = GetComponent<GameSettings>();

    }

    void Start()
    {
        // Get asteroid container
        asteroidContainer = GameObject.Find("Asteroids");

    }

    void Update()
    {
        if(!asteroidsExist())
        {
            // If there are no more asteroid, start a new level
            gameSettings.levelNumber++;

            // Generate first level
            GenerateLevel();
        }
    }
    void GenerateLevel()
    {

        // Define asteroid number
        int extraAsteroids = gameSettings.levelNumber / gameSettings.asteroidIncreaseInNumberFactor;
        int asteroidNumber = 4 + extraAsteroids;

        // Generate asteroids
        for(int i = 0; i < asteroidNumber; i++)
        {
            factory.produce("RandomAsteroid");
        }

    }

    bool asteroidsExist()
    {
        int childCount = asteroidContainer.transform.childCount;

        if(childCount == 0)
        {
            return false;
        } else
        {
            return true;
        }
    }

}
