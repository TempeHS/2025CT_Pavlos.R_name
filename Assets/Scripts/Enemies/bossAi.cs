using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

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
    [SerializeField] private GameObject Proj3;
    [SerializeField] private GameObject Proj4;
    [SerializeField] private GameObject Proj5;
    [SerializeField] private GameObject Proj6;
    [SerializeField] private GameObject Dopple;
    [SerializeField] private DoppleGanger DoppleScript;
    [SerializeField] private Tilemap FloorTile;
    [SerializeField] private bool floorFade;

    private Color currentFloorColor;

    public bool Proj4Attack;

    private float attacktime;

    private bool attacking = false;
    private int attackNum;
    private bool lineOfSight = false;

    public TextMeshProUGUI TextHealth;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        attacking = true;
        health = 150;
        Proj4Attack = false;
        floorFade = false;
}

    // Update is called once per frame
    void Update()
    {
        TextHealth.text = "Boss Health: " + health;

    }



    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (attacking)
        {
            StartCoroutine(randAttack());
        }

        if(floorFade && currentFloorColor.a > 0)
        {

            currentFloorColor = FloorTile.color;
            currentFloorColor.a -= Time.fixedDeltaTime;
            FloorTile.color = currentFloorColor;
        } else if (!floorFade && currentFloorColor.a < 1)
        {

            currentFloorColor = FloorTile.color;
            currentFloorColor.a += Time.fixedDeltaTime;
            FloorTile.color = currentFloorColor;
        }
    }

    IEnumerator randAttack()
    {
        attacking = false;

        attackNum = Random.Range(6, 7);
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
        else if (attackNum == 4)
        {
            StartCoroutine(attack4());
        }
        else if (attackNum == 5)
        {
            StartCoroutine(attack5());
        }
        else if (attackNum == 6)
        {
            StartCoroutine(attack6());
        }

        yield return new WaitForSeconds(8f);
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

    private IEnumerator attack4()
    {
        floorFade = true;
        yield return new WaitForSeconds(1);
        Proj4Attack = true;
        float radius = 12;
        int angle;
        angle = Random.Range(1, 361);

        Debug.Log("attack4");

        float projectileDirXposition = Player.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
        float projectileDirYposition = Player.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

        float negProjectileDirXposition = Player.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius * -1;
        float negProjectileDirYposition = Player.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius * -1;

        Vector2 spawnPoint = new Vector2(projectileDirXposition, projectileDirYposition);
        Vector2 negSpawnPoint = new Vector2(negProjectileDirXposition, negProjectileDirYposition);

        var proj = Instantiate(Proj3, spawnPoint, Quaternion.identity);
        Instantiate(Proj4, negSpawnPoint, Quaternion.identity);

        proj.GetComponent<Proj5>().Boss = this;

        StartCoroutine(Proj4Time());
    }

    private IEnumerator attack5()
    {
        floorFade = true;
        yield return new WaitForSeconds(1);
        //Proj4Attack = true;
        float radius = 20;
        int angle;
        angle = Random.Range(1, 361);
        int angleStep = 7;

        Vector2 PlayerPoint = Player.transform.position;
        StartCoroutine(Proj4Time());
        for (int i = 0; i < 96; i++)
        {
            float projectileDirXposition = PlayerPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = PlayerPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            float negProjectileDirXposition = PlayerPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius * -1;
            float negProjectileDirYposition = PlayerPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius * -1;


            Vector2 spawnPoint = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 negSpawnPoint = new Vector2(negProjectileDirXposition, negProjectileDirYposition);

            var proj = Instantiate(Proj5, spawnPoint, Quaternion.identity);
            var proj2 = Instantiate(Proj5, negSpawnPoint, Quaternion.identity);

            proj.GetComponent<Proj8>().Boss = this;
            proj.GetComponent<Proj8>().PlayerPoint = PlayerPoint;
            proj2.GetComponent<Proj8>().Boss = this;
            proj2.GetComponent<Proj8>().PlayerPoint = PlayerPoint;
            Instantiate(Proj4, PlayerPoint, Quaternion.identity);

            angle += angleStep;
            yield return new WaitForSeconds(0.04f);
        }
    }

    private IEnumerator attack6()
    {
        Vector2 startPoint = transform.position;
        float moveSpeed = 10;


        var proj = Instantiate(Proj6, startPoint, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed);

        yield return new WaitForSeconds(2);

        proj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }

    private IEnumerator Proj4Time()
    {
        yield return new WaitForSeconds(4);
        Proj4Attack = false;
        Debug.Log("attack false");

        yield return new WaitForSeconds(1);
        floorFade = false;
    }


    public void attack2Con(int numProj)
    {
        Vector2 startPoint = transform.position;
        int radius = 1;
        float angle = 0f;
        float angleStep = 90f / (numProj - 1);
        float moveSpeed = 20;

        angle = (Mathf.Atan2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y) * Mathf.Rad2Deg) - 45;

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
        float angleStep = 90f / (numProj - 1);
        float moveSpeed = 20;

        angle = (Mathf.Atan2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y) * Mathf.Rad2Deg) - 45;

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

    public void hurt(float damage) 
    {
        health -= damage;
    }
}
