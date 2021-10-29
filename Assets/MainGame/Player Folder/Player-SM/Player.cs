using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement Vars
    CharacterController cc;
    [SerializeField] bool rawMovement; // Keep off with RM
    [SerializeField] float movementSpeed; // 1
    [SerializeField] float movementAir; // 5
    [SerializeField] float jumpSpeed; // 2
    [SerializeField] float jumpCurve; // 0.3
    [SerializeField] float gravity; // 20
    public int outgoingDamage; // 20
    float finalJumpCalc;
    [SerializeField] bool isJumping;
    Vector3 velocity;

    [SerializeField] bool isAttackStart;
    [SerializeField] float attackColliderRadius;

    //anims
    [SerializeField] Animator playerAnimator;
    Vector2 input;

    //slopefix downforces
    float slopeForce = 20; //0.1f best value
    [SerializeField] float slopeForceRayLength; //3
    [SerializeField] float slideDownSpeed;//8
    RaycastHit slopeHit;
    RaycastHit ccHit;

    //turning & cam refs
    float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f; //0.1f
    public Transform cameraRig;

    [SerializeField] GameObject sphereColl; //collision location

    CharacterManager characterManager;
    LevelSystem levelSystem;

    public bool isUsingAbility;
    // Start is called before the first frame update
    void Start()
    {
        isUsingAbility = false;
        levelSystem = LevelSystem.instance;
        cameraRig = GameObject.FindGameObjectWithTag("CameraManager").transform;
        characterManager = GetComponent<CharacterManager>();
        slopeForce = 0.1f;// best value rn dont change
        cc = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        finalJumpCalc = Mathf.Sqrt(2 * gravity * jumpSpeed);
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

        if (!isUsingAbility)
        {

            if (Input.GetKeyDown(KeyCode.Space) && !isAttackStart)
            {
                PlayerJump();
            }

            if (Input.GetKey(KeyCode.Mouse0) && !isJumping && !isAttackStart)
            {
                isAttackStart = true;
                playerAnimator.SetTrigger("isAttacking");
            }

            float movementDir = Mathf.Clamp01(input.magnitude);
            movementDir = Mathf.Clamp(movementDir, 0, 1);
            playerAnimator.SetFloat("rmVelocity", movementDir);
        }
        else
        {
            playerAnimator.SetFloat("rmVelocity", 0);
        }
        
    }
    /*
    void FixedUpdate()//fixed update results in jerkiness for some reason with RMs
    {
        if (!isUsingAbility)
        {
            if (!isAttackStart)
            {
                MainMovement();
            }
            RotationTransformCamera();
        }
    }

    private void LateUpdate()
    {
        if (!isUsingAbility)
        {
            //RotationTransformCamera();
        }
    }
    */
    
    void LateUpdate()//fixed update results in jerkiness for some reason with RMs
    {
        if (!isUsingAbility)
        {
            if (!isAttackStart)
            {
                MainMovement();
            }
            RotationTransformCamera();
        }
    }
    

    public void MainMovement()
    {
        /*
        if (cc.velocity.magnitude >= movementSpeed * 1.2f)
        {
            Debug.LogWarning("Very fast");
            GroundedUpdateNormalized();
        }
        else 
        
        */if (OnSteepSlope())
        {
            cc.Move(SteepSlopeSlide() + Vector3.down * slopeForce);
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

        //out going dmg calc maybe change in the future for better results to scale to higher lvls
        int levelBasedDmg = (int)((levelSystem.currentLevel * 2) * Random.Range(0.7f,1.1f));
        outgoingDamage = (int)(characterManager.Strength.Value * Random.Range(1.5f, 2.5f) + levelBasedDmg);


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

    uint attackID;

    public void DashID()
    {
        //When sword is swung
        attackID = (uint) Random.Range(0, uint.MaxValue);
    }

    public void DashAttack()
    {
        hitColliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, attackColliderRadius);

        int levelBasedDmg = (int)((levelSystem.currentLevel * 2) * Random.Range(1f, 1.5f));

        outgoingDamage = (int)(characterManager.Strength.Value * Random.Range(0.5f, 1f) + levelBasedDmg);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                Debug.Log("I just hit an enemey");
                if (hitCollider.GetComponent<EnemyProtoVersion>().hitID != attackID)
                {
                    // Hit enemy
                    hitCollider.GetComponent<EnemyProtoVersion>().hitID = attackID;
                    hitCollider.GetComponent<EnemyProtoVersion>().TakeDamageFromPlayer(outgoingDamage);
                }
            }
        }
    }

    public void AOEAttack()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 5);

        int levelBasedDmg = (int)((levelSystem.currentLevel * 2) * Random.Range(1f, 1.5f));

        outgoingDamage = (int)(characterManager.Strength.Value * Random.Range(0.6f, 0.8f) + levelBasedDmg);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                Debug.Log("I just hit an enemey");
                hitCollider.GetComponent<EnemyProtoVersion>().TakeDamageFromPlayer(outgoingDamage);
            }
        }
    }

    void EndOfAttack()
    {
        isAttackStart = false;
    }

    public void GroundedUpdate()
    {
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

    public void GroundedUpdateNormalized()
    {
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

    private void AirUpdate()
    {
        velocity.y -= gravity * Time.deltaTime;
        Vector3 displacement = velocity * Time.deltaTime;
        displacement += AirMovement();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
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
        velocity = playerAnimator.velocity * jumpCurve * (movementSpeed / 6);
        playerAnimator.SetBool("isJumping", true);
        velocity.y = jumpVelo;
    }

    Vector3 AirMovement()
    {

        return ((cameraRig.transform.forward * input.y) + (cameraRig.transform.right * input.x)) * (movementAir - 2) * Time.deltaTime;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(sphereColl.transform.position, attackColliderRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + transform.forward + transform.up, attackColliderRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ccHit.normal = hit.normal;
    }


}
