using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    public float moveSpeed;

    public Rigidbody2D myRigidBody;

    public int damageAmount;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//~Note~ this is the formula commonly used to work out direction in degrees
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        AudioManager.instance.PlaySFXAdjusted(12);//boss shooting audio
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = -transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.damagePlayer(damageAmount);
        }

        if(impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        AudioManager.instance.PlaySFXAdjusted(11);//bullet impact sound
    }
}
