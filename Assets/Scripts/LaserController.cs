using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private GameObject player;
    private Rigidbody2D rb2d;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        direction = (player.transform.position - transform.position).normalized;
        transform.right = ((transform.position + (Vector3)direction) - transform.position) * -1;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = direction * speed;
    }


    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Die and hot");
        }
    }
}
