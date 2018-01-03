using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : Enhancement {

    void Start()
    {
        // The default state of the shield is "not active"
        stop();
    }

	public override void execute()
    {
        gameObject.SetActive(true);
    }

    public override void stop()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        GameObject other = o.gameObject;

        Vector2 otherVelocity = other.GetComponent<Rigidbody2D>().velocity;

        gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = otherVelocity;

    }

}
