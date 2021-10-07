using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerMain : MonoBehaviour
{
    //keys
    KeyCode LMB = KeyCode.Mouse0, RMB = KeyCode.Mouse1, MMB = KeyCode.Mouse2;

    //vars
    [SerializeField] float cameraHeight = 1, cameraDistanceMax = 10;
    float cameraMaxTilt = 90;
    [Range(0,4)]
    [SerializeField] float cameraSpeed = 2;
    float currentRotY, currentTiltX = 20, currentCameraDistance = 5;
    [Range(0, 10)]
    [SerializeField] float cameraSensitivity = 2;

    //ref
    PlayerScript player;
    [SerializeField] Transform tiltX;
    Camera mainCam;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        mainCam = Camera.main;

        transform.position = player.transform.position + (Vector3.up * cameraHeight);
        transform.rotation = player.transform.rotation;

        tiltX.eulerAngles = new Vector3(currentTiltX, transform.eulerAngles.y, transform.eulerAngles.z);
        mainCam.transform.position += tiltX.forward * -currentCameraDistance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        CameraHandler();
    }

    void CameraHandler()
    {
        //currentRotY = player.transform.eulerAngles.y;

        transform.position = player.transform.position + (Vector3.up * cameraHeight);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRotY, transform.eulerAngles.z);

        tiltX.eulerAngles = new Vector3(currentTiltX, tiltX.eulerAngles.y, tiltX.eulerAngles.z);
        mainCam.transform.position = transform.position + tiltX.forward * -currentCameraDistance;

        currentRotY += Input.GetAxisRaw("Mouse X") * cameraSensitivity;
        currentTiltX -= Input.GetAxisRaw("Mouse Y") * cameraSensitivity;

        currentTiltX = Mathf.Clamp(currentTiltX, -90, 90);

        if (Input.mouseScrollDelta.y == 1)
        {
            currentCameraDistance -= 0.5f;
        }
        else if (Input.mouseScrollDelta.y == -1)
        {
            currentCameraDistance += 0.5f;
        }

        currentCameraDistance = Mathf.Clamp(currentCameraDistance, 0, cameraDistanceMax);
    }    
}
