using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleFactory : MonoBehaviour {

    public GameObject blackHole;

    GameSettings gameSettings;

    void Awake()
    {
        // Get game settings
        gameSettings = GameObject.Find("Orchestrator").GetComponent<GameSettings>();

    }

    public GameObject instantiate(string name)
    {

        if (name == "BlackHole")
        {
            return generateBlackHole();
        }

        throw new System.ArgumentException(name + " cannot be recognised");
    }

    GameObject generateBlackHole()
    {
        return Instantiate(blackHole, getRandomPosition(), Quaternion.identity);
    }

    Vector3 getRandomPosition()
    {
        float xPos = UnityEngine.Random.Range(gameSettings.leftEdge, gameSettings.rightEdge);
        float yPos = UnityEngine.Random.Range(gameSettings.lowerEdge, gameSettings.upperEdge);
        return new Vector3(xPos, yPos, 1);
    }
}
