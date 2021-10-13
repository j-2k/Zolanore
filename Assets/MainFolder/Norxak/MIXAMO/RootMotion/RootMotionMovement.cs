using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionMovement : MonoBehaviour
{
    //Movement Vars
    CharacterController cc;
    [SerializeField] bool rawMovement; // on for raw movement else off for lerp movement
    [SerializeField] float movementSpeed; // 8 [SerializeField]
    [SerializeField] float movementAir; 
    [SerializeField] float jumpSpeed; // 3
    [SerializeField] float jumpCurve; // 3
    [SerializeField] float gravity; // 9
    float finalJumpCalc;
    [SerializeField] bool isJumping;
    Vector3 velocity;
    




    //anims
    Animator playerAnimator;
    Vector2 input;
    Vector3 rootMotion;

    //slopefix downforces
    float slopeForce; //0.1f best value
    [SerializeField] float slopeForceRayLength;
    [SerializeField] float slideDownSpeed;
    [SerializeField] float slideFriction; //0.3
    bool ccIsSlope;
    RaycastHit slopeHit;
    RaycastHit ccHit;

    //turning & cam refs
    float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.2f;
    [SerializeField] Transform cameraRig;


    // Start is called before the first frame update
    void Start()
    {
        slopeForce = 0.1f;// best value rn dont change
        cc = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        finalJumpCalc = Mathf.Sqrt(2 * gravity * jumpSpeed);
    }

    private void OnAnimatorMove()
    {
        rootMotion += playerAnimator.deltaPosition;
    }

    void Update()
    {
        if (rawMovement)    //!!!DISABLE SNAP IN INPUT PROJ SETTINGS FOR BETTER TURNING WHEN IT COMES TO RM OR ***USE SNAP & DONT USE RAW FOR BETTER RESULTS***
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }

        float movementDir = Mathf.Clamp01(input.magnitude);
        movementDir = Mathf.Clamp(movementDir, 0, 1);
        playerAnimator.SetFloat("rmVelocity", movementDir);
    }

    void LateUpdate()//fixed update results in jerkiness for some reason with RMs
    {
        RotationTransformCamera();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //playerAnimator.applyRootMotion = false;
            playerAnimator.SetTrigger("isAttacking");
            rootMotion = Vector3.zero;
        }


        if (OnSteepSlope())
        {
            cc.Move(SteepSlopeSlide() + Vector3.down * slopeForce);
            rootMotion = Vector3.zero;
            isJumping = true;
            //playerAnimator.SetBool("isJumping", true);
        }
        else if (isJumping) //or also in air
        {
            AirUpdate();
        }
        else //isgrounded
        {
            GroundedUpdate();
        }

    }

    private void GroundedUpdate()
    {

        Vector3 movementForward = rootMotion * movementSpeed;
        Vector3 downSlopeFix = Vector3.down * slopeForce;
        cc.Move(movementForward + downSlopeFix);
        rootMotion = Vector3.zero;

        if (!cc.isGrounded)
        {
            SetInAir(0);
        }

    }

    private void AirUpdate()
    {
        velocity.y -= gravity * Time.deltaTime;
        Vector3 displacement = velocity * Time.deltaTime;
        displacement += AirMovement();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        playerAnimator.SetBool("isJumping", isJumping);
    }

    #region old slope fix
    /* in update 
    if (!ccIsSlope)
        {
            velocity.x += (1f - ccHit.normal.y) * ccHit.normal.x* (1f - slideFriction);
            velocity.z += (1f - ccHit.normal.y) * ccHit.normal.z* (1f - slideFriction);
            AirUpdate2();
}
        else if(isJumping) //or also in air
        {
            AirUpdate();
        }
        else //isgrounded
        {
            GroundedUpdate();
        }

        ccIsSlope = (Vector3.Angle(Vector3.up, ccHit.normal) <= cc.slopeLimit);

    /*
    private void AirUpdate2()
    {play with grav * # <---
        velocity.y -= ( 3 * gravity) * 2 * (Time.deltaTime);
        Vector3 displacement = velocity * Time.deltaTime;
        displacement += AirMovement();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        playerAnimator.SetBool("isJumping", isJumping);
    }*/
    #endregion

    void PlayerJump()
    {
        if (!isJumping)
        {
            SetInAir(finalJumpCalc);
        }
    }

    void SetInAir(float jumpVelo)
    {
        isJumping = true;
        velocity = playerAnimator.velocity * jumpCurve * movementSpeed;
        velocity.y = jumpVelo;
        playerAnimator.SetBool("isJumping", true);
    }

    Vector3 AirMovement()
    {
        return ((cameraRig.transform.forward * input.y) + (cameraRig.transform.right * input.x)) * movementAir * Time.deltaTime;
    }

    bool OnSteepSlope()
    {
        if (!cc.isGrounded)
        {
            return false;
        }

        if (Physics.Raycast(transform.position + (transform.forward * 0.1f), Vector3.down, out slopeHit, (cc.height/2) + slopeForceRayLength))
        {
            float slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if(slopeAngle > cc.slopeLimit)
            {
                return true;
            }
        }
        return false;
    }

    Vector3 SteepSlopeSlide()
    {
        Vector3 slopeDir = Vector3.up - slopeHit.normal * Vector3.Dot(Vector3.up, slopeHit.normal);
        float slideSpeed = slideDownSpeed + Time.deltaTime;

        Vector3 moveDir = slopeDir * -slideSpeed;
        moveDir.y = moveDir.y - slopeHit.point.y;
        return moveDir * Time.deltaTime;
    }

    void RotationTransformCamera()
    {
        //rotation transform
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRot = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraRig.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, turnSmoothTime);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ccHit.normal = hit.normal;
    }
}
