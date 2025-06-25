using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj1 : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject Proj2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.forward = this.GetComponent<Rigidbody2D>().velocity;
         float angle = Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("destroy");

            Destroy(gameObject);
            Instantiate(Proj2, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }
}
