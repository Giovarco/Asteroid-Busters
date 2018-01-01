using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementController : MonoBehaviour {

    Enhancement enhancement;

    void Awake()
    {

        // Check what enhancement is the playership able to use
        Shield shield = GetComponent<Shield>();
        if(shield != null)
        {
            enhancement = shield;
        }

    }

	public void useEnhancement()
    {
        enhancement.execute();
    }

    public void stop()
    {
        enhancement.stop();
    }

}
