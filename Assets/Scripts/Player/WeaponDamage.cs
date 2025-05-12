using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    private PlayerStats stats;
    [SerializeField] private float damage;

    void awake() 
    {
        stats = Player.GetComponent<PlayerStats>();
    }

    void update() 
    {
        damage = stats.damage;
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().health -= damage;
            Debug.Log("Hit");
        }
    }

}
