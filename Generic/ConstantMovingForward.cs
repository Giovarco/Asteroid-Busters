using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovingForward : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        Vector3 currentPosition = transform.position;
        Vector3 velocity = Vector3.up * Time.deltaTime * speed;
        Vector3 newPosition = currentPosition + transform.rotation * velocity;
        transform.position = newPosition;
	}

}
