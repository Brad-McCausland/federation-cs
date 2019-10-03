using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BackgroundParralaxManager : MonoBehaviour
{
    public Camera mainCamera;
    private float parallaxValue = 0.1F;

    // Update is called once per frame
    void Update()
    {
        //transform.position = mainCamera.transform.position + (mainCamera.transform.position * parallaxValue);
        //Debug.Log(mainCamera.transform.position + " compare " + mainCamera.transform.position * parallaxValue);
    }
}
