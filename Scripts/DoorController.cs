using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public Animator anim;

    public float distanceToOpen;

    private PlayerController thePlayer;

    private bool playerExiting;

    public Transform exitPoint;
    public float movePlayerSpeed;

    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen) //~Note~ "Vector3.Distance" will calculate the distance between two points
        {
            anim.SetBool("doorOpen", true);
        } else
        {
            anim.SetBool("doorOpen", false);
        }

        if (playerExiting)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(!playerExiting)
            {
                thePlayer.canMove = false;

                StartCoroutine(useDoorCo());
            }
        }
    }

    IEnumerator useDoorCo()
    {

        playerExiting = true;

        thePlayer.anim.enabled = false;

        UIController.instance.StartFadeToBlack(); //calls the fade to function

        yield return new WaitForSeconds(1.5f);

        RespawnController.instance.SetSpawn(exitPoint.position);
        thePlayer.canMove = true;
        thePlayer.anim.enabled = true;

        UIController.instance.StartFadeFromBlack(); //calls the fade back function

        PlayerPrefs.SetString("ContinueLevel", levelToLoad); //Creates playpref to store data between game sessions for continuation

        //These values hold the points for the saved position of the player 
        PlayerPrefs.SetFloat("PosX", exitPoint.position.x);
        PlayerPrefs.SetFloat("PosY", exitPoint.position.y);
        PlayerPrefs.SetFloat("PosZ", exitPoint.position.z);

        SceneManager.LoadScene(levelToLoad); //loads new level
    }
}
