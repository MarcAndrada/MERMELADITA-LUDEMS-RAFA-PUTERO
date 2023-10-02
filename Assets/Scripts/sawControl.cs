using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class sawControl : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float spawnSpeed;

    [SerializeField]
    private float minScale;
    [SerializeField]
    private float maxScale;

    private float scaleProcess;

    private AudioSource aSource;
    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        transform.localScale = Vector3.zero;    
    }

    private void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -speed * Time.deltaTime);

        if (transform.localScale.x != maxScale)
        {
            scaleProcess += Time.deltaTime * spawnSpeed;
            transform.localScale = Vector3.Lerp(
                    new Vector3(minScale, minScale, minScale),
                    new Vector3(maxScale, maxScale, maxScale),
                    scaleProcess
                );
        }
        else
        {
            transform.localScale = new Vector3(maxScale, maxScale, maxScale);   
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Die();
        }
    }

}
