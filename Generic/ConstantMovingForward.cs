using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovingForward : MonoBehaviour {

    public float speed;
	
	void Start () {
        GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector2.up * speed;
	}

}
