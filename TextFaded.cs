using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFaded : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<CanvasRenderer>().SetAlpha(.5f);
    }

}
