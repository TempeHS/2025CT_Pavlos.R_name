using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Stats")]
    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] public float knockBackResist;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        } else if(Instance != null)
        {
            Destroy(this);
        }

    }
}
