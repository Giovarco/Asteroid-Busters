using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovingForward : MonoBehaviour {

    public float speed;
	
	void Start () {
        // Do not move this line to Awake() otherwise it will not work
        GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector2.up * speed;
	}

}
