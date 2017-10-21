using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
    
    GameSettings gameSettings;
    GameObject asteroidContainer;

    GameObject player;

    public GameObject bullet;

    public GameObject asteroid;
    public int asteroidSpritesIndex;
    public Sprite[] asteroidSprites;

    void Awake()
    {
        // Get game settings
        gameSettings = GetComponent<GameSettings>();

        // Get the asteroid containers
        asteroidContainer = GameObject.Find("Asteroids");

    }

    void Start()
    {
        // Get player
        player = GameObject.Find("PlayerShip");

    }



    public void produce(string name, GameObject obj = null)
    {
        if(obj == null)
        {
            if (name == "RandomAsteroid")
            {
                generateRandomAsteroid();
            }
            else if (name == "Bullet")
            {
                generateBullet();
            }
            else
            {
                print("No idea what '" + name + "' is.");
            }
        }
        else
        {
            if (name == "childAsteroid")
            {
                generateChildAsteroid(obj);
            }
            else
            {
                print("No idea what '" + name + "' is.");
            }
        }

    }

    void generateRandomAsteroid()
    {

        // Define asteroid speed
        float asteroidSpeed = gameSettings.baseAsteroidSpeed + (float)gameSettings.levelNumber / gameSettings.asteroidIncreaseInSpeedFactor;

        // Calculate spawn zones
        float newX;
        float newY = UnityEngine.Random.Range(gameSettings.lowerEdge, gameSettings.upperEdge);

        // Choose to spawn on the right or on the left
        if (getRandomBoolean())
        {
            // Right
            newX = UnityEngine.Random.Range(gameSettings.rightEdge - gameSettings.offsetEdge, gameSettings.rightEdge);
        }
        else
        {
            // Left
            newX = UnityEngine.Random.Range(gameSettings.leftEdge, gameSettings.leftEdge + gameSettings.offsetEdge);
        }

        // Create the asteroid
        GameObject newAsteroid = Instantiate(asteroid, new Vector3(newX, newY, 0), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));

        // Set variables
        AsteroidInformation newAsteroidInfo = newAsteroid.GetComponent<AsteroidInformation>();
        newAsteroidInfo.xSpeed = getPossibleMinusSign() * UnityEngine.Random.Range(asteroidSpeed - gameSettings.speedVariance, asteroidSpeed);
        newAsteroidInfo.ySpeed = getPossibleMinusSign() * UnityEngine.Random.Range(asteroidSpeed - gameSettings.speedVariance, asteroidSpeed);

        // Set asteroid appearance
        newAsteroid.GetComponent<SpriteRenderer>().sprite = asteroidSprites[asteroidSpritesIndex];

        // Set the new asteroid to be children of the asteroid container
        newAsteroid.transform.parent = asteroidContainer.transform;

    }

    bool getRandomBoolean()
    {
        return UnityEngine.Random.Range(0f, 1f) > 0.5f;
    }

    public void generateChildAsteroid(GameObject parentAsteroid)
    {

        // Get parent asteroid information
        AsteroidInformation parentAsteroidInfo = parentAsteroid.GetComponent<AsteroidInformation>();

        // Check if the parent asteroid can be divided
        if (parentAsteroidInfo.hp <= 1)
        {
            return;
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
        newAsteroidInfo.xSpeed = getPossibleMinusSign() * (parentAsteroidInfo.xSpeed - UnityEngine.Random.Range(0, gameSettings.speedVariance));
        newAsteroidInfo.ySpeed = getPossibleMinusSign() * (parentAsteroidInfo.ySpeed - UnityEngine.Random.Range(0, gameSettings.speedVariance));

        // Set asteroid appearance
        newAsteroid.GetComponent<SpriteRenderer>().sprite = parentAsteroid.GetComponent<SpriteRenderer>().sprite;

        // Set the new asteroid to be children of the asteroid container
        newAsteroid.transform.parent = asteroidContainer.transform;

    }

    void generateBullet()
    {
        // Get bullet height
        SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        float bulletHeight = bulletSpriteRenderer.transform.localScale.y;

        Vector3 bulletOffset = player.transform.rotation * new Vector3(0, bulletHeight, 0);
        Instantiate(bullet, player.transform.position + bulletOffset, player.transform.rotation);

    }

    int getPossibleMinusSign()
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