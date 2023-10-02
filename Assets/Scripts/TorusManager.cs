using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusManager : MonoBehaviour
{
    private ExpandObjectOnContact expansion;
    private BlastEffect blast;
    private RandomSounds sound;

    public bool needScale = false;

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

    private void Update()
    {
        Scale2();
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
        needScale = true;
    }

    public void Scale2()
    {
        if (needScale)
        {
            timer += Time.deltaTime / scaleDuration;
            Vector3 newScale = Vector3.Lerp(startScale, finalScale, timer);
            transform.localScale = newScale;
            if (timer > 1.0f) 
            {
                needScale = false;
                transform.localScale = finalScale;
            }
        }
    }
}
