using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCollisionController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        print("ENTER");
        other.gameObject.transform.position = new Vector3(-10,-10,0);
    }
}
