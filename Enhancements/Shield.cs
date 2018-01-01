using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Enhancement {

    void Start()
    {
        // The default state of the shield is "not active"
        stop();
    }

	public override void execute()
    {
        gameObject.SetActive(true);
    }

    public override void stop()
    {
        gameObject.SetActive(false);
    }

}
