using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public int damageAmount = 1;

    public bool destoryOnDamage;
    public GameObject destroyEffect;

    //deals damage to player on collision, paired with trigger to make scrip more universal
    private void OnCollisionStay2D(Collision2D other) //for walker's attack
    {
        if(other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//for flyer's attack
    {
        if(other.tag == "Player")
        {
            DealDamage();
        }

    }

    //calling player health controller, deal X amount of damage to player health
    void DealDamage()
    {
            PlayerHealthController.instance.damagePlayer(damageAmount);

        if(destoryOnDamage)//handles flyer death w/ effects on impact with player
        {
            if(destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
