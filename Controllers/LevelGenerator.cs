using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    // Private
    GameSettings gameSettings;
    AsteroidFactory asteroidFactory;
    DifficultyConfigurationData difficultyConfigData;

    void Awake()
    {
        // Get Factory
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();

        // Get Difficulty Configuration Data
        difficultyConfigData = GameObject.Find("Orchestrator").GetComponent<AssetReferences>().difficultyConfigData;

        // Get game settings
        gameSettings = GetComponent<GameSettings>();
    }
    
    public void generateLevel()
    {

        // Define asteroid number
        int extraAsteroids = Mathf.RoundToInt(gameSettings.currentLevel / difficultyConfigData.extraAsteroidFrequency);
        int asteroidNumber = 4 + extraAsteroids;
        asteroidNumber = 1;

        // Generate asteroids
        for(int i = 0; i < asteroidNumber; i++)
        {
            asteroidFactory.instantiate("RandomAsteroid");
        }

    }

}
