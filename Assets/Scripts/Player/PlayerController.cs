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
    private bool Parrying = false;

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

        if(health <= 0f) 
        {
            Destroy(gameObject);
        }


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
            flightTime = 2;
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
        if(parryTime > 0.66) 
        {
            parryTime -= Time.deltaTime;
        }
        else if (parryTime > 0)
        {
            Parrying = false;
            parryTime -= Time.deltaTime;
        }
        else
        {
            canParry = true;
        }

        if (canParry && inputHandler.ParryTriggered)
        {

            parryTime = 1f;
            canParry = false;
            Parrying = true;
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
            if (!Parrying)
            {
                collision.gameObject.GetComponent<EnemyStats>();
                health -= collision.gameObject.GetComponent<EnemyStats>().damage;
                rb.AddForce(Vector3.Normalize(new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y / 1.5f)) * collision.gameObject.GetComponent<EnemyStats>().knockback);
            }
            else if (Parrying)
            {
                collision.gameObject.GetComponent<EnemyScript>().rb.AddForce(Vector3.Normalize(new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y / 1.5f)) * collision.gameObject.GetComponent<EnemyStats>().knockback);
            }

        }
        
        if (TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag("Projectile"))
            {
                    health -= collision.gameObject.GetComponent<ProjStats>().damage;
            }


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
