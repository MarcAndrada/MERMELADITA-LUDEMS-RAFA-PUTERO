using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class LaserAparition : MonoBehaviour
{
    private Vector2 startVector;
    private BoxCollider2D laserCollision;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private CameraShake _camera;
    private Camera mainCamera;

    #region Boolean Values

    private bool bigger;
    private bool small;

    private bool fadeAnimation;

    #endregion

    [SerializeField] private float changeColor;
    [SerializeField] private float speed;
    [SerializeField] private float fadeDelay;
    [SerializeField] private float laserEndSize;
    [SerializeField] private float alphaValue;

    #region FlashEffect
    [Header("FlashEffect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    private Coroutine flashRoutine;
    #endregion

    private void Start()
    {
        mainCamera = Camera.main;

        #region GetComponent

        laserCollision = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = mainCamera.GetComponent<CameraShake>();

        #endregion

        spriteRenderer.enabled = false;
        startVector = new Vector2(transform.localScale.x, 0);
        originalMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spriteRenderer.enabled = true;
            fadeAnimation = true;
        }
        FadeAnimation();
        GrowLaser();
        ShrinkLaser();
    }

    private void FixedUpdate()
    {
        _camera.ShakeCamera();
    }

    private void GrowLaser()
    {
        if (bigger)
        {
            transform.localScale = new Vector2(startVector.x, startVector.y + speed);
            startVector = transform.localScale;

            if (startVector.y >= laserEndSize)
            {
                laserCollision.enabled = true;
                _camera.shakeCamera = true;
                fadeAnimation = false;
                bigger = false;
            }
        }
    }
    private void ShrinkLaser()
    {
        if (small)
        {
            transform.localScale = new Vector2(startVector.x, startVector.y - speed);
            startVector = transform.localScale;

            if (startVector.y < 0)
            {
                Destroy(gameObject);
                small = false;
            }
        }
    }

    public void FadeAnimation()
    {
        if (fadeAnimation)
        {
            StartCoroutine(LaserFade(alphaValue, fadeDelay));

            if (spriteRenderer.color.a >= alphaValue - 0.05f)
            {
                // Reset Size
                transform.localScale = new Vector2(startVector.x, 0);
                bigger = true;
                Flash();
            }
        }
    }

    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
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
    
    IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
        small = true;
    }

}
