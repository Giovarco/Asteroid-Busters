using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

    public float startingSize;
    public float finalSize;
    public float growingDuration;

	void Start () {
        StartCoroutine(blackHoleLife());
	}

    IEnumerator blackHoleLife()
    {
        yield return StartCoroutine(changeSize(startingSize, finalSize));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(changeSize(finalSize, startingSize));
    }

    IEnumerator changeSize(float startingSize, float finalSize)
    {

        float addend;

        if( startingSize <= finalSize )
        {
            addend = (finalSize - startingSize)*Time.deltaTime / growingDuration;

            for (float actualSize = startingSize; actualSize < finalSize; actualSize += addend) {
                transform.localScale = new Vector3(actualSize, actualSize, 1);
                yield return null;
            }
        }
        else
        {
            addend = (startingSize - finalSize) * Time.deltaTime / growingDuration;
            print(addend);
            for (float actualSize = startingSize; actualSize > finalSize; actualSize -= addend)
            {
                transform.localScale = new Vector3(actualSize, actualSize, 1);
                yield return null;
            }
        }

    }

}
