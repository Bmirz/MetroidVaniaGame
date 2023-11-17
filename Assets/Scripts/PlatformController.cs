using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public GameObject platformPathStart;
    public GameObject platformPathEnd;
    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private Rigidbody2D player;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance.myRigidBody;

        startPosition = platformPathStart.transform.position;
        endPosition = platformPathEnd.transform.position;
        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));

    }

    // Update is called once per frame
    void Update()
    {
        //moving platform back and forth
        if (transform.position == endPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }
        if (transform.position == startPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }

        /*if on platform is on point, wait a little bit, then move to next point in list
        myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);

        waitCounter -= Time.deltaTime;
        if (waitCounter <= 0)
        {
            waitCounter = waitAtPoint;

            currentPoint++;

            if (currentPoint >= movePoints.Length)
            {
                currentPoint = 0;
            }
        }
        */
    }

    //moving the player with the platform by setting in platform parent
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.transform.SetParent(gameObject.transform, true);
        player.interpolation = RigidbodyInterpolation2D.Extrapolate;
    }

    //removing player from platform parent once leaving the platform
    void OnCollisionExit2D(Collision2D other)
    {
        other.gameObject.transform.parent = null;
        player.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    //moving the platform
    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
}