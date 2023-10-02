using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private GameObject player;


    [SerializeField]
    private float speed;

    [SerializeField]
    private float timeToSpawnBall;

    private float timeToSpawnFirstLaser;
    [SerializeField]
    private float timeToSpawnLaser;

    private float timeToSpawnFirstSaw;
    [SerializeField]
    private float timeToSpawnSaw;

    private bool canSpawnLaser;
    private bool canSpawnSaw;

    [SerializeField]
    private TimerController timerController;

    private void Awake()
    {
        canSpawnLaser = true;
        canSpawnSaw = false;
        timeToSpawnFirstLaser = 20;
        timeToSpawnFirstSaw = 40;
    }

    private void Start()
    {
        Invoke("InstantitateBalls", timeToSpawnBall);

        Invoke("InstantitateLasers", timeToSpawnFirstLaser);

        //Invoke("InstantiateSaws", timeToSpawnSaw);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 point = map.transform.position;
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(point, axis, Time.deltaTime * speed);
    }

    private void InstantitateBalls()
    {
        BallController currentBall = Instantiate(ball, transform.position, Quaternion.identity).GetComponent<BallController>();
        currentBall.SetPlayerPos(player.transform.position);

        Invoke("InstantitateBalls", timeToSpawnBall);
    }
    private void InstantitateLasers()
    {
        LaserController currentLaser = Instantiate(laser, transform.position, Quaternion.identity).GetComponent<LaserController>();
        currentLaser.SetPlayer(player);

        Invoke("InstantitateLasers", timeToSpawnLaser);
    }

    //private void InstantiateSaw()
    //{
    //    SawController currentSaw = Instantiate(timeToSpawnFirstSaw, transform.position, Quaternion.identity).GetComponent<SawController>();
    //    currentSaw.SetPlayer(player);

    //    Invoke("InstantiateSaw", timeToSpawnSaw);
    //}

    public void SetCanSpawnLaser(bool value)
    {
        canSpawnLaser = value;
    }
    public void SetCanSpawnSaw(bool value)
    {
        canSpawnSaw = value;
    }
}
