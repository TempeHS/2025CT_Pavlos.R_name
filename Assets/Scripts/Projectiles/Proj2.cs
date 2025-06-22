using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj2 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float time;
    private Rigidbody2D rgd;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        time = 0;
        rgd = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        speed += Time.deltaTime / 10000;

        Vector2 move = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        move = Vector3.Normalize(move);
        rgd.AddForce(move * speed);

    }
}
