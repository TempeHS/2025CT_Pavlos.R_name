using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    [SerializeField] private GameObject PlayerStats;
    private PlayerStats stats;
    [SerializeField] private float damage;

    void Awake() 
    {
        PlayerStats = GameObject.Find("PlayerStats");
        stats = PlayerStats.GetComponent<PlayerStats>();
    }

    void Update() 
    {
        damage = stats.damage;
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
            //collision.gameObject.GetComponent<EnemyStats>().hurt(damage);
        }
    }

}
