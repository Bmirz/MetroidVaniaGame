using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{

    public static BossHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;

    public int currentHealth = 30;

    public BossBattle1 theBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;

            theBoss.EndBattle();

            AudioManager.instance.PlaySFX(14);//boss death audio
        } else
        {
            AudioManager.instance.PlaySFX(13);//boss impact audio
        }

        bossHealthSlider.value = currentHealth;
    }
}
