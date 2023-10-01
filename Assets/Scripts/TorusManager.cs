using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusManager : MonoBehaviour
{
    private ExpandObjectOnContact expansion;
    private BlastEffect blast;

    private void Awake()
    {
        expansion = GetComponent<ExpandObjectOnContact>();
        blast = GetComponent<BlastEffect>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            StopAllCoroutines();
            StartCoroutine(expansion.GrowAndShrink());
            StartCoroutine(blast.Blast());
        }
    }
}
