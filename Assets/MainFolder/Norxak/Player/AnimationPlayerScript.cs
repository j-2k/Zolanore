using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerScript : MonoBehaviour
{
    [SerializeField] bool simpleAnim;
    Animator playerAnimator;
    [SerializeField] float hInput;
    [SerializeField] float vInput;
    [SerializeField] bool isWalking;

    [SerializeField] float accell;
    [SerializeField] float decell;
    float velo;
    CharacterController cc;
    PlayerScript playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerSpeed = GetComponent<PlayerScript>();
        playerAnimator = GetComponentInChildren<Animator>();
        accell = 2.5f;
        decell = 3.5f;
        //simpleAnim = true;
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        //isWalking = playerAnimator.GetBool("isWalking");
        if (simpleAnim)
        {
            if (hInput != 0 || vInput != 0)//if (!isWalking && hInput != 0 || vInput != 0)
            {
                playerAnimator.SetBool("isWalking", true);
            }
            else
            {
                playerAnimator.SetBool("isWalking", false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                playerAnimator.SetTrigger("isAttacking");
            }
            else
            {
                playerAnimator.ResetTrigger("isAttacking");
            }
        }
        else
        {
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
    }
}
