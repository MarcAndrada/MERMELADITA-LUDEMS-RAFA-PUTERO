using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            FindObjectOfType<HitFreeze>().Stop(2f, 0.1f);
            Debug.Log("Floor Hit");
        }
    }
}
