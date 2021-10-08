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


    //camera collisions
    float smooth = 10f;
    Vector3 camDir;
    public Vector3 camDirAdjusted;
    float distance;

    //camcol2
    [SerializeField] bool camDebugCollision;
    [SerializeField] float camColClipping = 0.3f;
    [SerializeField] LayerMask cameraLayer;
    float adjustedCamDistance;
    Ray camRay;
    RaycastHit camRayHit;

    private void Awake()
    {
        transform.SetParent(null);
    }

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

        //cam cols
        camDir = mainCam.transform.localPosition.normalized;
        distance = mainCam.transform.localPosition.magnitude;
    }

    void LateUpdate()
    {
        CameraHandler();
        //CameraCollisions();
        CameraCollisions2();
    }

    void CameraHandler()
    {
        //currentRotY = player.transform.eulerAngles.y;

        transform.position = player.transform.position + (Vector3.up * cameraHeight);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRotY, transform.eulerAngles.z);

        tiltX.eulerAngles = new Vector3(currentTiltX, tiltX.eulerAngles.y, tiltX.eulerAngles.z);
        //THSI LINE BELOW FOR CAM MOVEMENT/SCROLLING
        //mainCam.transform.position = transform.position + tiltX.forward * -currentCameraDistance;
        //new col line
        mainCam.transform.position = transform.position + tiltX.forward * -adjustedCamDistance;

        currentRotY += Input.GetAxisRaw("Mouse X") * cameraSensitivity ;
        currentTiltX -= Input.GetAxisRaw("Mouse Y") * cameraSensitivity ;

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

    void CameraCollisions()
    {
        //Vector3 mainCameraPosition = transform.parent.TransformPoint(camDir * cameraDistanceMax);
        Vector3 mainCameraPosition = mainCam.transform.parent.TransformPoint(camDir * cameraDistanceMax);
        RaycastHit hit;

        if (Physics.Linecast(mainCam.transform.parent.position, mainCameraPosition, out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.8f), 0, cameraDistanceMax);

        }
        else
        {
            distance = cameraDistanceMax;
        }
        mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camDir * distance, Time.deltaTime * smooth);

    }

    void CameraCollisions2()
    {
        float camDistance = currentCameraDistance + camColClipping;


        camRay.origin = transform.position;
        camRay.direction = -tiltX.forward;

        //raycast setup
        if(Physics.Raycast(camRay,out camRayHit,camDistance, cameraLayer))
        {//VVV COLLIDING
            adjustedCamDistance = Vector3.Distance(camRay.origin, camRayHit.point) - camColClipping;
        }
        else
        {//VVV NOT COLLIDING
            adjustedCamDistance = currentCameraDistance;
        }

        //debug
        //Debug.Log(adjustedCamDistance);

        if(camDebugCollision)
        {
            Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction * currentCameraDistance,Color.green);
        }
    }
}
