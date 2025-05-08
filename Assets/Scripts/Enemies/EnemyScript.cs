using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject rangeAttack;
    private GameObject Player;
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] public float damage;
    [SerializeField] public float knockback;
    [SerializeField] public float health;

    public float usedKnockback;

    private Vector3 jumpVelocity;

    private float attackTimer = 2;
    private float useTime = 2;
    private bool knockIncrease = true;

    private bool rAttacking = false;
    private bool jAttacking = false;


    private Rigidbody2D rb;
    private bool lineOfSight = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rangeAttack.GetComponent<BoxCollider2D>().enabled = false;
        rangeAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(new Vector2(90, 90));

    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.transform.position - transform.position);
        if (ray.collider != null)
        {
            lineOfSight = ray.collider.CompareTag("Player");
            if (lineOfSight)
            {
                Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.green);
            } 
            else
            {
                Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.red);
            }
        }

        if (lineOfSight)
        {

            Vector3 targetVelocity = new Vector3(Player.transform.position.x - transform.position.x, 0, 0);
            targetVelocity = Vector3.Normalize(targetVelocity);
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                if((ray.distance < 3 && ray.distance > -3 && rAttacking != true) || jAttacking)
                {

                    Jump();

                }
                else if((ray.distance > 10 || ray.distance < -10 && jAttacking != true) || rAttacking)
                {

                    Range();

                }
                else
                {
                    rb.AddForce(targetVelocity * speed * 5);
                }
            } 
            else
            {
                usedKnockback = knockback;
                rb.AddForce(targetVelocity * speed);
            }



        }
        else
        {
            rb.AddForce(Vector3.zero);
            attackTimer = 0;
        }

    }
    private void Jump()
    {

        useTime -= Time.fixedDeltaTime;
        jAttacking = true;



        if(knockIncrease)
        {
            usedKnockback = knockback * 5;
            knockIncrease = false;
        }


        if (useTime > 1)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            jumpVelocity = new Vector3(Player.transform.position.x - transform.position.x, 0, 0);
            jumpVelocity = Vector3.Normalize(jumpVelocity);

        }
        else if (useTime > 0.98) 
        {

            rb.AddForce(jumpVelocity * speed * 10, ForceMode2D.Impulse);
        } else if(useTime > 0)
        {
            rb.AddForce(Vector3.zero);
        }
        else
        {
            jAttacking = false;
            useTime = 2;
            attackTimer = 2;
            knockIncrease = true;
        }
    }

    private void Range()
    {
        useTime -= Time.fixedDeltaTime;
        rAttacking = true; 
        Debug.Log(useTime);
        rb.velocity = Vector3.zero;
        if(useTime > 1.5)
        {
            rangeAttack.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y - 1.7f);
        }
        else if(useTime > 1)
        {
            rangeAttack.SetActive(true);
        } 
        else if(useTime > 0.5)
        {
            rangeAttack.GetComponent<BoxCollider2D>().enabled = true;
            rangeAttack.transform.position = new Vector2(rangeAttack.transform.position.x, Player.transform.position.y);
        } 
        else if(useTime > 0)
        {
            rangeAttack.GetComponent<BoxCollider2D>().enabled = false;
            rangeAttack.SetActive(false);
        } 
        else
        {
            rAttacking = false;
            useTime = 2;
            attackTimer = 2;
        }
    }

    private void die() 
    {
        if(health <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
