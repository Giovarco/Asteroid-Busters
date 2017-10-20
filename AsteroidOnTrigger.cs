using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidOnTrigger : MonoBehaviour {

    Factory factory;
    AsteroidInformation asteroidInfo;
    GameObject asteroidContainer;

    void Awake()
    {
        asteroidInfo = GetComponent<AsteroidInformation>();
    }

    void Start()
    {
        asteroidContainer = GameObject.Find("Asteroids");
        factory = GameObject.Find("Orchestrator").GetComponent<Factory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        createChild();
        createChild();
        Destroy(gameObject);

    }

    void createChild()
    {
        factory.produce("childAsteroid", gameObject);
    }

}
