using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
    
    GameSettings gameSettings;

    GameObject player;

    public GameObject bullet;


    void Awake()
    {
        // Get game settings
        gameSettings = GetComponent<GameSettings>();

    }

    void Start()
    {
        // Get player
        player = GameObject.Find("PlayerShip");

    }



    GameObject produce(string name, GameObject obj = null)
    {

        if(obj == null)
        {
            if (name == "Bullet")
            {
                return generateBullet();
            }
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