using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Rigidbody2D rb;

    [SerializeField] private float swingStrength;
    private float swingTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;

        if(Player.GetComponent<PlayerController>().flipped == true) 
            {
                
            }
    }

    public void Attack(int attackCount)
    {
        if(Player.GetComponent<PlayerController>().flipped == true)
        {
            if (attackCount == 0)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 45);
                rb.AddTorque(-swingStrength, ForceMode2D.Impulse);
            }
            else if (attackCount == 1)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -45);
                rb.AddTorque(swingStrength, ForceMode2D.Impulse);
            }
            else if (attackCount == 2)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 45);
                rb.AddTorque(-swingStrength, ForceMode2D.Impulse);
            }
            else if (attackCount == 3)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -45);
                rb.AddTorque(swingStrength * 4, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Error");
            }
        }

        if (Player.GetComponent<PlayerController>().flipped == false)
        {
            if (attackCount == 0)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 135);
                rb.AddTorque(swingStrength, ForceMode2D.Impulse);
            }
            else if (attackCount == 1)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -135);
                rb.AddTorque(-swingStrength, ForceMode2D.Impulse);
            }
            else if (attackCount == 2)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 135);
                rb.AddTorque(swingStrength, ForceMode2D.Impulse);
            }
            else if (attackCount == 3)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, -135);
                rb.AddTorque(-swingStrength * 4, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Error");
            }
        }



    }
}
