using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogAudio : MonoBehaviour
{
    public AudioSource hit;
    public void PlayHitSound()
    {
        hit.Play();
    }
}
