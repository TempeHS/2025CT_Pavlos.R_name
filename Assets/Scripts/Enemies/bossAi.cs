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
    private GameObject Proj1;
    [SerializeField] private GameObject Proj2;

    private float attacktime;

    private bool attacking = false;
    private bool lineOfSight = false;

    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        attack1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.transform.position - transform.position);
        if (ray.collider != null)
        {
            lineOfSight = ray.collider.CompareTag("Player");
            if (lineOfSight)
            {
                attacking = true;
            }
        }
    }

    void attack1()
    {
        radProj(10);
    }

    void radProj(int numProj)
    {
        Vector2 startPoint = transform.position;
        int radius = 1;
        float angle = 0f;
        float angleStep = 360f / numProj;
        float moveSpeed = 10;

        for (int i = 0; i <= numProj - 1; i++)
        {
            float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(Proj2, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }
}
