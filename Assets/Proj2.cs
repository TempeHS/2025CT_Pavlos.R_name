using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj2 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float time;
    private Rigidbody2D rgd;
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
        time += Time.deltaTime;
        speed += Time.deltaTime;

        //rgd.AddForce();

    }
}
