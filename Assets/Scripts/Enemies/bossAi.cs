using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAi : MonoBehaviour
{
    private GameObject Player;

    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] public float damage;
    [SerializeField] public float knockback;
    [SerializeField] public float health;
    [SerializeField] private GameObject Proj1;
    [SerializeField] private GameObject Proj2;
    [SerializeField] private GameObject Dopple;
    [SerializeField] private DoppleGanger DoppleScript;

    private float attacktime;

    private bool attacking = false;
    private int attackNum;
    private bool lineOfSight = false;

    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        attacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            StartCoroutine(randAttack()); 
        }
    }

    void FixedUpdate()
    {
        
    }

    IEnumerator randAttack()
    {
        attacking = false;

        attackNum = Random.Range(1, 3);
        if (attackNum == 1)
        {
            attack1();
        }
        else if (attackNum == 2)
        {
            attack2();
        }
        else if (attackNum == 3)
        {
            attack3();
        }
        yield return new WaitForSeconds(10f);
        attacking = true;
    }
    void attack1()
    {
        radProj(10);
    }

    void attack2()
    {
        Dopple.SetActive(true);
        DoppleScript.StartSpread();
    }

    void attack3()
    {
        Dopple.SetActive(true);
        DoppleScript.StartSpread2();
    }
    public void attack2Con(int numProj)
    {
        Vector2 startPoint = transform.position;
        int radius = 1;
        float angle = 0f;
        float angleStep = 90f / numProj;
        float moveSpeed = 20;

        angle = (Mathf.Atan2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y) * Mathf.Rad2Deg) - angleStep;

        for (int i = 0; i <= numProj - 1; i++)
        {
            float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(Proj1, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }
    public void attack3Con(int numProj)
    {
        Vector2 startPoint = transform.position;
        int radius = 1;
        float angle = 0f;
        float angleStep = 90f / 3;
        float moveSpeed = 20;

        angle = (Mathf.Atan2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y) * Mathf.Rad2Deg) - 15;

        for (int i = 0; i <= numProj - 1; i++)
        {
            float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(Proj1, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

    void radProj(int numProj)
    {
        Vector2 startPoint = transform.position;
        float moveSpeed = 10;


        var proj = Instantiate(Proj2, startPoint, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed);

    }
}
