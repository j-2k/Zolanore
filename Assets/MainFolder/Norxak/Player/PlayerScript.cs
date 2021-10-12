using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //debug
    [SerializeField] bool isDebugging;

    //Movement Vars
    [SerializeField] bool rawMovement; // on for raw movement else off for lerp movement
    public float movementSpeed; // 8 [SerializeField]
    [SerializeField] float jumpSpeed; // 3
    [SerializeField] float grav; // 9
    [SerializeField] float gravMultiplier; // 1.2
    float _dirY;
    float hInput;
    float vInput;


    //cc stuff
    CharacterController cc;
    //NEW WAY TO CHECK FOR SLOPE ANGLE BELOW
    Vector3 ccHitNormal;

    bool isJumping;
    bool ccIsGrounded;
    [SerializeField] float slideFriction;

    //slopefix downforces
    [SerializeField] float slopeForce;
    [SerializeField] float slopeForceRayLength;

    [SerializeField] bool badSlopeFix;

    //turning
    float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.2f;

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
        PlayerStats();
    }

    void Movement()
    {
        //debugs
        if (isDebugging)
        {
            //MOVEMENT VEC
            Debug.DrawRay(transform.position, cc.velocity, Color.cyan);
            //SLOPE CC NORM HIT VEC
            Debug.DrawRay(transform.position, ccHitNormal * 10, Color.red);

            Debug.Log(Vector3.Angle(Vector3.up, ccHitNormal) + "normal thing red debug");
            //slope force
            Debug.DrawRay(transform.position, Vector3.down * (cc.height / 2 * slopeForceRayLength), Color.white);
            float roundedMag = cc.velocity.magnitude;
            roundedMag = Mathf.RoundToInt(roundedMag);
            Debug.Log(roundedMag);
            //
            if (cc.isGrounded)
            {
                Debug.Log("grounded");
            }
            else
            {
                Debug.Log("not grounded");
            }
        }

        //raw movements
        if (rawMovement)
        {
            hInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            //bad fix for sliding down
            if (badSlopeFix)
            {
                if (ccIsGrounded)
                {
                    hInput = Input.GetAxis("Horizontal");
                    vInput = Input.GetAxis("Vertical");
                }
                else
                {
                    hInput = 0;
                    vInput = 0;
                }
            }
            else
            {
                hInput = Input.GetAxis("Horizontal");
                vInput = Input.GetAxis("Vertical");
            }
        }

        //get movement + camera into account
        Vector3 forwardMovement = cameraRig.transform.forward * vInput;
        Vector3 rightMovement = cameraRig.transform.right * hInput;

        //debug
        if (cc.velocity.magnitude > movementSpeed + 0.1f)
        {
            Debug.Log("mag over movement speed");
        }

        //gravity & jump
        Vector3 jumpDir = new Vector3(0, 0, 0);
        //dir.Normalize();

        if (cc.isGrounded)
        {
            //Debug.Log("grounded");

            isJumping = false;
            _dirY = -0.1f;

            cc.slopeLimit = 50;

            if (Input.GetKeyDown(KeyCode.Space) && ccIsGrounded)
            {
                isJumping = true;
                _dirY = jumpSpeed;
            }
        }
        else
        {
            //Debug.Log("not grounded");

            cc.slopeLimit = 0;

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


        Vector3 velo = jumpDir + forwardMovement + rightMovement;

        if (!ccIsGrounded)
        {
            velo.x += (((1f - ccHitNormal.y) * ccHitNormal.x)) * (1f - slideFriction); //*3;
            velo.z += (((1f - ccHitNormal.y) * ccHitNormal.z)) * (1f - slideFriction); //*3;
                                                                                       //NEEDS A FIX SOON // DELETED ALL HACKY SOLUTIONS
                                                                                       //PROBLEM = GOING AGAINST A SLOPE WILL KEEP THE PLAYER STATIONARY * FIX THIS
        }

        //final movement
        cc.Move(Vector3.ClampMagnitude(velo, 1) * movementSpeed * Time.deltaTime);

        ccIsGrounded = (Vector3.Angle(Vector3.up, ccHitNormal) <= cc.slopeLimit);

        //downslope force
        if (OnSlope()) //hInput != 0 ||vInput != 0 && 
        {
            cc.Move((Vector3.down * cc.height / 2 * slopeForce) * Time.deltaTime);
        }


        //rotation transform
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRot = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraRig.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, turnSmoothTime);
        }
    }

    bool OnSlope()
    {
        if (isJumping)
        {
            return false;
        }

        RaycastHit slopeHit;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, cc.height / 2 * slopeForceRayLength))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    void PlayerStats()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ccHitNormal = hit.normal;
    }
}
