using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            Player = GameObject.Find("Player(Clone)");
        }
        transform.position = Player.transform.position;
    }
}
