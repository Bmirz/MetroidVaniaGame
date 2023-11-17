using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFullCamController : MonoBehaviour
{

    public MapCameraController mapCam; //reference to the camp controller script

    public float zoomSpeed;
    private float startSize;
    public float maxZoom, minZoom;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        startSize = cam.orthographicSize; //map staring zoom = preset value in unity


    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * cam.orthographicSize * Time.unscaledDeltaTime;

        if(Input.GetKey(KeyCode.E))
        {
            cam.orthographicSize -= zoomSpeed * Time.unscaledDeltaTime; //~Note~ unscaledDeltaTime ignores any augmentations to the ingame delta time and goes off real time instead
        }

        if (Input.GetKey(KeyCode.Q))
        {
            cam.orthographicSize += zoomSpeed * Time.unscaledDeltaTime;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom); //Clamps the camera size to be fixed between the minimum and maximum values of zoom
    }

    //~Note~ onEnable is a preset unity function that is called when a game object is activated
    private void OnEnable()
    {
        transform.position = mapCam.transform.position; //when initially opening full map, snap to same position as the minimap location (ON Player)
    }
}
