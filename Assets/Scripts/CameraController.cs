using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ~The purpose of this script is handle in game camera movements and limitaions~
 * The key functions involved are the ability of the camera to follow the player character, and 
 * setting bounds to limit camera movement to within each scene.
 */

public class CameraController : MonoBehaviour
{

    private PlayerController player;
    public BoxCollider2D boundsBox;

    private float halfHeight, halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        //finds object in any scene that has the "PlayerController" script attached to it
        player = FindObjectOfType<PlayerController>();

        //~Note~ "Camera.main" accesses main camera in use in each scene -is no longer a bad function to use-
        halfHeight = Camera.main.orthographicSize; 
        halfWidth = halfHeight * Camera.main.aspect;

        AudioManager.instance.PlayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        //enables camera to follow player within the cameras bounds
        if (player != null) //if player is identified/ "alive" -prevents errors-
        {
            transform.position = new Vector3(
               Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
               Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y, boundsBox.bounds.max.y),
                transform.position.z);
        } else
        {
            player = FindObjectOfType<PlayerController>();
        }
    }
}
