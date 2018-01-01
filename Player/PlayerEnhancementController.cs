using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnhancementController : MonoBehaviour {

    EnhancementController enhancementController;

    void Awake()
    {
        enhancementController = transform.Find("Enhancement").GetComponent<EnhancementController>();
    }

	void Update()
    {

        if( Input.GetKey(KeyCode.LeftAlt) )
        {
            enhancementController.useEnhancement();
        } else
        {
            enhancementController.stop();
        }

    }

}
