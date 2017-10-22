using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    AsteroidFactory asteroidFactory;
    AsteroidProperties asteroidInfo;
    GameObject asteroidContainer;

    void Awake()
    {
        asteroidInfo = GetComponent<AsteroidProperties>();
    }

    void Start()
    {
        asteroidContainer = GameObject.Find("Asteroids");
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        createChild();
        createChild();
        Destroy(gameObject);
    }

    void createChild()
    {
        if(asteroidInfo.hp > 1)
        {
            asteroidFactory.instantiate("ChildAsteroid", gameObject);
        }
    }

}
