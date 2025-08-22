using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject player;


    public IEnumerator Spawn(int room, Vector2 spawn)
    {
        SceneManager.LoadScene(room);
        yield return new WaitForSeconds(1f);
        var Player = Instantiate(player);
        Player.gameObject.transform.position = spawn;
    }


}
