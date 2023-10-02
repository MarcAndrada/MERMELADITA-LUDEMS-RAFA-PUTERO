using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class sawControl : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -speed * Time.deltaTime);
    }

}
