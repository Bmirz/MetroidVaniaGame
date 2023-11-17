using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public float timeToExplode = .5f;
    public GameObject explosion;

    public float blastRange;
    public LayerMask whatIsDestructable;

    public int damageAmount;
    public LayerMask whatIsDamageable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if(timeToExplode <= 0)
        {
            if(explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            Destroy(gameObject);

            //creates a list of all the objects within the blast radius that we want to "explode" or delete from game
            Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructable);

            //handles destroying destructible objects with bomb explosion
            if(objectsToRemove.Length > 0)
            {
                foreach(Collider2D col in objectsToRemove)
                {
                    Destroy(col.gameObject);
                }
            }

            //Handles bombs damaging enemies
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDamageable);

            foreach(Collider2D col in objectsToDamage)
            {
                EnemyHealthController enemyHealth = col.GetComponent<EnemyHealthController>();
                if(enemyHealth != null)
                {
                    enemyHealth.DamageEnemy(damageAmount);
                }
            }

            AudioManager.instance.PlaySFXAdjusted(10);//playes enemy explode sound

        }
    }
}