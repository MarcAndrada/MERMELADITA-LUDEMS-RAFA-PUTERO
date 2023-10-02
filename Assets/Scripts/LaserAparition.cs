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
    private CameraShake _camera;
    private Camera mainCamera;

    #region Float Values 
    private float alphaValue = 1;
    #endregion

    #region Boolean Values
    private bool bigger;
    private bool fadeAnimation;
    #endregion

    [SerializeField] private float changeColor;
    [SerializeField] private float speed;
    [SerializeField] private float fadeDelay;
    [SerializeField] private float laserEndSize;

    #region FlashEffect
    [SerializeField] private Material flashMaterial;

    #endregion
    private void Start()
    {
        mainCamera = Camera.main;

        #region GetComponent

        laserCollision = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = mainCamera.GetComponent<CameraShake>();

        #endregion

        startVector = transform.localScale;
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
        if (bigger)
        {
            fadeAnimation = true;

            transform.localScale = new Vector2(startVector.x, startVector.y + speed);
            startVector = transform.localScale;

            if (startVector.y >= laserEndSize)
            {
                bigger = false;
            }
        }

        if (fadeAnimation)
        {
            StartCoroutine(LaserFade(alphaValue, fadeDelay));

            if (spriteRenderer.color.a >= 0.8f)
            {
                laserCollision.enabled = true;
                _camera.shakeCamera = true;
                fadeAnimation = false;
            }
        }
    }

    IEnumerator LaserFade(float alphaValue, float fadeDelay)
    {
        float alpha = spriteRenderer.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeDelay)
        {
            Color newSpriteColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(alpha, alphaValue, t));
            spriteRenderer.color = newSpriteColor;

            yield return null;

        }
    }
    /*
    IEnumerator Flash()
    {
        spriteRenderer.material = flashMaterial;
    }
    */
}
