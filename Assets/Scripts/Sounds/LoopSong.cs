using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSong : MonoBehaviour
{
    private static LoopSong audioManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
}
