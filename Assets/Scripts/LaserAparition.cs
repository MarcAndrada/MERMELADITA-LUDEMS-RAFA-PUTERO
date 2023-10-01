using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LaserAparition : MonoBehaviour
{
    private Vector2 startVector;

    private BoxCollider2D bc2d;
    private SpriteRenderer SpR;

    private float newVectorY;
    private bool bigger;

    private void Start()
    {
        startVector = transform.localScale;
        bc2d = GetComponent<BoxCollider2D>();
        SpR = GetComponent<SpriteRenderer>();
    }

    private void Laser()
    {
        if(bigger)
        {
            transform.localScale = new Vector2(startVector.x, startVector.y + 0.0005f);
            SpR.color = new Color(255, 240, 130 - 5f);
            startVector = transform.localScale;

            if(startVector.y >= 0.5) 
            {
                bigger = false;
                bc2d.enabled = true;
            }

        }   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            bigger = true;
        }

        Laser();
    }
}
