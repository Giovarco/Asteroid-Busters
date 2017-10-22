using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour {

    GameSettings gameSettings;
    GameObject asteroidContainer;

    public GameObject asteroid;
    public int asteroidSpritesIndex;
    public Sprite[] asteroidSprites;

    void Awake()
    {
        // Get game settings
        gameSettings = GameObject.Find("Orchestrator").GetComponent<GameSettings>();

        // Get the asteroid containers
        asteroidContainer = GameObject.Find("Asteroids");

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

        throw new System.ArgumentException(name+" not found");

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

    Vector3 getRandomDirection()
    {
        float xDirection = getRandomSign() * UnityEngine.Random.Range(1f - gameSettings.speedVariance, 1f);
        float yDirection = getRandomSign() * UnityEngine.Random.Range(1f - gameSettings.speedVariance, 1f);
        Vector3 randomDirection = new Vector3(xDirection, yDirection, 0);
        return randomDirection;
    }

    float getAsteroidY()
    {
        return UnityEngine.Random.Range(gameSettings.lowerEdge, gameSettings.upperEdge);
    }

    float getAsteroidX()
    {
        // Choose to spawn on the right or on the left
        if (getRandomBoolean())
        {
            // Right
            return UnityEngine.Random.Range(gameSettings.rightEdge - gameSettings.offsetEdge, gameSettings.rightEdge);
        }
        else
        {
            // Left
            return UnityEngine.Random.Range(gameSettings.leftEdge, gameSettings.leftEdge + gameSettings.offsetEdge);
        }
    }

    GameObject generateChildAsteroid(GameObject parentAsteroid)
    {

        // Define asteroid speed
        float asteroidSpeed = getAsteroidSpeed();

        // Get parent asteroid information
        AsteroidInformation parentAsteroidInfo = parentAsteroid.GetComponent<AsteroidInformation>();

        // Check if the parent asteroid can be divided
        if (parentAsteroidInfo.hp <= 1)
        {
            return null;
        }

        // Instatiate the new asteroid and get its information
        GameObject newAsteroid = Instantiate(asteroid, parentAsteroid.transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
        AsteroidInformation newAsteroidInfo = newAsteroid.GetComponent<AsteroidInformation>();

        // Set new asteroid size
        float localScaleValue = parentAsteroid.transform.localScale.x;
        float newSize = localScaleValue / gameSettings.sizeReductionFactor;
        newAsteroid.transform.localScale = new Vector3(newSize, newSize, 1);

        // Set new HP
        newAsteroidInfo.hp = parentAsteroidInfo.hp - 1;

        // Set Speed
        //newAsteroidInfo.xSpeed = getPossibleMinusSign() * (parentAsteroidInfo.xSpeed - UnityEngine.Random.Range(0, gameSettings.speedVariance));
        //newAsteroidInfo.ySpeed = getPossibleMinusSign() * (parentAsteroidInfo.ySpeed - UnityEngine.Random.Range(0, gameSettings.speedVariance));

        // Set variables
        float xSpeed = getRandomSign() * UnityEngine.Random.Range(1f - gameSettings.speedVariance, 1f);
        float ySpeed = getRandomSign() * UnityEngine.Random.Range(1f - gameSettings.speedVariance, 1f);
        Vector3 randomDirection = new Vector3(xSpeed, ySpeed, 0);
        newAsteroid.GetComponent<Rigidbody2D>().velocity = randomDirection * asteroidSpeed;

        // Set asteroid appearance
        newAsteroid.GetComponent<SpriteRenderer>().sprite = parentAsteroid.GetComponent<SpriteRenderer>().sprite;

        // Set the new asteroid to be children of the asteroid container
        newAsteroid.transform.parent = asteroidContainer.transform;

        return newAsteroid;
    }

    float getAsteroidSpeed()
    {
        return gameSettings.baseAsteroidSpeed + (float)gameSettings.levelNumber / gameSettings.asteroidIncreaseInSpeedFactor;
    }

    bool getRandomBoolean()
    {
        return UnityEngine.Random.Range(0f, 1f) > 0.5f;
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

    public int getAsteroidSpritesLength()
    {
        return asteroidSprites.Length;
    }

}
