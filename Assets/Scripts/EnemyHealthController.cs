using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public int totalHealth = 3;

    public GameObject deathEffect;

    //damages enemy, handles enemy death with effect if enemy health falls below threshold.
    public void DamageEnemy(int damageAmount)
    {

        totalHealth -= damageAmount;

        if (totalHealth <= 0)
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);

            }

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(10);//playes enemy explode sound
        }
    }
}
