using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerScript : MonoBehaviour
{
    [SerializeField] bool simpleAnim;
    Animator playerAnimator;
    [SerializeField] float hInput;
    [SerializeField] float vInput;
    [SerializeField] float accell;
    [SerializeField] float decell;
    [SerializeField] Collider sphereColl;
    float velo;
    CharacterController cc;
    PlayerScript playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponentInParent<CharacterController>();
        playerSpeed = GetComponent<PlayerScript>();
        playerAnimator = GetComponent<Animator>();
        accell = 2.5f;
        decell = 3.5f;
        //simpleAnim = true;
    }
    public bool isAttackNow;
    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (cc.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!isAttackNow)
                {
                    isAttackNow = true;
                    playerAnimator.SetTrigger("isAttackSM");
                }
            }
            playerAnimator.SetBool("isJumping", false);
            //float speedPercent = cc.velocity.magnitude / 1;
            if (hInput != 0 || vInput != 0)// && velo <= 1f)//if (!isWalking && hInput != 0 || vInput != 0)
            {
                velo += Time.deltaTime * accell;
            }
            else
            {
                velo -= Time.deltaTime * decell;
            }
            velo = Mathf.Clamp(velo, 0, 1);
            playerAnimator.SetFloat("SpeedPercent", velo);//, accell, Time.deltaTime);
        }
        else
        {
            Debug.Log("jump");
            playerAnimator.SetBool("isJumping", true);
        }

    }

    Collider[] hitColliders;
    public void PeakOfAttack()
    {
        Debug.Log("height attack");

        hitColliders = Physics.OverlapSphere(sphereColl.transform.position, sphereColl.transform.localScale.x / 3);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                Debug.Log("I just hit an enemey");
                hitCollider.GetComponent<SimpleEnemy>().TakeDamageFromPlayer(20);
            }
            if (hitCollider.tag == "Orb")
            {
                Debug.Log("Boss hit");
                hitCollider.GetComponent<RotateFast>().damageComingFromPlayer = 20 / 2;
                hitCollider.GetComponent<RotateFast>().isHit = true;
            }
        }
    }

    public void LastFrameAttack()
    {
        Debug.Log("end attack");
        isAttackNow = false;
    }
}
