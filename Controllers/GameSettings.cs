using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    [ReadOnly]
    public float upperEdge;

    [ReadOnly]
    public float lowerEdge;

    [ReadOnly]
    public float rightEdge;

    [ReadOnly]
    public float leftEdge;

    [ReadOnly]
    float screenRatio;

    /*
    public enum Status
    {
        Ok,
        Teleporting
    };
    */

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
