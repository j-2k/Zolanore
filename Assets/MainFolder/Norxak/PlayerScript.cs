using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement Vars
    [SerializeField] bool rawMovement; // on for raw movement else off for lerp movement
    [SerializeField] float movementSpeed; // 8
    [SerializeField] float jumpSpeed; // 3
    [SerializeField] float grav; // 9
    [SerializeField] float gravMultiplier; // 1.2
    CharacterController cc;
    [SerializeField] float _dirY;
    float hInput;
    float vInput;

    //cam refs
    [SerializeField] Transform cameraRig;

    //Player Stat Vars
    //[SerializeField] int playerHP;
    //[SerializeField] int playerMP;

    /*
    //playercam
    float mouseX, mouseY;
    [SerializeField] float rotationSpeedCam;
    [SerializeField] float zoomCam;
    [SerializeField] Camera playerCam;
    [SerializeField] Transform targetCam;
    [SerializeField] 
    */

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

        if (Input.GetKey(KeyCode.Q))
        {
            cc.transform.Rotate(0, -200 * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            cc.transform.Rotate(0, 200 * Time.deltaTime, 0);
        }


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

        Vector3 jumpDir = new Vector3(0, 0, 0);
        //dir.Normalize();
        
        Vector3 forwardMovement = cameraRig.transform.forward * vInput;
        Vector3 rightMovement = cameraRig.transform.right * hInput;


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

        jumpDir.y = _dirY;

        //cc.Move(dir * movementSpeed * Time.deltaTime);
        cc.Move(Vector3.ClampMagnitude(jumpDir + forwardMovement + rightMovement, 1) * movementSpeed * Time.deltaTime);
        /*
        if (cc.velocity.magnitude >= 0.05f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, cameraRig.eulerAngles.y, transform.eulerAngles.z);
        }
        */

        Vector3 vel = cc.velocity;
        vel.y = 0f;
        //cc.velocity = vel;
        if (cc.velocity.magnitude <= 0.1f)
        {
            
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(vel), 10 * Time.deltaTime);
        }

    }

    void PlayerStats()
    {

    }

    
    void PlayerCameraHandling()
    {
        //==
        /* no need child but have to -1 clamp
        Quaternion rot = Quaternion.Euler(mouseY, mouseX, 0);
        Vector3 zoomVec = new Vector3(0, 0, zoomCam);
        playerCam.transform.position = targetCam.position + rot * zoomVec;
        playerCam.transform.LookAt(targetCam.position);
        =====

        mouseX += Input.GetAxisRaw("Mouse X") * rotationSpeedCam;
        mouseY -= Input.GetAxisRaw("Mouse Y") * rotationSpeedCam;
        mouseY = Mathf.Clamp(mouseY, -90, 90);
        
        ==
        ~~
        ==

        /*
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
        */
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
