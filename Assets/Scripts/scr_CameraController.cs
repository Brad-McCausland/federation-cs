using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_CameraController : MonoBehaviour
{
    public int MIN_ZOOM_FACTOR; //3
    public int MAX_ZOOM_FACTOR; //9

    public float ZOOM_SPEED; //1
    public float PAN_SPEED; //0.01
    public float ZOOM_PAN_SPEED; //0.005

    private Vector3 previousMousePosition;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Zooming
        var targetDelta = Camera.main.transform.position - (Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0));
        targetDelta = targetDelta * ZOOM_PAN_SPEED;
        //Debug.Log(Camera.main.transform.position + " - " + Input.mousePosition + " = " + targetDelta);

        if (Input.GetAxis("Mouse ScrollWheel") > 0) // zoom out
        {
            if (Camera.main.orthographicSize > MIN_ZOOM_FACTOR)
            {
                Camera.main.orthographicSize -= ZOOM_SPEED;
                Camera.main.transform.position -= targetDelta;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // zoom in
        {
            if (Camera.main.orthographicSize < MAX_ZOOM_FACTOR)
            {
                Camera.main.orthographicSize += ZOOM_SPEED;
                Camera.main.transform.position += targetDelta;
            }
        }

        // Dragging
        if (Input.GetMouseButtonDown(2))
        {
            previousMousePosition = Input.mousePosition;
            isDragging = true;
        }
        if (isDragging)
        {
            var mouseDelta = previousMousePosition - Input.mousePosition;
            previousMousePosition = Input.mousePosition;
            Camera.main.transform.position = Camera.main.transform.position + (mouseDelta * 0.01F);
        }
        if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
            previousMousePosition = Vector3.zero;
        }

        // Panning
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.position += new Vector3(0, PAN_SPEED, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.position += new Vector3(0, -PAN_SPEED, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.position += new Vector3(-PAN_SPEED, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.position += new Vector3(PAN_SPEED, 0, 0);
        }
    }
}
