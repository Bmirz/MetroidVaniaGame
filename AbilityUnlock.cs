using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * ~The purpose of this script is to handle player abilities Unlocks~ 
 * If a player collects the ability pickup object in game then they are able to unlock special abilities.
 * Abilities: Dash, ball form, double jump, and drop bomb
 * *Ability Pickup effect* + short ability unlocked text description
*/

public class AbilityUnlock : MonoBehaviour
{

    public bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockDropBomb;

    public GameObject pickUpEffect;

    public string unlockMessage;
    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            // Each if statment says if player unlocks ability then give player said ability
            if(unlockDoubleJump)
            {
                player.canDoubleJump = true;

                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1); //common in code to set int value to 1 (meaning true) or 0 (meaning false)

            }

            if(unlockDash)
            {
                player.canDash = true;

                PlayerPrefs.SetInt("DashUnlocked", 1);
            }

            if(unlockBecomeBall)
            {
                player.canBecomeBall = true;

                PlayerPrefs.SetInt("BallUnlocked", 1);
            }

            if(unlockDropBomb)
            {
                player.canDropBomb = true;

                PlayerPrefs.SetInt("BombUnlocked", 1);
            }

            Instantiate(pickUpEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5f);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(9);//playes pick up gem sound
        }
    }

}
