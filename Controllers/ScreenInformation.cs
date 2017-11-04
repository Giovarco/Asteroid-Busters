using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInformation : MonoBehaviour {

    [ReadOnly]
    public float leftEdge;

    [ReadOnly]
    public float lowerEdge;

    [ReadOnly]
    public float rightEdge;

    [ReadOnly]
    public float upperEdge;

    [ReadOnly]
    float screenRatio;

    void Awake()
    {

        // Calculate edge positions
        screenRatio = (float)Screen.width / (float)Screen.height;

        upperEdge = Camera.main.orthographicSize;
        lowerEdge = -upperEdge;

        rightEdge = Camera.main.orthographicSize * screenRatio;
        leftEdge = -rightEdge;

    }

}
