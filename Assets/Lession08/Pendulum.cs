using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    public GameObject mass;
    public GameObject cylinder;

    public float phi = Mathf.PI * 0.2f;
    public float a;
    public float v;

    float g;
    public float l = 10f;

    public float new_l = 10f;
    // Start is called before the first frame update
    void Start()
    {
        g = -Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {

        float last_v = v;

        a = -(g / l) * Mathf.Sin(phi);
        v += a * Time.deltaTime;
        phi += v * Time.deltaTime;

        if ((last_v >= 0 && v <= 0) || (last_v <= 0 && v >= 0)) l = new_l;

        mass.transform.position = new Vector3(Mathf.Sin(phi) * new_l, -Mathf.Cos(phi) * new_l);

        cylinder.transform.position = new Vector3(Mathf.Sin(phi) * new_l / 2f, -Mathf.Cos(phi) * new_l / 2f);
        cylinder.transform.rotation = Quaternion.Euler(0f, 0f, phi * Mathf.Rad2Deg);
        cylinder.transform.localScale = new Vector3(0.1f, new_l / 2f, 0.1f);
    }

    private void OnGUI()
    {
        new_l = GUI.HorizontalSlider(new Rect(10, 10, 300, 100), new_l, 1f, 20f);
    }
}
