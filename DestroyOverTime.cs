using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ~The purpose of this script it to create an effect that causes objects to disappear over a given time~
 * This script allows reusable code that can be implemented for any game object that requires this feature.
 */

public class DestroyOverTime : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
