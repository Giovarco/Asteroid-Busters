using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    // Game information
    [Header("Game information")]

    [Tooltip("Default value: 0")]
    public int levelNumber;

    [HideInInspector]
    public float upperEdge;

    [HideInInspector]
    public float lowerEdge;

    [HideInInspector]
    public float rightEdge;

    [HideInInspector]
    public float leftEdge;

    float screenRatio;

    // Gameplay factors
    [Header("Gameplay factors")]

    [Tooltip("The lower, the harder")]
    public float asteroidIncreaseInSpeedFactor;

    [Tooltip("2 = 50%, 4 = 25% etc.")]
    public float sizeReductionFactor;

    [Tooltip("How many levels to wait before adding one asteroid")]
    public int asteroidIncreaseInNumberFactor; // 

    [Tooltip("Asteroid spawn offset from right/left edge")]
    public float offsetEdge;

    // Asteroid statistics
    [Header("Asteroid statistics")]

    public float baseAsteroidSpeed;

    [Tooltip("This variable prevents all the asteroid to have all the same speed (not intended to exceed maximum speed)")]
    public float speedVariance;

    void Awake()
    {
        // Calculate other useful information
        screenRatio = (float)Screen.width / (float)Screen.height;

        // Calculate edge positions
        upperEdge = Camera.main.orthographicSize;
        lowerEdge = -upperEdge;

        rightEdge = Camera.main.orthographicSize * screenRatio;
        leftEdge = -rightEdge;
    }
}
