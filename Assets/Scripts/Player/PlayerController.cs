using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] public bool isGrounded;

    private float ySpeedTrue;
    public float flightTime;
    private Vector3 Velocity = Vector3.zero;

    public Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private SpriteRenderer spriteRenderer;
    private GameObject PlayerStats;
    private PlayerStats stats;
    private PlayerSpawnManager spawner;
    private HitStop hitStop;
    [SerializeField] private GameObject weapon;
    private WeaponController weaponController;

    private GameObject ParryPartObject;
    public ParticleSystem ParryPart;

    public bool flipped;

    public float horizontalInput;
    public float verticalInput;

    private bool canParry = true;
    private float parryTime;
    public bool Parrying = false;

    private bool canDash = true;
    public int dashDirection = 1;
    private float dashTime;

    private bool canAttack = true;
    [SerializeField] private float attackTime;
    [SerializeField] public int attackCount = 0;
    [SerializeField] public bool isAttacking;
    [SerializeField] private bool attackBuffer;

    public TextMeshProUGUI TextHealth;

    private void Awake()
    {
        PlayerStats = GameObject.Find("PlayerStats");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = PlayerStats.GetComponent<PlayerStats>();
        spawner = PlayerStats.GetComponent<PlayerSpawnManager>();
        hitStop = PlayerStats.GetComponent<HitStop>();


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
        flightTime = 2;
        ParryPart = ParryPartObject.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        horizontalInput = inputHandler.MoveInput.x;
        verticalInput = inputHandler.MoveInput.y;

        if (TextHealth == null)
        {
            GameObject TextHealthObj = GameObject.Find("Text Health");
            TextHealth = TextHealthObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            TextHealth.text = "Health: " + health;
        }


        if(health <= 0f) 
        {
            Debug.Log("Die");
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
            flightTime = stats.flightTime;
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
            dashTime -= Time.fixedDeltaTime;
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

        if (attackTime <= 0 && attackBuffer == false || Parrying)
        {
            attackCount = 0;
            isAttacking = false;
        }

        if (inputHandler.AttackTriggered || attackBuffer)
        {

            if (attackCount < 3 && attackTime > 0f && attackTime < 0.3 && attackBuffer == false && !Parrying)
            {
                attackCount += 1;
                attackBuffer = true;
            }
            /*else if (attackCount >= 2)
            {
                attackCount = 0;
            }*/

            if(attackTime <= 0 && attackBuffer == false && !Parrying)
            {
                attackCount += 1;
                canAttack = true;
            } 
            else
            {
                canAttack = false;
            }

            if(canAttack || attackBuffer && attackTime <= 0 && !Parrying)
            {
                if(attackCount > 2)
                {
                    attackCount = 1;
                }
                weaponController.Attack(attackCount);
                attackTime = 0.5f;
                attackBuffer = false;
                isAttacking = true;
                
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!Parrying)
            {
                hitStop.stop(collision.gameObject.GetComponent<EnemyStats>().StopTime);
                collision.gameObject.GetComponent<EnemyStats>();
                health -= collision.gameObject.GetComponent<EnemyStats>().damage;
                rb.AddForce(Vector3.Normalize(new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y / 1.5f)) * collision.gameObject.GetComponent<EnemyStats>().knockback);
                Debug.Log("hurt");
            }
            else if (Parrying)
            {

                hitStop.stop(stats.StopTime);
                ParryPart.Play();
                collision.gameObject.GetComponent<EnemyScript>().rb.AddForce(Vector3.Normalize(new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y / 1.5f)) * collision.gameObject.GetComponent<EnemyStats>().knockback);
                Debug.Log("Parry");
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Tags>(out var tags))
        {
            Debug.Log("HasTags");
            if (tags.HasTag("Projectile"))
            {
                if (!Parrying) 
                {
                    health -= collision.gameObject.GetComponent<ProjStats>().damage;
                    Debug.Log("hurt");
                    Destroy(collision.gameObject);
                } else if(Parrying)
                {
                    ParryPart.Play();
                    //hitStop.stop(stats.StopTime);
                    Destroy(collision.gameObject);
                }



            } else if(tags.HasTag("Transition"))
            {
                TransitionInfo transition = collision.GetComponent<TransitionInfo>();
                StartCoroutine(RoomTransport(transition.RoomTo, transition.Location, transition.bossFlight));

            }


        }
    }

    private IEnumerator RoomTransport(int room, Vector2 position, bool flight)
    {

        StartCoroutine(roomFade());
        yield return new WaitForSeconds(0.5f);
        if(flight == true)
        {
            stats.flightTime = 4f;
        } else
        {
            stats.flightTime = 0.2f;
        }
        StartCoroutine(spawner.Spawn(room, position));
        

    }

    private IEnumerator roomFade()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject Fade = GameObject.Find("Fade Out");
            Fade.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, Fade.GetComponent<SpriteRenderer>().color.a + 0.1f);
            yield return new WaitForSeconds(0.005f);
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
