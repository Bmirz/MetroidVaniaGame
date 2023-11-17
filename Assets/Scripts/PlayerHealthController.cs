using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()//~Note~ "Awake" is a prebuild function that runs before the start function.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//"oh look theres already an instance let me just destroy my self to prevent two players from spawning"
        }
    }

    [HideInInspector]
    public int currentHealth;
    public int maxHealth;

    public float invincibilityLength;
    private float invincCounter;

    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
            if (invincCounter > 0)
            {
                invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            if (invincCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        }
    }

    //deals with damaging the player
    public void damagePlayer(int damageAmount)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false); - obsolete since we now have a respawning system

                RespawnController.instance.Respawn(); // Respawns Player
                AudioManager.instance.PlaySFX(6);//playes death sound effect
            }
            else
            {
                invincCounter = invincibilityLength;
                AudioManager.instance.PlaySFXAdjusted(3);//playes damaged sound effect
            }

            UIController.instance.UpdateHealth(currentHealth, maxHealth);//updates player health bar when damaged
        }
    }

    //handles refilling players health during respawn
    public void FillHealth()
    {
        currentHealth = maxHealth; //fully heals player

        UIController.instance.UpdateHealth(currentHealth, maxHealth);//updates player health bar

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D other) //for dead zone, cancle invincibility
    {
        if (other.gameObject.tag == "DeadZone")
        {
            invincCounter = 0;
        }
    }
}