﻿using System; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class GameOrchestrator : MonoBehaviour
{

    public GameObject levelText;

    GameObject asteroidContainer;
    AsteroidFactory asteroidFactory;
    BlackHoleData blackHoleData;
    BlackHoleFactory blackHoleFactory;
    LevelGenerationData levelGenerationData;
    LevelGenerator levelGenerator;
    SpaceDistortionEffectData spaceDistortionEffectData;



    void Awake()
    {
        // Get internal references 
        levelGenerator = GetComponent<LevelGenerator>();

        // Get in-the-scene references 
        asteroidContainer = GameObject.Find("Asteroids");
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();
        blackHoleFactory = GameObject.Find("Factories").GetComponent<BlackHoleFactory>();
    }

    void OnEnable()
    {
        EventManager.StartListening("AsteroidDestroyed", isLevelCompletedWrapper);
    }

    void Start()
    {
        // Get references 
        blackHoleData = AssetReferences.blackHoleData;
        spaceDistortionEffectData = AssetReferences.spaceDistortionEffectData;
        levelGenerationData = AssetReferences.levelGenerationData;

        // Start coroutines 
        StartCoroutine( generateLevel() );
    }

    bool asteroidsExist()
    {
        int childCount = asteroidContainer.transform.childCount;

        if (childCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void createSpaceDistortion()
    {

        foreach (Transform asteroidTransform in asteroidContainer.transform)
        {

            // The asteroid to apply effects to
            GameObject asteroid = asteroidTransform.gameObject;

            // Components involved
            asteroid.AddComponent<SpriteFlash>();
            SpaceDistortionEffect spaceDistortionEffect = asteroid.AddComponent<SpaceDistortionEffect>();

            // Activate components
           StartCoroutine( spaceDistortionEffect.execute( spaceDistortionEffectData.turningSpeed ) );

        }

    }

    IEnumerator isLevelCompleted()
    {
        // Destroy(gameObject) is delayed, so skipping this frame is needed  
        yield return null;

        if (!asteroidsExist())
        {
            StartCoroutine( generateLevel() );
        }
    }

    void isLevelCompletedWrapper()
    {
        StartCoroutine( isLevelCompleted() );
    }

    IEnumerator generateLevel()
    {

        // Show the text with a the new level value 
        levelGenerator.currentLevel++;
        levelText.GetComponent<UnityEngine.UI.Text>().text = "Level " + levelGenerator.currentLevel;
        levelText.SetActive(true);

        // Set the new asteroid sprite 
        asteroidFactory.setNextSprite();

        // Wait some seconds 
        yield return new WaitForSeconds(levelGenerationData.levelTextDuration);

        // Deactive the level text 
        levelText.SetActive(false);

        // Wait a bunch of other seconds 
        yield return new WaitForSeconds(levelGenerationData.asteroidSpawnDelay);

        // Generate first level 
        levelGenerator.generateLevel();

        // yield return new WaitForSeconds(5f);

        createSpaceDistortion(); 
        //StartCoroutine( spawnBlackHole() );

    }

    IEnumerator spawnBlackHole()
    {
        while (true)
        {
            yield return new WaitForSeconds(blackHoleData.spawnFrequency);
            blackHoleFactory.instantiate("BlackHole");
        }

    } 

 
}