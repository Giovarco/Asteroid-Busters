﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour {

    public GameObject asteroid;
    public Sprite[] asteroidSprites;
    public int asteroidSpritesIndex;

    GameObject asteroidContainer;
    AsteroidData asteroidData;
    float asteroidIncreaseInSpeedFactor;

    [Tooltip("Asteroid spawn offset from right/left edge")]
    [ReadOnly]
    float asteroidSpawnOffset;

    DifficultyConfigurationData difficultyConfigData;
    LevelGenerator levelGenerator;
    ScreenInformation screenInfo;

    void Awake()
    {
        // Get references
        GameObject orchestrator = GameObject.Find("Orchestrator");
        levelGenerator = orchestrator.GetComponent<LevelGenerator>();
        screenInfo = GameObject.Find("Main Camera").GetComponent<ScreenInformation>();
        asteroidData = orchestrator.GetComponent<AssetReferences>().asteroidData;
        difficultyConfigData = orchestrator.GetComponent<AssetReferences>().difficultyConfigData;
        asteroidContainer = GameObject.Find("Asteroids");

        // Initialize variables
        asteroidIncreaseInSpeedFactor = difficultyConfigData.hardLevel / (difficultyConfigData.hardAsteroidSpeed - asteroidData.baseSpeed);
        asteroidSpawnOffset = screenInfo.rightEdge / 3;

    }

    public int getAsteroidSpritesLength()
    {
        return asteroidSprites.Length;
    }

    public GameObject instantiate(string name, GameObject obj = null)
    {
        if (obj == null)
        {
            if (name == "RandomAsteroid")
            {
                return generateRandomAsteroid();
            }
        }
        else
        {
            if (name == "ChildAsteroid")
            {
                return generateChildAsteroid(obj);
            }
        }

        throw new ArgumentException(name + " cannot be recognised");

    }

    
    GameObject generateChildAsteroid(GameObject parentAsteroid)
    {

        // Instatiate
        GameObject newAsteroid = Instantiate(asteroid, parentAsteroid.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));

        // Get parent and child asteroid information for later
        AsteroidProperties newAsteroidInfo = newAsteroid.GetComponent<AsteroidProperties>();
        AsteroidProperties parentAsteroidInfo = parentAsteroid.GetComponent<AsteroidProperties>();

        // Define speed
        float asteroidSpeed = getAsteroidSpeed();

        // Set direction
        Vector3 randomDirection = getRandomDirection();
        newAsteroid.GetComponent<Rigidbody2D>().velocity = randomDirection * asteroidSpeed;

        // Set HP
        newAsteroidInfo.hp = parentAsteroidInfo.hp - 1;

        // Set size depeding on the parent asteroid
        float localScaleValue = parentAsteroid.transform.localScale.x;
        float newSize = localScaleValue / asteroidData.sizeReductionFactor;
        newAsteroid.transform.localScale = new Vector3(newSize, newSize, 1);

        // ONLY AFTER setting the size, re-define again edge leaving properties
        EdgeLeaving edgeLeaving = newAsteroid.GetComponent<EdgeLeaving>();
        edgeLeaving.updateSpriteSize();
        edgeLeaving.updateVisualLimits();

        // Set appearance
        newAsteroid.GetComponent<SpriteRenderer>().sprite = parentAsteroid.GetComponent<SpriteRenderer>().sprite;

        // Set the new asteroid to be children of the asteroid container
        newAsteroid.transform.parent = asteroidContainer.transform;

        return newAsteroid;
    }

    GameObject generateRandomAsteroid()
    {

        // Define speed
        float asteroidSpeed = getAsteroidSpeed();

        // Define spawn position
        float newX = getAsteroidX();
        float newY = getAsteroidY();

        // Instantiate
        GameObject newAsteroid = Instantiate(asteroid, new Vector3(newX, newY, 0), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));

        // Set velocity
        newAsteroid.GetComponent<Rigidbody2D>().velocity = getRandomDirection() * asteroidSpeed;

        // Set appearance
        newAsteroid.GetComponent<SpriteRenderer>().sprite = asteroidSprites[asteroidSpritesIndex];

        // Set the new asteroid to be children of the asteroid container
        newAsteroid.transform.parent = asteroidContainer.transform;

        return newAsteroid;
    }
    float getAsteroidSpeed()
    {
        return asteroidData.baseSpeed + (float)levelGenerator.currentLevel / asteroidIncreaseInSpeedFactor;
    }

    float getAsteroidX()
    {
        // Choose to spawn on the right or on the left
        if (getRandomBoolean())
        {
            // Right
            return UnityEngine.Random.Range(screenInfo.rightEdge - asteroidSpawnOffset, screenInfo.rightEdge);
        }
        else
        {
            // Left
            return UnityEngine.Random.Range(screenInfo.leftEdge, screenInfo.leftEdge + asteroidSpawnOffset);
        }
    }

    float getAsteroidY()
    {
        return UnityEngine.Random.Range(screenInfo.lowerEdge, screenInfo.upperEdge);
    }

    bool getRandomBoolean()
    {
        return UnityEngine.Random.Range(0f, 1f) > 0.5f;
    }

    Vector3 getRandomDirection()
    {
        float xDirection = getRandomSign() * UnityEngine.Random.Range(1f - asteroidData.speedVariance, 1f);
        float yDirection = getRandomSign() * UnityEngine.Random.Range(1f - asteroidData.speedVariance, 1f);
        Vector3 randomDirection = new Vector3(xDirection, yDirection, 0);
        return randomDirection;
    }
    int getRandomSign()
    {
        if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
