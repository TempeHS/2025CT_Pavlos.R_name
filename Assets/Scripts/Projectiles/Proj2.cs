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

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        speed += Time.fixedDeltaTime / 500;
        if (time <= 4)
        {
            Vector2 move = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            move = Vector3.Normalize(move);
            rgd.AddForce(move * speed);
        }
        else
        {
            Destroy(gameObject);
        }

        float angle = Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


    }
}
