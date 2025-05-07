using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float xSpeed = 5;
    [SerializeField] private float ySpeed = 5;
    [SerializeField] private float dashStrength = 5f;
    [SerializeField, Range(0f, 0.3f)] private float moveDamp;

    [Header("Local Stats")]
    [SerializeField] public float health;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGrounded;

    private float ySpeedTrue;
    private float flightTime;
    private Vector3 Velocity = Vector3.zero;

    public Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private SpriteRenderer spriteRenderer;
    private PlayerStats stats;
    [SerializeField] private GameObject weapon;
    private WeaponController weaponController;

    public bool flipped;

    public float horizontalInput;
    private float verticalInput;

    private bool canParry = true;
    private float parryTime;

    private bool canDash = true;
    public int dashDirection = 1;
    private float dashTime;

    private bool canAttack = true;
    [SerializeField] private float attackTime;
    [SerializeField] public int attackCount = 0;
    [SerializeField] private bool attackBuffer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();

    }

    private void Start()
    {
        //xSpeed = xSpeed * stats.speed;
        //ySpeed = ySpeed * stats.speed;

        health = stats.health;

        weaponController = weapon.GetComponent<WeaponController>();
        inputHandler = PlayerInputHandler.Instance;
        dashTime = 1;
        ySpeedTrue = ySpeed;
        flightTime = 1;
    }

    private void Update()
    {
        horizontalInput = inputHandler.MoveInput.x;
        verticalInput = inputHandler.MoveInput.y;


    }

    private void FixedUpdate()
    {
        flip();

        isGrounded = false;
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.833389f - 0.1648924f, 0.25f - 2.1f), CapsuleDirection2D.Horizontal, 0, whatIsGround);

        Vector3 targetVelocity = new Vector2(horizontalInput * xSpeed, verticalInput * ySpeedTrue);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, moveDamp);
        Dash();
        Parry();
        Attack();


        if(verticalInput > 0)
        {

            flightTime -= Time.fixedDeltaTime;


        } else if(isGrounded)
        {
            flightTime = 1;
        }

        if(flightTime <= 0)
        {
            ySpeedTrue = 0f;
        } else
        {
            ySpeedTrue = ySpeed;
        }

        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        } else
        {
            canDash = true;
        }

        if (parryTime > 0)
        {
            parryTime -= Time.deltaTime;
        }
        else
        {
            canParry = true;
        }

    }

    private void Dash()
    {
        if (canDash && inputHandler.DashTriggered)
        {
            rb.AddForce(new Vector2(dashDirection * dashStrength, 0f), ForceMode2D.Impulse);
            dashTime = 0.5f;
            canDash = false;
        }
    }

    private void Parry()
    {
        if (canParry && inputHandler.ParryTriggered)
        {

            parryTime = 0.5f;
            canParry = false;
        }
    }

    void Attack()
    {
        attackTime -= Time.fixedDeltaTime;

        if (attackTime <= 0 && attackBuffer == false)
        {
            attackCount = 0;
        }

        if (inputHandler.AttackTriggered || attackBuffer)
        {
            if(attackCount < 4 && attackTime > 0f && attackTime < 0.3 && attackBuffer == false)
            {
                attackCount += 1;
                attackBuffer = true;
            }
            else if (attackCount >= 4)
            {
                attackCount = 0;
            }

            if(attackTime <= 0 && attackBuffer == false)
            {
                canAttack = true;
            } 
            else
            {
                canAttack = false;
            }

            if(canAttack || attackBuffer && attackTime <= 0)
            {
                weaponController.Attack(attackCount);
                attackTime = 0.5f;
                attackBuffer = false;
                
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>();
            health -= collision.gameObject.GetComponent<EnemyScript>().damage;
            rb.AddForce(Vector3.Normalize(new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y / 1.5f)) * collision.gameObject.GetComponent<EnemyScript>().usedKnockback);
        }
    }

    void flip()
    {
        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            Debug.Log("Change");
            dashDirection = -1;
            flipped = true;
        }
        else if (horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

            dashDirection = 1;
            Debug.Log("Change");
            flipped = false;
        }
    }

}
