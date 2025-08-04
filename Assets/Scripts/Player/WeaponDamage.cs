using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    [SerializeField] private GameObject PlayerStats;
    private PlayerStats stats;
    [SerializeField] private float damage;
    [SerializeField] private GameObject Boss;

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
            
            collision.gameObject.GetComponent<bossAi>().hurt(damage);
        } else if(collision.gameObject.tag == "BossSpawn") 
        {
            Destroy(collision.gameObject);
            Instantiate(Boss, new Vector3(95.752f, 27.50237f, 0f), Quaternion.identity);
        }
    }

}
