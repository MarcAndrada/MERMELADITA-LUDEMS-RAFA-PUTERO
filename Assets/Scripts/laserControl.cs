using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserControl : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private GameObject player;
    private Rigidbody2D rb2d;

    private Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        direction = (player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.localRotation = new Quaternion(transform.localRotation.x - 90, transform.localRotation.y -90, transform.localRotation.z, transform.localRotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Die and hot");
        }
    }
}
