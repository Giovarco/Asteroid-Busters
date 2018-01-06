using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : Enhancement {

    float currentCharge;
    Cooldown overheat;
    ShieldControllerData shieldControllerData;
    StroboscopicSpriteFlash stroboscopicSpriteFlash;


    void Start()
    {

        shieldControllerData = AssetReferences.shieldControllerData;

        // The battery is full when the game starts
        currentCharge = shieldControllerData.capacity;

        // Instantiate references
        overheat = new Cooldown(shieldControllerData.overheatDuration);
        stroboscopicSpriteFlash = GetComponent<StroboscopicSpriteFlash>();

        // The default state of the shield is "not active"
        stop();
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            if(currentCharge < shieldControllerData.stroboscopicSpriteFlashStart)
            {
                stroboscopicSpriteFlash.execute();
            }
        }
    }

	public override void execute()
    {
        // Check if there is energy
        if(!overheat.isOnCooldown())
        {
            if (currentCharge > 0)
            {
                // Activate shield
                gameObject.SetActive(true);

                // Consume energy
                currentCharge -= shieldControllerData.consumption * Time.deltaTime;
            }
            else
            {
                stop();
                overheat.start();
            }
        }

    }

    public override void stop()
    {
        // Deactivate shield
        gameObject.SetActive(false);

        // Recharge energy
        if(!overheat.isOnCooldown())
        {
            if (currentCharge < shieldControllerData.capacity)
            {
                currentCharge += shieldControllerData.rechargeSpeed * Time.deltaTime;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D o)
    {
        // Get the velocity of the other GO (e.g. an asteroid)
        GameObject other = o.gameObject;
        Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;

        // The player ship is affected by the impact
        GameObject playerShip = gameObject.transform.parent.gameObject;
        playerShip.GetComponent<Rigidbody2D>().velocity = otherVelocity;

    }

}
