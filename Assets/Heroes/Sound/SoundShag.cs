using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundShag : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    [SerializeField] AudioSource _source;

    private void Awake()
    {
        _source.clip = _clips[Random.Range(0, _clips.Length)];
        _source.Play();

    }
}
