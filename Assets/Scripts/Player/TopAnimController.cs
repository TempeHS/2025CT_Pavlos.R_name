using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class TopAnimController : MonoBehaviour
{

    public Animator animator;

    public bool IsSwinging;
    public bool Jumping;
    public bool Falling;
    public bool Parrying;

    public float runningFrame;
    public float Running;

    public int SwingCount;

    [SerializeField] private PlayerController player;
    [SerializeField] private WeaponController weapon;
    [SerializeField] private BotAnimController botAnim;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        if(weapon == null)
        {
            weapon = player.GetComponentInChildren<WeaponController>();
        }

    }

    // Update is called once per frame
    void Update()
    {

        Running = player.horizontalInput;
        SwingCount = player.attackCount;
        Parrying = player.Parrying;
        IsSwinging = player.isAttacking;

        runningFrame = botAnim.runningFrame;



        if (player.flightTime <= 0 && player.isGrounded == false)
        {
            Falling = true;
        }
        else if (player.verticalInput <= 0 && player.isGrounded == false) 
        {
            Falling = true;
        } else
        {
            Falling = false;
        }

        if(player.verticalInput > 0 && player.isGrounded == false && player.flightTime > 0)
        {
            Jumping = true;
        } else
        {
            Jumping = false;
        }




        animator.SetBool("IsSwinging", IsSwinging);
        animator.SetBool("Jumping", Jumping);
        animator.SetBool("Falling", Falling);
        animator.SetBool("Parrying", Parrying);

        animator.SetFloat("running frame", runningFrame);
        animator.SetFloat("Running", Mathf.Abs(Running));

        animator.SetInteger("Swing Count", SwingCount);
    }
}
