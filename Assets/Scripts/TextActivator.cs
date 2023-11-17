using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActivator : MonoBehaviour
{
    public GameObject textToActivate; 
    public float fadeCounter;
    private bool startCounter = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            textToActivate.SetActive(true);
            startCounter = true;
        }
    }

        // Update is called once per frame
    void Update()
    {
        if (startCounter == true && fadeCounter > 0)
        {
            fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        textToActivate.SetActive(false);
                        gameObject.SetActive(false);
                    }
        }
    }
}
