using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    //Movement Vars
    [SerializeField] bool rawMovement; // on for raw movement else off for lerp movement
    [SerializeField] float movementSpeed; // 8
    [SerializeField] float jumpSpeed; // 3
    [SerializeField] float grav; // 9
    [SerializeField] float gravMultiplier; // 1.2
    CharacterController cc;
    [SerializeField] float _dirY;
    [SerializeField] float hInput;
    [SerializeField] float vInput;

    //Player Stat Vars
    [SerializeField] int playerHP;
    [SerializeField] int playerMP;

    //playercam
    float mouseX, mouseY;
    [SerializeField] float rotationSpeedCam;
    [SerializeField] float zoomCam;
    [SerializeField] Camera playerCam;
    [SerializeField] Transform targetCam;
    [SerializeField] 


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        PlayerStats();
        //PlayerCameraHandling();
    }

    void Movement()
    {

        //debugs
        Debug.DrawRay(transform.position, cc.velocity, Color.green);

        float roundedMag = cc.velocity.magnitude;
        roundedMag = Mathf.RoundToInt(roundedMag);
        Debug.Log(roundedMag);
        //



        if (rawMovement)
        {
            hInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
        }

        Vector3 dir = new Vector3(hInput, 0, vInput);
        //dir.Normalize();


        if (cc.velocity.magnitude > movementSpeed + 0.1f)
        {
            Debug.Log("mag over movement speed");
        }

        //grav
        if (cc.isGrounded)
        {
            Debug.Log("grounded");
            _dirY = -0.1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _dirY = jumpSpeed;
            }
        }
        else
        {
            Debug.Log("not grounded");
            if (cc.velocity.y < 0)
            {
                _dirY -= grav * gravMultiplier * Time.deltaTime;
            }
            else
            {
                _dirY -= grav * Time.deltaTime;
            }
        }

        dir.y = _dirY;

        //cc.Move(dir * movementSpeed * Time.deltaTime);
        cc.Move(Vector3.ClampMagnitude(dir, 1) * movementSpeed * Time.deltaTime);
    }

    void PlayerStats()
    {

    }

    void PlayerCameraHandling()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * rotationSpeedCam;
        mouseY -= Input.GetAxisRaw("Mouse Y") * rotationSpeedCam;
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        /* no need child but have to -1 clamp
        Quaternion rot = Quaternion.Euler(mouseY, mouseX, 0);
        Vector3 zoomVec = new Vector3(0, 0, zoomCam);
        playerCam.transform.position = targetCam.position + rot * zoomVec;
        playerCam.transform.LookAt(targetCam.position);
        */

        targetCam.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        playerCam.transform.rotation = targetCam.transform.rotation;
        playerCam.transform.position = targetCam.transform.position + new Vector3(0,0,zoomCam);


        if (Input.mouseScrollDelta.y == 1)
        {
            zoomCam += 0.5f;
        }
        else if (Input.mouseScrollDelta.y == -1)
        {
            zoomCam -= 0.5f;
        }
        zoomCam = Mathf.Clamp(zoomCam, -10, 0);
    }


    //old movement
    /*
    if (Input.GetKey(KeyCode.W))
    {
        //cc.Move(transform.forward * movementSpeed * Time.deltaTime);
        hInput1 = movementSpeed;
    }
            else
    {
        hInput1 = 0;
    }
    if (Input.GetKey(KeyCode.A))
    {
        //cc.Move(-transform.right * movementSpeed * Time.deltaTime);
        vInput1 = movementSpeed;
    }
            else
    {
        vInput1 = 0;
    }
    if (Input.GetKey(KeyCode.S))
    {
        //cc.Move(-transform.forward * movementSpeed * Time.deltaTime);
        hInput2 = movementSpeed;
    }
            else
    {
        hInput2 = 0;
    }
    if (Input.GetKey(KeyCode.D))
    {
        //cc.Move(transform.right * movementSpeed * Time.deltaTime);
        vInput2 = movementSpeed;
    }
            else
    {
        vInput2 = 0;
    }
    */
}
