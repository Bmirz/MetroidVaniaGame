using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShooter : MonoBehaviour
{

    public float shotDelay;
    private float shotCounter;

    public BossBullet shotToFire;
    public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        } else
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation);
            shotCounter = shotDelay;
        }
    }
}
