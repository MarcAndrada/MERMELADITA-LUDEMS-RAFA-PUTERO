using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class LaserAparition : MonoBehaviour
{
    private Vector2 startVector;
    private BoxCollider2D laserCollision;
    private SpriteRenderer sRenderer;

    [SerializeField] private float speed;

    private float newVectorY;
    private bool bigger;

    private void Start()
    {
        startVector = transform.localScale;
        laserCollision = GetComponent<BoxCollider2D>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            bigger = true;
        }

        Laser();
    }
    private void Laser()
    {
        if(bigger)
        {
            transform.localScale = new Vector2(startVector.x, startVector.y + speed);
            sRenderer.color = new Color(255, 240, 130 - 5f);
            startVector = transform.localScale;

            if(startVector.y >= 0.5) 
            {
                bigger = false;
                laserCollision.enabled = true;
            }

        }   
    }

  
}
