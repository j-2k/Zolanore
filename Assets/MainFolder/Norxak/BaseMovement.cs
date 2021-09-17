using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] bool rawMovement; //
    [SerializeField] float movementSpeed; //
    [SerializeField] float jumpSpeed; //
    [SerializeField] float floorDistanceCheck; //1.1
    [SerializeField] float grav; //
    [SerializeField] float gravMultiplier; //
    CharacterController cc;
    float _dirY;
    float hInput;
    float vInput;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, cc.velocity, Color.green);

        float roundedMag = cc.velocity.magnitude;
        roundedMag = Mathf.RoundToInt(roundedMag);
        Debug.Log(roundedMag);

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
        dir.Normalize();

        //grav
        if (cc.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _dirY = jumpSpeed;
            }
            //grav = 0;
        }
        else
        {
            //cc.Move(-transform.up * grav * Time.deltaTime);
            //grav += gravMultiplier * Time.deltaTime;
            //grav += 10 * Time.deltaTime;
            if (cc.velocity.y <= 0)
            {
                _dirY -= grav * gravMultiplier * Time.deltaTime;
            }
            else
            {
                _dirY -= grav * Time.deltaTime;
            }
        }


        dir.y = _dirY;

        cc.Move(dir * movementSpeed * Time.deltaTime);

        if (cc.velocity.magnitude >= 5.1)
        {
            
            Debug.Log("yoooo over 5berah");
        }


        //movement
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
}
