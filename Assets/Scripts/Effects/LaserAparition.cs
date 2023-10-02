using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static Unity.VisualScripting.Member;

public class LaserAparition : MonoBehaviour
{
    private Vector2 startVector;
    private BoxCollider2D laserCollision;
    private SpriteRenderer spriteRenderer;
    private AudioSource aSource;
    private Material originalMaterial;
    private CameraShake _camera;
    private Camera mainCamera;

    #region Boolean Values

    private bool bigger;
    private bool small;

    private bool fadeAnimation;
    private bool unfadeAnimation;

    #endregion

    [SerializeField] private float changeColor;
    [SerializeField] private float speed;
    [SerializeField] private float fadeDelay;
    [SerializeField] private float laserEndSize;
    [SerializeField] private float alphaValue;
    [SerializeField] private float fadeDuration;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cannon;

    #region PostProcessing
    [Header("PostProcessing")]
    [SerializeField] private Volume volume;
    [SerializeField] private float intesity;
    [SerializeField] private float lensSpeed;

    private LensDistortion lensDistortion;
    #endregion

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
        aSource = GetComponent<AudioSource>();

        #endregion

        spriteRenderer.enabled = false;
        startVector = new Vector2(transform.localScale.x, 0);
        originalMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartShoot();
        }

        FadeAnimation();
        GrowLaser();
        ShrinkLaser();
        UnFadeAnimation();
    }

    private void FixedUpdate()
    {
        _camera.ShakeCamera();
    }

    public void StartShoot()
    {
        transform.position = cannon.transform.position;
        spriteRenderer.enabled = true;
        fadeAnimation = true;
        transform.right = (player.transform.position - transform.position).normalized;
    }
    
    public void FadeAnimation()
    {
        if (fadeAnimation)
        {
            StartCoroutine(LaserFade(alphaValue, fadeDelay));

            if (spriteRenderer.color.a >= alphaValue - 0.05f)
            {
                transform.localScale = new Vector2(startVector.x, 0);
                bigger = true;
                Flash();
                if (!aSource.isPlaying)
                    aSource.PlayOneShot(aSource.clip);
            }
        }
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
                unfadeAnimation = true;
                small = false;
            }
        }
    }
    public void UnFadeAnimation()
    {
        if (unfadeAnimation)
        {
            StartCoroutine(LaserUnFade(0, fadeDelay));

            if (spriteRenderer.color.a <= 0.1f)
            {
                transform.localScale = new Vector2(startVector.x, laserEndSize);
                unfadeAnimation = false;
                spriteRenderer.enabled = false;
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
    IEnumerator LaserUnFade(float alphaValue, float fadeDelay)
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
        volume.profile.TryGet(out lensDistortion);
        {
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, intesity, lensSpeed);
        }

        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);

        volume.profile.TryGet(out lensDistortion);
        {
            lensDistortion.intensity.value = 0;
        }

        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
        small = true;
        laserCollision.enabled = false;
    }

}
