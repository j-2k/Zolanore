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

    //new jump
    //[SerializeField] AnimationCurve jumpFallOff;
    //[SerializeField] float jumpMultiplier;
    //[SerializeField] KeyCode jumpKey;

    bool isJumping;

    //slopefix
    [SerializeField] float slopeForce;
    [SerializeField] float slopeForceRayLength;

    float turnSmoothVelocity;
    [SerializeField]float turnSmoothTime = 0.2f;

    //cam refs
    [SerializeField] Transform cameraRig;

    //Player Stat Vars
    //[SerializeField] int playerHP;
    //[SerializeField] int playerMP;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //Jump();
        PlayerStats();
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

        
        Vector3 forwardMovement = cameraRig.transform.forward * vInput;
        Vector3 rightMovement = cameraRig.transform.right * hInput;

        //debug
        if (cc.velocity.magnitude > movementSpeed + 0.1f)
        {
            Debug.Log("mag over movement speed");
        }

        Vector3 jumpDir = new Vector3(0, 0, 0);
        //dir.Normalize();
        
        //grav
        if (cc.isGrounded)
        {
            Debug.Log("grounded");
            isJumping = false;
            _dirY = -0.1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
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
        
        cc.Move(Vector3.ClampMagnitude(jumpDir + forwardMovement + rightMovement, 1) * movementSpeed * Time.deltaTime);

        if ((vInput != 0 || hInput != 0) && OnSlope())
        {
            cc.Move(Vector3.down * cc.height / 2 * slopeForce * Time.deltaTime);
        }

        //rotation trans
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRot = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraRig.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, turnSmoothTime);
        }
    }

    /*
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;
        
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            cc.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!cc.isGrounded && cc.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }
    */

    bool OnSlope()
    {
        if (isJumping)
        {
            return false;
        }

        RaycastHit slopeHit;
        Debug.DrawRay(transform.position, Vector3.down * (cc.height / 2 * slopeForceRayLength), Color.black);
        if(Physics.Raycast(transform.position,Vector3.down,out slopeHit,cc.height/2 * slopeForceRayLength))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false;
    }

    void PlayerStats()
    {

    }
}
