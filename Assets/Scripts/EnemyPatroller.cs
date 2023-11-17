using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{

    public Transform[] patrolPoints;
    private int currentPoint;


    public float moveSpeed, waitAtPoint;
    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D myRigidBody;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;

        //@ start of game, set patrolpoint's parent to null so that the points do not move from original position and follow the walker enemy instead.
        foreach(Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //similar to player movement, handles sprite turning
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
                transform.localScale = Vector3.one;
            }

            //handles walker moving towards point on the Y axis
            if (transform.position.y < patrolPoints[currentPoint].position.y -.5f && myRigidBody.velocity.y < .1f)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
            }
        }
        else
        {
            //if on walker is on point, wait a little bit, then move to next point in list
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);

            waitCounter -= Time.deltaTime;
            if(waitCounter <= 0)
            {
                waitCounter = waitAtPoint;

                currentPoint++;

                if(currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }

        }
        //animates walking if walker is moving
        anim.SetFloat("speed", Mathf.Abs(myRigidBody.velocity.x));

    }
}
