using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class sawControl : MonoBehaviour
{
    private Vector2 startVector;
    private BoxCollider2D sawCollision;
    private SpriteRenderer spriteRenderer;
    private CameraShake _camera;

    [SerializeField] private float changeColor;
    [SerializeField] private float speed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float fadeDelay;
    [SerializeField] private float alphaValue;

    private bool bigger;

    private GameObject player;

    private void Start()
    {
        mainCamera = Camera.main;

        #region GetComponent

        sawCollision = GetComponent<BoxCollider2D>();
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
        transform.Rotate(new Vector3(0f, 0f, -150f) * Time.deltaTime);

        Saw();
    }

    private void FixedUpdate()
    {
        _camera.ShakeCamera();
    }

    private void Saw()
    {
        if (bigger)
        {
            transform.localScale = new Vector2(startVector.x, startVector.y + speed);


            StartCoroutine(SawFade(alphaValue, fadeDelay));

            startVector = transform.localScale;

            if (startVector.y >= 0.5)
            {
                bigger = false;
                sawCollision.enabled = true;
                _camera.shakeCamera = true;
            }

        }
    }

    IEnumerator SawFade(float alphaValue, float fadeDelay)
    {
        float alpha = spriteRenderer.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeDelay)
        {
            Color newSpriteColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(alpha, alphaValue, t));
            spriteRenderer.color = newSpriteColor;
            yield return null;
        }
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
}
