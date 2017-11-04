using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLeaving : MonoBehaviour {

    float heightSprite;

    [SerializeField]
    float leftVisualLimit;

    [SerializeField]
    float lowerVisualLimit;

    float offset;

    [SerializeField]
    float rightVisualLimit;

    ScreenInformation screenInfo;

    SpriteRenderer sr;

    [SerializeField]
    float upperVisualLimit;

    float widthSprite;

    void Awake()
    {
        setSpriteSize();
        offset = 0.001f;
        screenInfo = GameObject.Find("Main Camera").GetComponent<ScreenInformation>();
    }

    void Start()
    {
        setVisualLimits();
    }

    void Update()
    {
        handleEdgeLeaving();
    }

    void setSpriteSize()
    {
        sr = GetComponent<SpriteRenderer>();
        widthSprite = sr.bounds.size.x;
        heightSprite = sr.bounds.size.y;
    }

    void setVisualLimits()
    {
        upperVisualLimit = screenInfo.upperEdge + heightSprite / 2;
        lowerVisualLimit = screenInfo.lowerEdge - heightSprite / 2;
        rightVisualLimit = screenInfo.rightEdge + widthSprite / 2;
        leftVisualLimit = screenInfo.leftEdge - widthSprite / 2;
    }

    public void updateVisualLimits()
    {
        setSpriteSize();
        setVisualLimits();
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
        if (pos.x > rightVisualLimit)
        {
            Vector3 newPos = new Vector3(leftVisualLimit + offset, pos.y, 0);
            transform.position = newPos;
        }

        // Left edge
        if (pos.x < leftVisualLimit)
        {
            Vector3 newPos = new Vector3(rightVisualLimit - offset, pos.y, 0);
            transform.position = newPos;
        }

    }

}
