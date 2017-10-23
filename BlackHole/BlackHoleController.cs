using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

    public float startingSize;
    public float finalSize;
    public float sizeChangeDuration;

    overTimeSizeChanger sizeChanger;

    void Awake()
    {
        sizeChanger = GetComponent<overTimeSizeChanger>();
    }

	void Start () {
        StartCoroutine(blackHoleLife());
	}

    IEnumerator blackHoleLife()
    {
        yield return StartCoroutine(sizeChanger.changeSize(startingSize, finalSize, sizeChangeDuration));
        yield return new WaitForSeconds(sizeChangeDuration);
        yield return StartCoroutine(sizeChanger.changeSize(finalSize, startingSize, sizeChangeDuration));
    }


}
