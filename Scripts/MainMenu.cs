using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public string newGameScene;

    public GameObject continueButton;

    public PlayerAbilityTracker player;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //if there are playerPrefs stored, then show the continue button in main menu
        if(PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueButton.SetActive(true);
        }

        AudioManager.instance.PlayMainMenuMusic();
    }

    //handles loading up a new game from main menu
    public void NewGame()
    {
        PlayerPrefs.DeleteAll(); //Deletes all saved game data

        StartCoroutine(startGameCo());
    }

    //handles loading up a saved game from main menu
    public void Continue()
    {
        //utilizes player prefs to gather the level we are at firstly, and where to load the player secondly
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));

        //saves player abilities that have been unlocked
        if(PlayerPrefs.HasKey("DoubleJumpUnlocked"))
        {
            if (PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1)
            {
                player.canDoubleJump = true;
            }
        }
        if (PlayerPrefs.HasKey("DashUnlocked"))
        {
            if (PlayerPrefs.GetInt("DashUnlocked") == 1)
            {
                player.canDash = true;
            }
        }
        if (PlayerPrefs.HasKey("BallUnlocked"))
        {
            if (PlayerPrefs.GetInt("BallUnlocked") == 1)
            {
                player.canBecomeBall = true;
            }
        }
        if (PlayerPrefs.HasKey("BombUnlocked"))
        {
            if (PlayerPrefs.GetInt("BombUnlocked") == 1)
            {
                player.canDropBomb = true;
            }
        }

        StartCoroutine(startGameCo());
    }

    //handles quiting out of the application from main menu
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); //for dev purposes
    }

    IEnumerator startGameCo()
    {
        anim.SetBool("startGame", true);
        yield return new WaitForSeconds(1.5f);

        if(PlayerPrefs.HasKey("ContinueLevel"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
        } else
        {
            SceneManager.LoadScene(newGameScene);
        }
    }


}
