using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour {

    // Private
    GameSettings gameSettings;
    AsteroidFactory asteroidFactory;

    void Awake()
    {
        // Get Factory
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();

        // Get game settings
        gameSettings = GetComponent<GameSettings>();

    }
    
    public void generateLevel()
    {

        // Define asteroid number
        int extraAsteroids = gameSettings.levelNumber / gameSettings.asteroidIncreaseInNumberFactor;
        int asteroidNumber = 4 + extraAsteroids;
        asteroidNumber = 1;

        // Generate asteroids
        for(int i = 0; i < asteroidNumber; i++)
        {
            asteroidFactory.instantiate("RandomAsteroid");
        }

    }

}
