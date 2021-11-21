using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{

    public GameObject cam; // ������ ������, ������������� � ����������

    float yRotation = 0f; // ���� �������� ������ �� Y ������ � ��������� ����������

    float speed_rotate = 360f; // �������� ��������

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Mouse Y");

        //��������� ����� ���� ������� ������
        //�� ����� �������� �� rotation.eulerAngles, �.�. ��� ���� �������� �������� �� ��� ��� �����
        //��� ���������� �� Y ������ �� ����� ����������
        yRotation = Mathf.Clamp(yRotation - y * Time.deltaTime * speed_rotate, -80f, 80f);

        //� ������������� ������ ���� �� ����� ����������
        cam.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
    }
}
