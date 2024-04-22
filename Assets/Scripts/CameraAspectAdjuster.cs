using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectAdjuster : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        AdjustCameraAspect();
        Debug.Log("Adjusted game camera screen resolution");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AdjustCameraAspect()
    {
        float targetAspect = 9f / 16f;
        float currentAspect = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            mainCamera.orthographicSize = mainCamera.orthographicSize * scaleHeight;
        }
    }
}
