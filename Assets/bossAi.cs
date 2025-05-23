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

    private bool attacking = false;
    private bool lineOfSight = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
}
