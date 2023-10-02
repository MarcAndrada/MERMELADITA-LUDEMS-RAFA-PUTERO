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
    public GameObject sierra;
    [SerializeField]
    private GameObject chargedLaser;


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

    [SerializeField]
    private float timeToSpawnChargedLaser;

    [SerializeField]
    private TimerController timerController;

    private void Awake()
    {
        timeToSpawnFirstLaser = 20;
        timeToSpawnFirstSaw = 30;
    }

    private void Start()
    {
        Invoke("InstantitateBalls", timeToSpawnBall);

        Invoke("InstantitateLasers", timeToSpawnFirstLaser);

        Invoke("InstantiateSaws", timeToSpawnFirstSaw);
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

    private void InstantiateSaws()
    {
        sawControl currentSaw = Instantiate(sierra, transform.position, Quaternion.identity).GetComponent<sawControl>();
        currentSaw.SetPlayer(player);

        Invoke("InstantiateSaws", timeToSpawnSaw);
    }

    public void SetEnabled(bool value)
    {
        this.enabled = value;
    }
}