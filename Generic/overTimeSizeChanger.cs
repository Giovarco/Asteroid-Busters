using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeSizeChanger : MonoBehaviour {

    public IEnumerator changeSize(float startingValue, float finalValue, float changeDuration)
    {

        // Get the actual time
        float startingTime = Time.time;

        // Change size over time
        while (Time.time - startingTime < changeDuration)
        {
            float t = (Time.time - startingTime) / changeDuration;
            float actualValue = Mathf.Lerp(startingValue, finalValue, t);
            transform.localScale = new Vector3(actualValue, actualValue, 1);
            yield return null;
        }

    }

}
