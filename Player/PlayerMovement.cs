using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    PlayerData playerData;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerData = AssetReferences.playerData; // Cannot stay in Awake()
    }

    void Update()
    {
        handleRotation();
    }

    void FixedUpdate()
    {
        handleForwardMovement();
    }

    void handleForwardMovement()
    {
        // Limit speed
        if (rb.velocity.magnitude > playerData.maxForwardSpeed)
        {
            // BUG: Sometime you got stuck at max speed with no chance to slow down. This -0.001f solves this problem, but we have to get to the bottom of this
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, playerData.maxForwardSpeed - 0.001f);
        }
        else
        {

            // Accelerate only forward
            float forwardAcceleration = Input.GetAxis("Vertical");
            if (forwardAcceleration > 0)
            {
                rb.AddForce(forwardAcceleration * transform.up * playerData.forwardSpeed);
            }

        }
    }

    void handleRotation()
    {
        // Get the current angles and obtain the new angles
        Vector3 currentAngles = transform.rotation.eulerAngles;
        float newExtraAngle = Input.GetAxis("Horizontal") * playerData.rotationSpeed * Time.deltaTime;
        float newZ = currentAngles.z - newExtraAngle;
        Quaternion newAngles = Quaternion.Euler(0, 0, newZ);

        // Apply angle changes
        transform.rotation = newAngles;
    }

}
