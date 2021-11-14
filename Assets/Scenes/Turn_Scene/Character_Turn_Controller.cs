using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Turn_Controller : MonoBehaviour
{

    [SerializeField] Animator _anim;
    public float speed = 2f;
    public float speed_rotate = 90f;
    bool is_die;

    private void Awake()
    {
        _anim.SetFloat("Forward", -1f);
        _anim.SetFloat("Turn", 0f);
        is_die = false;
    }

    private void Update()
    {
        float iv = 0f;
        float ih = 0f;
        if (!is_die)
        {
            iv = Input.GetAxis("Vertical");
            ih = Input.GetAxis("Horizontal");
        }
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _anim.enabled = false;
            is_die = true;
        }
    }
}
