using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public static MapController instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public GameObject[] maps;
    public GameObject fullMapCam;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject map in maps)
        {
            if(PlayerPrefs.GetInt("Maps_" + map.name) == 1)
            {
                map.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Pressing the M key will show the full map screen and pause the game while viewing
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (!UIController.instance.pauseScreen.activeInHierarchy) //if pause screen is inactive then you may look at full screen map
            {
                if (!UIController.instance.fullScreenMap.activeInHierarchy)
                {
                    UIController.instance.fullScreenMap.SetActive(true);
                    Time.timeScale = 0f; //pauses time
                    fullMapCam.SetActive(true);
                }
                else
                {
                    UIController.instance.fullScreenMap.SetActive(false);
                    Time.timeScale = 1f; //continues time
                    fullMapCam.SetActive(false);
                }
            }
        }
    }

    public void ActivateMap(string mapToActivate)
    {
        foreach(GameObject map in maps)
        {
            if(map.name == mapToActivate)
            {
                map.SetActive(true);
                PlayerPrefs.SetInt("Map_" + mapToActivate, 1);
            }
        }
    }
}
