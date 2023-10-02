using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusManager : MonoBehaviour
{
    private ExpandObjectOnContact expansion;
    private BlastEffect blast;
    private RandomSounds sound;

    [SerializeField]
    private Vector3 finalScale;
    private Vector3 startScale;
    [SerializeField]
    private float scaleDuration;

    float timer = 0.0f;

    private void Awake()
    {
        expansion = GetComponent<ExpandObjectOnContact>();
        blast = GetComponent<BlastEffect>();
        sound = GetComponent<RandomSounds>();
        startScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            StopAllCoroutines();
            StartCoroutine(expansion.GrowAndShrink());
            StartCoroutine(blast.Blast());
            sound.Sounds();
        }
    }

    public void Scale()
    {
        while (timer <= 1.0)
        {
            Debug.Log("aaa");
            timer += Time.deltaTime * scaleDuration;
            Vector3 newScale = Vector3.Lerp(startScale, finalScale, timer);
            transform.localScale = newScale;
        }
    }
}
