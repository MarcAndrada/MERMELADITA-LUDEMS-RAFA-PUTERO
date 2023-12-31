using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BallController : MonoBehaviour
{
    #region ballVariables
    [SerializeField] private float speed;
    #endregion

    #region Collision
    private Vector3 Player;
    private Vector3 posToGo;
    private Rigidbody2D rb2d;
    private Collider2D col;
    private bool isParried;
    #endregion

    #region Effects
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private Color parryColor;

    private SpriteRenderer spriteRenderer;
    private HitFreeze hitFreeze;
    private AudioManager audioManager;
    #endregion

    private void Awake()
    {
        hitFreeze = GetComponent<HitFreeze>();
        rb2d = GetComponent<Rigidbody2D>();
        explosionParticle.Stop();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager._instance;
        posToGo = (Player - transform.position).normalized;
        rb2d.AddForce(posToGo.normalized * speed,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isParried) 
        {
            //Matar al player
            collision.gameObject.GetComponent<PlayerController>().Die();
            Destroy(gameObject);
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
        if (collider.CompareTag("BallTrigger") && gameObject.layer == LayerMask.NameToLayer("OutsideBall"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default"); 
        }
        else if (collider.CompareTag("Parry") )
        {
            //Hacer que la pelota este parreada
            isParried = true;
            gameObject.layer = LayerMask.NameToLayer("Parried");
            rb2d.velocity = (transform.position - collider.transform.position).normalized * rb2d.velocity.magnitude;
            if (!explosionParticle.isPlaying)
            {
                explosionParticle.Play();
            }
            //Make Sound
            spriteRenderer.color = parryColor;
            hitFreeze.Stop(0.025f, 0.5f);
        }
    }
    public void SetPlayerPos(Vector3 _playerPos)
    {
        Player = _playerPos;
    }

    public void SetCollisions(bool value)
    {
        col.enabled = value;
    }
}
