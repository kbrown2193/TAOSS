using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraZoomer : MonoBehaviour
{
    public Camera camera;
    public float zoomStepAmount = 0.01875f; //0.01875, 0.0375, 0.075... 0.3, 0.6, 1.2, 2.4, 4.8, 9.6, 19.2, 38.4, 76.8, 153.6, 307.2, 614.4, 1228.8
    //public float zoomStepAmount = .15f; //0.01875, 0.0375, 0.075... 0.3, 0.6, 1.2, 2.4, 4.8, 9.6, 19.2, 38.4, 76.8, 153.6, 307.2, 614.4, 1228.8
    // .15 good for small incremental
    public float zoomResetSize = 9.6f;

    public float zoomSpeed; // todo, if want a smoother transition, have a zoom speed and how long it will take to reach the new zoom amount

    public float clamp_limit_lower = 0.01f;
    public float clamp_limit_upper = 1228.8f;

    // Update is called once per frame
    void Update()
    {
        // change later to new input system... testing...
        //if (Input.GetKeyDown(KeyCode.E))
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Zoom in Pressed");
            ZoomIn();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Zoom out Pressed");
            ZoomOut();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Zoom Rest Pressed");
            ZoomReset();
        }
    }

    public void ZoomIn()
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - zoomStepAmount, clamp_limit_lower, clamp_limit_upper);
    }

    public void ZoomOut()
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + zoomStepAmount, clamp_limit_lower, clamp_limit_upper);
    }

    public void ZoomReset()
    {
        camera.orthographicSize = zoomResetSize;
    }

    #region Getters / Setters
    public Camera GetCamera()
    {
        return camera;
    }
    public float GetZoomStepAmount()
    {
        return zoomStepAmount;
    }
    public float GetZoomResetSize()
    {
        return zoomResetSize;
    }
    public void SetCamera(Camera newCamera)
    {
        camera = newCamera;
    }
    public void SetZoomStepAmount(float amount)
    {
        zoomStepAmount = amount;
    }
    public void SetZoomResetSize(float amount)
    {
        zoomResetSize = amount;
    }
    #endregion
}
