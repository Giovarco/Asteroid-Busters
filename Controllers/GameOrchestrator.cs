using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject levelText;

    GameObject asteroidContainer;
    AsteroidFactory asteroidFactory;
    BlackHoleData blackHoleData;
    BlackHoleFactory blackHoleFactory;
    LevelGenerator levelGenerator;

    void Awake()
    {
        // Get internal references
        blackHoleData = GetComponent<AssetReferences>().blackHoleData;
        levelGenerator = GetComponent<LevelGenerator>();

        // Get in-the-scene references
        asteroidContainer = GameObject.Find("Asteroids");
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();
        blackHoleFactory = GameObject.Find("Factories").GetComponent<BlackHoleFactory>();
    }

    void OnEnable()
    {
        EventManager.StartListening("AsteroidDestroyed", isLevelCompletedWrapper);
        print("GameOrchestrator: subscribed to AsteroidDestroyed");
    }

    void Start()
    {
        StartCoroutine(generateLevel());
        StartCoroutine(spawnBlackHole());
    }

    bool asteroidsExist()
    {
        int childCount = asteroidContainer.transform.childCount;
        print("GameOrchestrator: childcount = "+childCount);
        if (childCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator isLevelCompleted()
    {
        yield return null;
        print("GameOrchestrator: isLevelCompleted()");
        print("GameOrchestrator: asteroidsExist() = " + asteroidsExist());
        print("GameOrchestrator: generateLevel() = " + generateLevel());
        // If there are no more asteroid, start a new level
        if (!asteroidsExist())
        {
            print("GameOrchestrator: there are no more asteroids! I'll generate a new level!");
            StartCoroutine(generateLevel());
        }
    }

    void isLevelCompletedWrapper()
    {
        StartCoroutine(isLevelCompleted());
    }

    IEnumerator generateLevel()
    {
        print("GameOrchestrator: generateLevel()");
        // Show the text with a the new level value
        levelGenerator.currentLevel++;
        levelText.GetComponent<UnityEngine.UI.Text>().text = "Level " + levelGenerator.currentLevel;
        levelText.SetActive(true);

        // Set the new asteroid sprite
        asteroidFactory.setNextSprite();

        // Wait some seconds
        yield return new WaitForSeconds(4f);

        // Deactive the level text
        levelText.SetActive(false);

        // Wait a bunch of other seconds
        yield return new WaitForSeconds(1.5f);

        // Generate first level
        levelGenerator.generateLevel();

        yield return new WaitForSeconds(0.5f);

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
