using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmLoader : MonoBehaviour
{
    public AudioManager theAM;

    private void Awake()
    {
        if(AudioManager.instance == null)
        {
            AudioManager newAM = Instantiate(theAM);
            AudioManager.instance = newAM;
            DontDestroyOnLoad(newAM.gameObject);
        }
    }
}
