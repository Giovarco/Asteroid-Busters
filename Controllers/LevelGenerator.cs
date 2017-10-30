using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [Tooltip("If it equals zero, then the first level will be one")]
    [ReadOnly]
    public int currentLevel;

    // Private
    AsteroidFactory asteroidFactory;
    DifficultyConfigurationData difficultyConfigData;
    GameConfigurationData gameConfigData;

    void Awake()
    {
        // Get references
        gameConfigData = GetComponent<AssetReferences>().gameConfigData;

        // Set the current level
        currentLevel = gameConfigData.startingLevel;

        // Get Factory
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();

        // Get Difficulty Configuration Data
        difficultyConfigData = GameObject.Find("Orchestrator").GetComponent<AssetReferences>().difficultyConfigData;

    }
    
    public void generateLevel()
    {

        // Define asteroid number
        int extraAsteroids = Mathf.RoundToInt(currentLevel / difficultyConfigData.extraAsteroidFrequency);
        int asteroidNumber = 4 + extraAsteroids;
        // asteroidNumber = 1;

        // Generate asteroids
        for(int i = 0; i < asteroidNumber; i++)
        {
            asteroidFactory.instantiate("RandomAsteroid");
        }

    }

}
