using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Rigidbody2D rb;

    [SerializeField] private float swingStrength;
    private float swingTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Attack(int attackCount)
    {
    }
}
