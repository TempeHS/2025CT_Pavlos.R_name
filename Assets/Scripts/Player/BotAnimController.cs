using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimController : MonoBehaviour
{

    public Animator animator;

    public bool Jumping;
    public bool Falling;

    public float runningFrame;
    public float Running;

    [SerializeField] private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        Running = player.horizontalInput;

        if (player.flightTime <= 0 && player.isGrounded == false)
        {
            Falling = true;
        }
        else if (player.verticalInput <= 0 && player.isGrounded == false)
        {
            Falling = true;
        }
        else
        {
            Falling = false;
        }

        if (player.verticalInput > 0 && player.isGrounded == false && player.flightTime > 0)
        {
            Jumping = true;
        }
        else
        {
            Jumping = false;
        }

        if(Running != 0)
        {
            runningFrame = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }


        animator.SetBool("Jumping", Jumping);
        animator.SetBool("Falling", Falling);

        animator.SetFloat("Running", Mathf.Abs(Running));
    }
}
