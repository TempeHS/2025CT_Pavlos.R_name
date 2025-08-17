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

    [SerializeField] private Material bossMat;
    private SpriteRenderer rend;

    private Vector2 playerPos;

    private Color currentFloorColor;

    public bool Proj4Attack;

    private float attacktime;

    private bool attacking = false;
    private int attackNum;
    private bool lineOfSight = false;

    public TextMeshProUGUI TextHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material = new Material(rend.material);
    }
    void Start()
    {
        bossMat = rend.material;
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
            bossMat.SetFloat("_Alpha", currentFloorColor.a);
            FloorTile.color = currentFloorColor;

        } else if (!floorFade && currentFloorColor.a < 1)
        {

            currentFloorColor = FloorTile.color;
            currentFloorColor.a += Time.fixedDeltaTime;
            bossMat.SetFloat("_Alpha", currentFloorColor.a);
            FloorTile.color = currentFloorColor;
        }

        if(currentFloorColor.a <= 0)
        {
            FloorTile.gameObject.SetActive(false);
        } else
        {
            FloorTile.gameObject.SetActive(true);
        }
    }

    IEnumerator randAttack()
    {
        attacking = false;

        attackNum = Random.Range(1, 8);
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
        else if (attackNum == 7)
        {
            StartCoroutine(attack7());
        }

        yield return new WaitForSeconds(9f);
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

        playerPos = Player.transform.position;

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

        playerPos = Player.transform.position;

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

    private IEnumerator attack7()
    {
        floorFade = true;
        yield return new WaitForSeconds(1);

        playerPos = Player.transform.position;

        //Proj4Attack = true;
        float radius = 20;
        int angle;
        angle = Random.Range(1, 361);
        int angleStep = 7;

        Vector2 PlayerPoint = Player.transform.position;
        StartCoroutine(Proj4Time());
        for (int i = 0; i < 48; i++)
        {
            float projectileDirXposition = PlayerPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = PlayerPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            float negProjectileDirXposition = PlayerPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius * -1;
            float negProjectileDirYposition = PlayerPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius * -1;

            float projectileDirXposition2 = PlayerPoint.x + Mathf.Sin(((angle + 90) * Mathf.PI) / 180) * radius;
            float projectileDirYposition2 = PlayerPoint.y + Mathf.Cos(((angle + 90) * Mathf.PI) / 180) * radius;

            float negProjectileDirXposition2 = PlayerPoint.x + Mathf.Sin(((angle + 90) * Mathf.PI) / 180) * radius * -1f;
            float negProjectileDirYposition2 = PlayerPoint.y + Mathf.Cos(((angle + 90) * Mathf.PI) / 180) * radius * -1f;


            Vector2 spawnPoint = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 negSpawnPoint = new Vector2(negProjectileDirXposition, negProjectileDirYposition);

            Vector2 spawnPoint2 = new Vector2(projectileDirXposition2, projectileDirYposition2);
            Vector2 negSpawnPoint2 = new Vector2(negProjectileDirXposition2, negProjectileDirYposition2);

            var proj = Instantiate(Proj5, spawnPoint, Quaternion.identity);
            var proj2 = Instantiate(Proj5, negSpawnPoint, Quaternion.identity);

            var proj3 = Instantiate(Proj5, spawnPoint2, Quaternion.identity);
            var proj4 = Instantiate(Proj5, negSpawnPoint2, Quaternion.identity);

            proj.GetComponent<Proj8>().Boss = this;
            proj.GetComponent<Proj8>().PlayerPoint = PlayerPoint;
            proj2.GetComponent<Proj8>().Boss = this;
            proj2.GetComponent<Proj8>().PlayerPoint = PlayerPoint;

            proj3.GetComponent<Proj8>().Boss = this;
            proj3.GetComponent<Proj8>().PlayerPoint = PlayerPoint;
            proj4.GetComponent<Proj8>().Boss = this;
            proj4.GetComponent<Proj8>().PlayerPoint = PlayerPoint;
            Instantiate(Proj4, PlayerPoint, Quaternion.identity);

            angle += angleStep;
            yield return new WaitForSeconds(0.08f);
        }
    }

    private IEnumerator Proj4Time()
    {
        yield return new WaitForSeconds(4);
        Proj4Attack = false;
        Debug.Log("attack false");

        yield return new WaitForSeconds(1.95f);
        Player.transform.position = playerPos;
        yield return new WaitForSeconds(0.05f);
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
