using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigatable : MonoBehaviour
{
    public event Action NavigationComplete;

    [SerializeField]
    List<Transform> locations;

    private bool isMoving = false;

    private static readonly WaitForEndOfFrame wait = new();

    public void StartNavigation() {
        if (!isMoving) {
            isMoving = true;
            StartCoroutine(Navigate());
        }
    }

    IEnumerator Navigate() {
        foreach (Transform location in locations)
        {
            while (Vector3.Distance(transform.position, location.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, location.position, Time.deltaTime);
                yield return wait;
            }
        }
        NavigationComplete?.Invoke();
        // Stop the coroutine
        yield break;
    }
    
}
