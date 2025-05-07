using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerEmpty : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
       controller = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
    }

}
