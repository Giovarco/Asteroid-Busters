using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour {

    public GameObject bullet;

    GameSettings gameSettings;
    GameObject player;

    void Awake()
    {
        // Get game settings
        gameSettings = GameObject.Find("Orchestrator").GetComponent<GameSettings>();

    }

    void Start()
    {
        // Get player
        player = GameObject.Find("PlayerShip");

    }

    public GameObject instantiate(string name)
    {

        if (name == "RedBullet1")
        {
            return generateBullet();
        }

        throw new ArgumentException(name + " cannot be recognised");
    }

    GameObject generateBullet()
    {
        // Get bullet height
        SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        float bulletHeight = bulletSpriteRenderer.transform.localScale.y;

        Vector3 bulletOffset = player.transform.rotation * new Vector3(0, bulletHeight, 0);
        GameObject newBullet = Instantiate(bullet, player.transform.position + bulletOffset, player.transform.rotation);

        return newBullet;
    }

}