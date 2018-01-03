using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : Enhancement {

    Cooldown overheat;

    float capacity = 3;
    [SerializeField]
    float currentCharge;
    float consumption = 1;
    float rechargeSpeed = 1;


    void Start()
    {
        // The battery is full when the game starts
        currentCharge = capacity;

        // Instantiate references
        overheat = new Cooldown(2f);

        // The default state of the shield is "not active"
        stop();
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
                currentCharge -= consumption * Time.deltaTime;
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
            if (currentCharge < capacity)
            {
                currentCharge += rechargeSpeed * Time.deltaTime;
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
