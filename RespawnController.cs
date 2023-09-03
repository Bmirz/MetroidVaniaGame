using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; //allows manipulation of unity scenes

public class RespawnController : MonoBehaviour
{

    public static RespawnController instance;

    private void Awake()//allows us to care over information between reloaded scenes
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 respawnPoint;
    public float waitToRespawn;

    private GameObject thePlayer;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;

        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
    
    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        /*if (deathEffect != null)
        {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }*/
    }

    IEnumerator RespawnCo() //A CoRoutine? used to delay the call of a function in real time similar to a timer?
    {
        thePlayer.SetActive(false);//delete player
        if (deathEffect != null)
        {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }

        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reload Scene

        thePlayer.transform.position = respawnPoint;//place player at point of death
        thePlayer.SetActive(true);//bring back player

        PlayerHealthController.instance.FillHealth();
    }
}
