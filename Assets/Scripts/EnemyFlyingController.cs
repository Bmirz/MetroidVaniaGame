using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{

    public float rangeToStartChase;
    private bool isChasing;

    public float moveSpeed, turnSpeed;

    private Transform player;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isChasing)
        {
            if(Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {
                isChasing = true;

                anim.SetBool("isChasing", isChasing);
            }
        }
        else
        {
            if(player.gameObject.activeSelf) //code for turning(rotating) the flyer towards the player, moving towards the player
            {
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//~Note~ this is the formula commonly used to work out direction in degrees
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);//~Note~ "Slerp" unclear?, special way to move rotations

                //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;

            }
        }
    }
}
