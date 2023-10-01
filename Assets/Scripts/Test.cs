using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    bool ballExpand = false;
    float ballSizeMult = 0.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 2, 3f);
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale / 2, 3f);

        }
    }

    IEnumerator Expand(float duration, float expansion)
    {
        ballExpand = true;

        yield return new WaitForSeconds(duration);
        ballSizeMult = expansion;

        ballExpand = false;
    }
}
