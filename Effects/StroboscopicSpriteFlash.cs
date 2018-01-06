using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroboscopicSpriteFlash : MonoBehaviour {

    Cooldown flashCd;
    SpriteFlash spriteFlash;

	void Awake () {
        flashCd = new Cooldown(0.04f);
        spriteFlash = gameObject.AddComponent<SpriteFlash>();
	}
	
	void Update () {
		
	}

    public void execute()
    {

        // Check if you can activate / deactivate the flash
        if (flashCd.isAvailable())
        {

            // Start the cooldown
            flashCd.start();

            // Flash or remove flash
            if(spriteFlash.getFlashAmount() >= 1f)
            {
                spriteFlash.execute(0f, Color.white);
            } else
            {
                spriteFlash.execute(1f, Color.white);
            }

        }

    }

}
