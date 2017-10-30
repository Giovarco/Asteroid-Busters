using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLeaving : MonoBehaviour {

    // Private
    SpriteRenderer sr;
    float widthSprite;
	float heightSprite;
    ScreenInformation screenInfo;
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
        updateSpriteSize();
        offset = 0.001f;
        screenInfo = GameObject.Find("Main Camera").GetComponent<ScreenInformation>();
    }

    void Start () {
        updateVisualLimits();
    }

    public void updateSpriteSize()
    {
        sr = GetComponent<SpriteRenderer>();
        widthSprite = sr.bounds.size.x;
        heightSprite = sr.bounds.size.y;
    }

    public void updateVisualLimits()
    {
        upperVisualLimit = screenInfo.upperEdge + heightSprite / 2;
        lowerVisualLimit = screenInfo.lowerEdge - heightSprite / 2;
        RightVisualLimit = screenInfo.rightEdge + widthSprite / 2;
        LeftVisualLimit = screenInfo.leftEdge - widthSprite / 2;
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
