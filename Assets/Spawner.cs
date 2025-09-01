using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private PlayerSpawnManager spawnMan;
    [SerializeField] private GameObject player;
    void Awake() 
    {
        spawnMan = GameObject.Find("PlayerStats").GetComponent<PlayerSpawnManager>();
        spawn(spawnMan.spawnPoint);
    }

    void spawn(Vector3 spawn) 
    {
        var Player = Instantiate(player);
        Player.gameObject.transform.position = spawn;
    }
}
