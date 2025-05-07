using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] public float knockBackResist;
}
