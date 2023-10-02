using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;

    AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void Sounds()
    {
        AudioClip clip = sounds[Random.Range(0, sounds.Length)];
        aSource.PlayOneShot(clip);
    }
}
