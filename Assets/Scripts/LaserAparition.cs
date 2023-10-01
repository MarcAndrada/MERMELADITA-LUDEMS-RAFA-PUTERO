using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class LaserAparition : MonoBehaviour
{
    private Vector2 startVector;
    private BoxCollider2D laserCollision;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Camera mainCamera;
    private CameraShake _camera;

    [SerializeField]
    private float R;
    [SerializeField]
    private float G;
    [SerializeField]
    private float B;
    [SerializeField]
    private float A;
    [SerializeField]
    private float changeColor;

    [SerializeField] private float speed;

    private bool bigger;

    private void Start()
    {
        mainCamera = Camera.main;
        startVector = transform.localScale;
        laserCollision = GetComponent<BoxCollider2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        _camera = mainCamera.GetComponent<CameraShake>();
        A = spriteRenderer.color.a;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            bigger = true;
        }

        Laser();
    }

    private void FixedUpdate()
    {
        _camera.ShakeCamera();
    }
    private void Laser()
    {
        if(bigger)
        {
            transform.localScale = new Vector2(startVector.x, startVector.y + speed);
            spriteRenderer.color = new Color(R, G, B, A + changeColor);
            A = spriteRenderer.color.a;
            startVector = transform.localScale;

            if(startVector.y >= 0.5) 
            {
                bigger = false;
                laserCollision.enabled = true;
                _camera.shakeCamera = true;
            }

        }   
    }

  
}
