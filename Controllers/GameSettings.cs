﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    // References
    GameConfigurationData gameConfigData;
    AsteroidData asteroidData;

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

    // Asteroid configuration
    /*
    [Header("Asteroid configuration")]
    public float baseAsteroidSpeed;

    [Tooltip("2 = 50%, 4 = 25% etc.")]
    public float sizeReductionFactor;

    [Tooltip("This variable prevents all the asteroid to have all the same speed (not intended to exceed maximum speed)")]
    public float speedVariance;
    */

    // Asteroid difficulty factors
    [Header("Difficulty configuration")]

    [Tooltip("Number of levels before adding one asteroid")]
    public int extraAsteroidFrequency;

    [Tooltip("Needed to calculate the asteroid increase-in-speed factor")]
    public float hardLevel;

    [Tooltip("Needed to calculate the asteroid increase-in-speed factor")]
    public float hardAsteroidSpeed;

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
        asteroidIncreaseInSpeedFactor = hardLevel / (hardAsteroidSpeed - asteroidData.baseSpeed);
    }

}
