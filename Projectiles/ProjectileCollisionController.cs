using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;

        if(otherGameObject.layer != LayerMask.NameToLayer("BlackHoles"))
        {
            Destroy(gameObject);
        }
        
    }

}
