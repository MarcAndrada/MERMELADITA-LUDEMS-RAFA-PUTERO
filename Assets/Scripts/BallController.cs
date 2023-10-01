using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    enum State { ORBITING, SHOOTING, DEFAULT}
    [SerializeField]
    private Transform spawnTransform;

    private Vector3 Player;

    private Vector3 posToGo;

    private Rigidbody2D rb2d;
    private CircleCollider2D collider2d; 

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CircleCollider2D>();
        collider2d.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        posToGo = (Player - transform.position).normalized;
        rb2d.AddForce(posToGo.normalized * 10,ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collider2d.enabled = true;
    }

    public void SetPlayerPos(Vector3 _playerPos)
    {
        Player = _playerPos;
    }
}
