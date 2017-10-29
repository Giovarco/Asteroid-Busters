﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

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

    public enum Status
    {
        Ok,
        Teleporting
    };


    void Awake()
    {

        // Calculate edge positions
        screenRatio = (float)Screen.width / (float)Screen.height;

        upperEdge = Camera.main.orthographicSize;
        lowerEdge = -upperEdge;

        rightEdge = Camera.main.orthographicSize * screenRatio;
        leftEdge = -rightEdge;

        // Calculate asteroid spawn offset
        asteroidSpawnOffset = rightEdge / 3;

    }

}
