using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private float health;

    [SerializeField] private Tag _tagCheck;
    private int enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        search();
        /*damage = enemy.damage;
        health = enemy.health;*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void search()
    {
        if (gameObject.TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag("Enemy"))
            {
                if (tags.HasTag("Boss"))
                {
                    enemy = 2;
                }
                else
                {
                    enemy = 1;
                }
            }
        }
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
