using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementController : MonoBehaviour {

    Enhancement enhancement;

    void Awake()
    {

        // Check what enhancement is the playership able to use
        ShieldController shield = GetComponent<ShieldController>();
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
