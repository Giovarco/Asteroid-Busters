using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeSizeChanger : MonoBehaviour {

    public IEnumerator changeSize(float startingSize, float finalSize, float sizeChangeDuration)
    {

        float addend;

        if (startingSize <= finalSize)
        {
            addend = (finalSize - startingSize) * Time.deltaTime / sizeChangeDuration;

            for (float actualSize = startingSize; actualSize < finalSize; actualSize += addend)
            {
                transform.localScale = new Vector3(actualSize, actualSize, 1);
                yield return null;
            }
        }
        else
        {
            addend = (startingSize - finalSize) * Time.deltaTime / sizeChangeDuration;
            for (float actualSize = startingSize; actualSize > finalSize; actualSize -= addend)
            {
                transform.localScale = new Vector3(actualSize, actualSize, 1);
                yield return null;
            }
        }

    }

}
