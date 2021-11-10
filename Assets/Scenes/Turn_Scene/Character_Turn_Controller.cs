using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Turn_Controller : MonoBehaviour
{

    [SerializeField] Animator _anim;
    public float speed = 2f;
    public float speed_rotate = 90f;

    private void Awake()
    {
        _anim.SetFloat("Forward", -1f);
        _anim.SetFloat("Turn", 0f);
    }

    private void Update()
    {
        float iv = Input.GetAxis("Vertical");
        float ih = Input.GetAxis("Horizontal");
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        float v = iv;
        float h = ih;

        if (v < 0f) v = 0f;

        if (sprint) v *= 2f;

        v -= 1f;

        _anim.SetFloat("Forward", v, 0.1f, Time.deltaTime);
        _anim.SetFloat("Turn", h, 0.1f, Time.deltaTime);

        Vector3 direction = new Vector3(0f, 0f, iv);

        transform.Translate(direction * speed * (sprint ? 2 : 1) * Time.deltaTime);

        transform.Rotate(0f, h * speed_rotate * Time.deltaTime, 0f);
    }
}
