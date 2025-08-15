using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj8 : MonoBehaviour
{

    private float timer;
    
    [SerializeField] private Material swirl;
    [SerializeField] private GameObject Proj1;
    [SerializeField] private GameObject Player;
    [SerializeField] public bossAi Boss;

    public Vector2 PlayerPoint;

    private SpriteRenderer rend;

    private bool attack;
    // Start is called before the first frame update

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material = new Material(rend.material);
    }
    void Start()
    {
        swirl = rend.material;
        attack = true;
        Player = GameObject.Find("Player");
        StartCoroutine(release());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        /*if (timer >= 3   && attack == true)
        {
            StartCoroutine(release());
        }*/
    }

    IEnumerator release(/*int numProj*/)
    {

            Vector2 startPoint = transform.position;
            int radius = 1;
            float angle = Mathf.Atan2(PlayerPoint.x - transform.position.x, PlayerPoint.y - transform.position.y) * Mathf.Rad2Deg;
            float moveSpeed = 60;
            float strong = 0;
            swirl.SetFloat("_strength", 0);
            swirl.SetFloat("_Radius", 0);
            
            
            for (int i = 0; i <= 20; i++)
        {


            strong += 0.05f;
            swirl.SetFloat("_strength", strong);
            swirl.SetFloat("_Radius", strong);
            yield return new WaitForSeconds(0.0125f);
        }


            Debug.Log("Angle: " + angle);
            for (int i = 0; i <= 1; i++)
            {
                float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

                var proj = Instantiate(Proj1, startPoint, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                yield return new WaitForSeconds(0.0375f);
            }


            for (int i = 0; i <= 20; i++)
            {


                strong -= 0.05f;
                swirl.SetFloat("_strength", strong);
                swirl.SetFloat("_Radius", strong);
                yield return new WaitForSeconds(0.0125f);
            }

            attack = false;
            Destroy(gameObject);


    }
}
