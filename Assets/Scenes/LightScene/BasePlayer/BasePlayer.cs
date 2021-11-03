using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] float speed_rotate = 360f;
    [SerializeField] GameObject cam;

    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x, y, z;

        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(x, 0, z).normalized;

        float doublespeed = 1;
        if (Input.GetKey(KeyCode.LeftShift)) doublespeed = 2;

        transform.Translate(direction * Time.deltaTime * speed * doublespeed);

        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        yRotation = Mathf.Clamp(yRotation - y * Time.deltaTime * speed_rotate, -80f, 80f);

        transform.Rotate(new Vector3(0f, x * Time.deltaTime * speed_rotate, 0f));

        cam.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

    }
}
