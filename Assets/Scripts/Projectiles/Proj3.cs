using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj3 : MonoBehaviour
{

    private Animator anim;
    private float timer;
    
    [SerializeField] private GameObject Proj1;

    private bool attack;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        anim.SetFloat("Timer", timer);

        if (timer >= 3   && attack == true)
        {
            release(10);
        }
    }

    void release(int numProj)
    {
        Vector2 startPoint = transform.position;
        int radius = 1;
        float angle = 0f;
        float angleStep = 360f / numProj;
        float moveSpeed = 20;

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

        attack = false;
        Destroy(gameObject);
        
    }
}
