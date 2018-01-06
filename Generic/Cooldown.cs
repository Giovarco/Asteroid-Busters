using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown {

    float duration;
    float cooldownStart;

    public Cooldown(float duration)
    {
        this.duration = duration;
    }

    public void start()
    {
        cooldownStart = Time.time;
    }

    public bool isOnCooldown()
    {
        return Time.time - cooldownStart < duration;
    }

    public bool isAvailable()
    {
        return !isOnCooldown();
    }
}
