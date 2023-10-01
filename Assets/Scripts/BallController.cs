using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{

    [SerializeField] private float speed; 
    enum State { ORBITING, SHOOTING, DEFAULT}

    private Vector3 Player;

    private Vector3 posToGo;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        posToGo = (Player - transform.position).normalized;
        rb2d.AddForce(posToGo.normalized * speed,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("BallTrigger"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default"); 
        }
    }
    public void SetPlayerPos(Vector3 _playerPos)
    {
        Player = _playerPos;
    }
}
