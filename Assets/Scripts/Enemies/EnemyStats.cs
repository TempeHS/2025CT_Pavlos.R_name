using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private float health;
    private EnemyScript enemy;
    // Start is called before the first frame update
    void Start()
    {

        enemy = gameObject.GetComponent<EnemyScript>();
        damage = enemy.damage;
        health = enemy.health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void search()
    {

        enemy = gameObject.GetComponent<EnemyScript>();

        if (enemy == null)
        {

        }
    }
    public void hurt(float plyDamage)
    {
        enemy.health -= plyDamage;
    }
}
