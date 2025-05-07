using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    private PlayerStats stats;

    void awake() 
    {
        stats = Player.GetComponent<PlayerStats>();
    }

}
