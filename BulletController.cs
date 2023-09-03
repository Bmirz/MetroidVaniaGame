using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D myRigidBody;

    public Vector2 moveDirX;
    public Vector2 moveDirY;

    public GameObject impactEffect;

    public int damageAmount = 1;

    // Update is called once per frame
    void Update()
    {
        //bullet speed based on direction
        myRigidBody.velocity = moveDirX * bulletSpeed;
        myRigidBody.velocity = moveDirY * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") //regesters damage on enemies
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }

        if(other.tag == "Boss") //registers damage on boss
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }

        //calls particle effect on bullet collision
        if(impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity); //~Note~ Quaternion.identity simply means no rotation
        }

        AudioManager.instance.PlaySFXAdjusted(11);//playes bullet impact sound

        Destroy(gameObject); //destroys bullet
    }

    //When object (bullet) goes off screen, destroy game object
    private void OnBecameInvisible()// ~Note~ "OnBecameInvisible" is a built in function that handles anything that goes of the game screen
    {
        Destroy(gameObject);
    }
}
