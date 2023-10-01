using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private GameObject player;


    [SerializeField]
    private float speed; 

    Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstantitateEnemies();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 point = map.transform.position;
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(point, axis, Time.deltaTime * speed);
    }

    private void InstantitateEnemies()
    {

        BallController currentBall = Instantiate(ball, transform.position, Quaternion.identity).GetComponent<BallController>();
        currentBall.SetPlayerPos(player.transform.position);
    }
}
