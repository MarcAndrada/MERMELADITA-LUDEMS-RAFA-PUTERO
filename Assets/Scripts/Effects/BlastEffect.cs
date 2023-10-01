using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastEffect : MonoBehaviour
{
    [SerializeField] private int pointsCount;
    [SerializeField] private float maxRadius;
    [SerializeField] private float startRadius;
    [SerializeField] private float speed;
    [SerializeField] private float startWidth;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointsCount + 1;
    }

    private IEnumerator Blast()
    {
        float currentRadius = startRadius;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            yield return null;
        }
    }

    private void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360.0f / pointsCount;

        for(int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector2 dir = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
            Vector2 pos = dir * currentRadius;

            lineRenderer.SetPosition(i, pos);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Blast());
        }
    }
}
