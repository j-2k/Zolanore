using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRMScript : MonoBehaviour
{
    //Movement Vars
    CharacterController cc;
    [SerializeField] bool rawMovement; // Keep off with RM
    [SerializeField] float movementSpeed; // 1
    [SerializeField] float movementAir; // 5
    [SerializeField] float jumpSpeed; // 2
    [SerializeField] float jumpCurve; // 0.3
    [SerializeField] float gravity; // 20
    [SerializeField] int outgoingDamage; // 20
    float finalJumpCalc;
    [SerializeField] bool isJumping;
    Vector3 velocity;

    [SerializeField] bool isAttackStart;
    [SerializeField] float attackColliderRadius;

    //anims
    Animator playerAnimator;
    AnimatorStateInfo animState;
    Vector2 input;
    Vector3 rootMotion;

    //slopefix downforces
    float slopeForce; //0.1f best value
    [SerializeField] float slopeForceRayLength; //3
    [SerializeField] float slideDownSpeed;//8
    RaycastHit slopeHit;
    RaycastHit ccHit;

    //turning & cam refs
    float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f; //0.1f
    Transform cameraRig;

    [SerializeField] GameObject sphereColl; //collision location

    [SerializeField] bool god;
    [SerializeField] int health;

    [SerializeField] HPBar hpBar;

    [SerializeField] bool isRootMotion;
    // Start is called before the first frame update
    void Start()
    {
        if (hpBar != null)
        {
            health = 200;
            hpBar.SetMaxHealth(health);
        }
        cameraRig = GameObject.FindGameObjectWithTag("CameraManager").transform;
        slopeForce = 0.1f;// best value rn dont change
        cc = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        finalJumpCalc = Mathf.Sqrt(2 * gravity * jumpSpeed);
    }

    //handled by script in inspector is because of this
    private void OnAnimatorMove()
    {
        animState = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (isRootMotion)
        {
            rootMotion += playerAnimator.deltaPosition;
        }
    }
    
    void Update()
    {
        if (cc.isGrounded)
        {
            Debug.Log("grounded");
        }
        else
        {
            Debug.Log("Not Grounded");
        }


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

        if (Input.GetKeyDown(KeyCode.Space) && !isAttackStart)
        {
            PlayerJump();
        }

        if (Input.GetKey(KeyCode.Mouse0) && !isJumping && !isAttackStart)
        {
            isAttackStart = true;
            playerAnimator.SetTrigger("isAttacking");
            rootMotion = Vector3.zero;
        }

        float movementDir = Mathf.Clamp01(input.magnitude);
        movementDir = Mathf.Clamp(movementDir, 0, 1);
        playerAnimator.SetFloat("rmVelocity", movementDir);
    }

    void LateUpdate()//fixed update results in jerkiness for some reason with RMs
    {
        if (!isAttackStart)
        {
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

        RotationTransformCamera();
    }


    /// <summary>
    /// REVIST THIS DETECTION FOR ENEMIES HIT MAYBE USE A DIFF IN THE FUTURE THIS WAS ORIGINALLY PALCEHODLER
    /// </summary>
    /// 
    Collider[] hitColliders;
    void PeakofAttack()
    {
        Debug.Log("Peak of Attack");
        //MIGHT USE ANOTHER TYPE OF COLLISION LOGIC HERE THIS IS PLACE HOLDER

        hitColliders = Physics.OverlapSphere(sphereColl.transform.position, attackColliderRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                Debug.Log("I just hit an enemey");
                //hitCollider.GetComponent<SimpleEnemy>().TakeDamageFromPlayer(outgoingDamage);
                hitCollider.GetComponent<EnemyProtoVersion>().TakeDamageFromPlayer(outgoingDamage);
            }
        }
    }

    void EndOfAttack()
    {
        isAttackStart = false;
    }

    private void GroundedUpdate()
    {
        if (isRootMotion)
        {
            movementSpeed = 1;
            slopeForce = 0.1f;
            Vector3 movementForward = rootMotion * movementSpeed;
            Vector3 downSlopeFix = Vector3.down * slopeForce;
            cc.Move(movementForward + downSlopeFix);
            rootMotion = Vector3.zero;

            if (!cc.isGrounded)
            {
                SetInAir(0);
            }
        }
        else
        {
            slopeForce = 20;
            movementSpeed = 10;
            Vector3 forwardMovement = (cameraRig.transform.forward * input.y) * movementSpeed;
            Vector3 rightMovement = (cameraRig.transform.right * input.x) * movementSpeed;
            Vector3 downSlopeFix = (Vector3.down * cc.height / 2 * slopeForce);
            Vector3 finalVelo = rightMovement + forwardMovement + downSlopeFix;

            cc.Move(Vector3.ClampMagnitude(finalVelo, 1) * movementSpeed * Time.deltaTime);
            if (!cc.isGrounded)
            {
                SetInAir(0);
            }
            
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
        if (isRootMotion)
        {
            velocity = playerAnimator.velocity * jumpCurve * movementSpeed;
        }
        else
        {
            velocity = playerAnimator.velocity * jumpCurve * (movementSpeed / 6);
        }
            playerAnimator.SetBool("isJumping", true);
            velocity.y = jumpVelo;
    }

    Vector3 AirMovement()
    {
        if (isRootMotion)
        {
            return ((cameraRig.transform.forward * input.y) + (cameraRig.transform.right * input.x)) * movementAir * Time.deltaTime;
        }
        else
        {
            return ((cameraRig.transform.forward * input.y) + (cameraRig.transform.right * input.x)) * (movementAir-2) * Time.deltaTime;
        }
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

    bool oneRun;

    void RotationTransformCamera()
    {
        if (!isAttackStart)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            //rotation transform
            oneRun = false;
            if (inputDir != Vector2.zero)
            {
                float targetRot = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraRig.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, turnSmoothTime);
            }
             
        }
        else
        {
            if (!oneRun)
            {
                float targetRot = cameraRig.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, 0);
                oneRun = true;
            }
        }
    }

    public void TakeDamageFromEnemy(int incDmg)
    {
        if (god)
        {

        }
        else
        {
            health -= incDmg;
            if (health <= 0)
            {
                this.enabled = false;
            }
        }
        if (hpBar != null)
        {
            hpBar.SetHealth(health);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(sphereColl.transform.position, attackColliderRadius);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ccHit.normal = hit.normal;
    }


}