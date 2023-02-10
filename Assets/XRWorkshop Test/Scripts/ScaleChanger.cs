using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

/// <summary>
/// The role of the scale changer component is to move the player inside the forest once all of the restoration interactions have been completed.
/// </summary>
public class ScaleChanger : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float targetSize;

    private bool once = false;

    public void MoveAndScalePlayer()
    {
        if (!once) {
            // Move the player to the target position
            transform.position = target.position;

            // Scale the player to the target size
            transform.localScale = new Vector3(targetSize, targetSize, targetSize);

            once = true;
        }
        
    }
}
