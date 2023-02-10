using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    public event Action GrowthComplete;

    // a serialized float called target size
    [SerializeField]
    private float targetSize = 1.0f;

    // a serialized float called growth duration
    [SerializeField]
    private float growthDuration = 1.0f;

    bool once = false;

    public void StartGrowing() {
        if (!once) { 
            // Start the coroutine
            StartCoroutine(Grow());
            once = true;
            GrowthComplete?.Invoke();
        }
    }

    // Write a coroutine that scales the object to the target size over the growth duration
    public IEnumerator Grow()
    {
        float elapsedTime = 0.0f;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(targetSize, targetSize, targetSize);
        while (elapsedTime < growthDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, (elapsedTime / growthDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }

}
