using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLeaving : MonoBehaviour {

    // Private
    SpriteRenderer sr;
    float widthSprite;
	float heightSprite;
    GameSettings gameSettings;
    float offset;
    [SerializeField]
    float upperVisualLimit;
    [SerializeField]
    float lowerVisualLimit;
    [SerializeField]
    float RightVisualLimit;
    [SerializeField]
    float LeftVisualLimit;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        widthSprite = sr.bounds.size.x;
        heightSprite = sr.bounds.size.y;
        offset = 0.001f;
        gameSettings = GameObject.Find("Orchestrator").GetComponent<GameSettings>();
    }

    void Start () {
        updateVisualLimits();
    }

    public void updateVisualLimits()
    {
        upperVisualLimit = gameSettings.upperEdge + heightSprite / 2;
        lowerVisualLimit = gameSettings.lowerEdge - heightSprite / 2;
        RightVisualLimit = gameSettings.rightEdge + widthSprite / 2;
        LeftVisualLimit = gameSettings.leftEdge - widthSprite / 2;
    }

    // Update is called once per frame
    void Update () {
        handleEdgeLeaving();
    }

    void handleEdgeLeaving()
    {

        // Get object position
        Vector3 pos = transform.position;

        // Upper edge
        if (pos.y > upperVisualLimit)
        {
            Vector3 newPos = new Vector3(pos.x, lowerVisualLimit + offset, 0);
            transform.position = newPos;
		}

        // Lower edge
        if (pos.y < lowerVisualLimit)
        {
            Vector3 newPos = new Vector3(pos.x, upperVisualLimit - offset, 0);
            transform.position = newPos;
        }

        // Right edge
        if(pos.x > RightVisualLimit)
        {
            Vector3 newPos = new Vector3(LeftVisualLimit + offset, pos.y, 0);
            transform.position = newPos;
        }

        // Left edge
        if (pos.x < LeftVisualLimit)
        {
            Vector3 newPos = new Vector3(RightVisualLimit - offset, pos.y, 0);
            transform.position = newPos;
        }

    }

}
