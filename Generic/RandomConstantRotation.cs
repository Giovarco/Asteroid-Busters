﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomConstantRotation : MonoBehaviour {

    public float maxRotation = 200;
    public float minRotation = 50;
    float rotationSpeed;

	void Awake () {
        rotationSpeed = Random.Range(minRotation, maxRotation);
	}
	
	void Update () {

        // Get the current angles and obtain the new angles
        Vector3 currentAngles = transform.rotation.eulerAngles;
        float newExtraAngle = rotationSpeed * Time.deltaTime;
        float newZ = currentAngles.z - newExtraAngle;
        Quaternion newAngles = Quaternion.Euler(0, 0, newZ);

        // Apply angle changes
        transform.rotation = newAngles;

    }
}
