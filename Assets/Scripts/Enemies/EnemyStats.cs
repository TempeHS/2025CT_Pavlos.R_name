using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private float health;
    private int enemy;
    
    // Start is called before the first frame update
    void Start()
    {


        /*damage = enemy.damage;
        health = enemy.health;*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void search()
    {
        
    }
    public void hurt(float plyDamage)
    {
        switch (enemy)
        {

            case 1:
                EnemyScript e1 = gameObject.GetComponent<EnemyScript>();
                e1.health -= plyDamage;
                break;

            case 2:
                bossAi e2 = gameObject.GetComponent<bossAi>();
                e2.health -= plyDamage;
                break;

        }
        
    }
}
