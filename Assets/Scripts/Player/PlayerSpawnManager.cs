using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Spawner;
    [SerializeField] public Vector2 spawnPoint;


    public IEnumerator Spawn(int room, Vector2 spawn)
    {
       spawnPoint = spawn;
        SceneManager.LoadScene(room);
        yield return new WaitForSeconds(1f);

    }


}
