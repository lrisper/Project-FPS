using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _axeSounds;

    void PlaySounds()
    {
        _audioSource.clip = _axeSounds[Random.Range(0, _axeSounds.Length)];
        _audioSource.Play();

    }
}

