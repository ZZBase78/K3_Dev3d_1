using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{

    public GameObject cam; // Объект камеры, прикрепляется в инспекторе

    float yRotation = 0f; // Угол вращения камеры по Y держим в отдельной переменной

    float speed_rotate = 360f; // скорость вращения

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Mouse Y");

        //Вычисляем новый угол поворта камеры
        //Не берем значения из rotation.eulerAngles, т.к. там углы вращения меняются не так как нужно
        //Все вычисление по Y делаем по нашей переменной
        yRotation = Mathf.Clamp(yRotation - y * Time.deltaTime * speed_rotate, -80f, 80f);

        //И устанавливаем нужный угол из нашей переменной
        cam.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
    }
}
