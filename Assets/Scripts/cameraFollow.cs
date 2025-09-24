using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraFollow : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    private CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            Player = GameObject.Find("Player(Clone)");
        } else
        {

            virtualCamera.m_Follow = Player.transform;
        }


    }
}
