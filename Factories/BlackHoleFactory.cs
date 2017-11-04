using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleFactory : MonoBehaviour {

    public GameObject blackHole;

    public GameObject instantiate(string name)
    {

        if (name == "BlackHole")
        {
            return generateBlackHole();
        }

        throw new System.ArgumentException(name + " cannot be recognised");
    }

    GameObject generateBlackHole()
    {
        return Instantiate(blackHole, Vector2.zero, Quaternion.identity);
    }

}
