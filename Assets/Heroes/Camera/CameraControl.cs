using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        Vector3 new_pos = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 5f);
        transform.position = new_pos;

        Quaternion new_rot = Quaternion.Lerp(transform.rotation, target.transform.rotation, Time.deltaTime * 5f);
        transform.rotation = new_rot;

    }
}
