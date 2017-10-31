using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [Tooltip("If it equals zero, then the first level will be one")]
    [ReadOnly]
    public int currentLevel;

    AsteroidFactory asteroidFactory;
    DifficultyConfigurationData difficultyConfigData;
    GameConfigurationData gameConfigData;

    void Awake()
    {
        // Get in-the-game references
        gameConfigData = GetComponent<AssetReferences>().gameConfigData;
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();
        difficultyConfigData = GameObject.Find("Orchestrator").GetComponent<AssetReferences>().difficultyConfigData;

        // Set the current level
        currentLevel = gameConfigData.startingLevel;
    }

    public void generateLevel()
    {

        // Define asteroid number
        int extraAsteroids = Mathf.RoundToInt(currentLevel / difficultyConfigData.extraAsteroidFrequency);
        int asteroidNumber = 4 + extraAsteroids;
        asteroidNumber = 1;

        // Generate asteroids
        for (int i = 0; i < asteroidNumber; i++)
        {
            asteroidFactory.instantiate("RandomAsteroid");
        }

    }

}
