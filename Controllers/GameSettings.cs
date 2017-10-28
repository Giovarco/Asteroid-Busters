﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    // References
    GameConfigurationData gameConfigData;
    AsteroidData asteroidData;
    DifficultyConfigurationData difficultyConfigData;

    // Game information
    [Header("Game configuration")]

    [Tooltip("If it equals zero, then the first level will be one")]
    [ReadOnly]
    public int currentLevel;

    [Tooltip("Asteroid spawn offset from right/left edge")]
    [ReadOnly]
    public float asteroidSpawnOffset;

    [HideInInspector]
    public float upperEdge;

    [HideInInspector]
    public float lowerEdge;

    [HideInInspector]
    public float rightEdge;

    [HideInInspector]
    public float leftEdge;

    float screenRatio;

    // Asteroid difficulty factors
    [Header("Difficulty configuration")]
    /*
    [Tooltip("Number of levels before adding one asteroid")]
    [ReadOnly]
    public int extraAsteroidFrequency;

    [Tooltip("Needed to calculate the asteroid increase-in-speed factor")]
    [ReadOnly]
    public float hardLevel;

    [Tooltip("Needed to calculate the asteroid increase-in-speed factor")]
    [ReadOnly]
    public float hardAsteroidSpeed;
    */
    [Tooltip("The lower, the harder")]
    [ReadOnly]
    public float asteroidIncreaseInSpeedFactor;



    public enum Status
    {
        Ok,
        Teleporting
    };

    void Awake()
    {
        // Get references
        asteroidData = GetComponent<AssetReferences>().asteroidData;
        gameConfigData = GetComponent<AssetReferences>().gameConfigData;
        difficultyConfigData = GetComponent<AssetReferences>().difficultyConfigData;

        // Set the current level
        currentLevel = gameConfigData.startingLevel;

        // Calculate edge positions
        screenRatio = (float)Screen.width / (float)Screen.height;

        upperEdge = Camera.main.orthographicSize;
        lowerEdge = -upperEdge;

        rightEdge = Camera.main.orthographicSize * screenRatio;
        leftEdge = -rightEdge;

        // Calculate asteroid spawn offset
        asteroidSpawnOffset = rightEdge / 3;

        // Calculate others
        asteroidIncreaseInSpeedFactor = difficultyConfigData.hardLevel / (difficultyConfigData.hardAsteroidSpeed - asteroidData.baseSpeed);
    }

}
