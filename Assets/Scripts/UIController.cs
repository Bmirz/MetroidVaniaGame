using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;

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

    public Slider healthSlider;

    public Image fadeScreen;
    public float fadeSpeed = 2f;
    private bool fadingToBlack, fadingFromBlack;

    public string mainMenuScene;

    public GameObject pauseScreen;

    public GameObject fullScreenMap;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateHealth(PlayerHealthController.instance.maxHealth, PlayerHealthController.instance.maxHealth); - unneccesary
    }



    // Update is called once per frame
    void Update()
    {
        if(fadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 1f)
            {
                fadingToBlack = false;
            }
        } else if (fadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadingFromBlack = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    //handles updates to player health bar
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void StartFadeToBlack() //fade screen to black when entering doorways
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void StartFadeFromBlack() //fade screen from black when entering doorways
    {
        fadingFromBlack = true;
        fadingToBlack = false;
    }

    public void PauseUnpause()
    {
        if (!fullScreenMap.activeInHierarchy) //if map screen is inactive then you may pause
        {
            if (!pauseScreen.activeSelf)
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0f; //freezes time in game. Time is default set to 1

            }
            else
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    //loades main menu and handles destroying game objects unwanted on the main menu screen
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;

        Destroy(PlayerHealthController.instance.gameObject); //removes the health bar while in main menu
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject); //removes respawn while in main menu
        RespawnController.instance = null;

        Destroy(MapController.instance.gameObject); //removes mini map while in main menu
        MapController.instance = null;

        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }
}
