using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] GameObject _sound_shag;
    [SerializeField] GameObject _sound_sword;

    public void Play_shag()
    {
        GameObject go = Instantiate(_sound_shag, transform.position, Quaternion.identity);
        go.transform.parent = transform;
        Destroy(go, 1f);
    }
    public void Play_sword()
    {
        GameObject go = Instantiate(_sound_sword, transform.position, Quaternion.identity);
        go.transform.parent = transform;
        Destroy(go, 1f);
    }
}
