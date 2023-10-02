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

    private bool isParried;

    [SerializeField] private ParticleSystem explosionParticle;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        explosionParticle.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        posToGo = (Player - transform.position).normalized;
        rb2d.AddForce(posToGo.normalized * speed,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isParried) 
        {
            //Matar al player
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
        
        else if (collision.gameObject.CompareTag("Ball") && isParried)
        {
            //Romper ambas pelotas

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("BallTrigger"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default"); 
        }
        else if (collider.CompareTag("Parry"))
        {
            //Hacer que la pelota este parreada
            isParried = true;
            rb2d.velocity = (transform.position - collider.transform.position).normalized * rb2d.velocity.magnitude;
            if (!explosionParticle.isPlaying)
                explosionParticle.Play();
            
               
        }
    }
    public void SetPlayerPos(Vector3 _playerPos)
    {
        Player = _playerPos;
    }
}
