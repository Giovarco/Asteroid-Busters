using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollisionController : MonoBehaviour {

    AsteroidFactory asteroidFactory;
    AsteroidProperties asteroidInfo;

    void Awake()
    {
        asteroidInfo = GetComponent<AsteroidProperties>();
    }

    void Start()
    {
        asteroidFactory = GameObject.Find("Factories").GetComponent<AsteroidFactory>();
    }

    void createChild()
    {
        if (asteroidInfo.hp > 1)
        {
            asteroidFactory.instantiate("ChildAsteroid", gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("BlackHoles") && other.gameObject.tag != "Immaterial")
        {
            createChild();
            createChild();
            Destroy(gameObject);

            EventManager.TriggerEvent("AsteroidDestroyed");

        }

    }

}
