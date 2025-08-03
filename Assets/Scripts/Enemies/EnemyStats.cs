using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField] public float damage;
    [SerializeField] public float health;
    [SerializeField] public float knockback;

    [SerializeField] private Tag _tagCheck;
    private int enemy;
    [SerializeField] public float StopTime;
    
    // Start is called before the first frame update

}
