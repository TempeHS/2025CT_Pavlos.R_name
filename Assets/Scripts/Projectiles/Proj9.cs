using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj9 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DeathTime());
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.forward = this.GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}
