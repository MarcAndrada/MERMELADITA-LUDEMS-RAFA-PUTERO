using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantLaserAim : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerPos;


    private void Start()
    {
        playerPos = player.transform.position;
    }

    private void Update()
    {
    }
}
