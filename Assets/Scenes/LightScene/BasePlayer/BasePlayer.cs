using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] float speed_rotate = 360f;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject bullet_spawn;
    [SerializeField] GameObject flash_prefab;
    [SerializeField] GameObject fire_shoot_prefab;
    [SerializeField] Texture cross_fire_texture;


    float yRotation = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

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

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

    }

    void Fire()
    {
        if (Physics.Raycast(bullet_spawn.transform.position, bullet_spawn.transform.forward, out RaycastHit hit))
        {

            GameObject go;

            go = Instantiate(flash_prefab, hit.point, Quaternion.identity);
            Destroy(go, 0.1f);

            go = Instantiate(flash_prefab, bullet_spawn.transform.position + bullet_spawn.transform.forward, Quaternion.identity);
            Destroy(go, 0.1f);

            go = Instantiate(fire_shoot_prefab, bullet_spawn.transform.position, Quaternion.identity);
            Destroy(go, 1f);
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2 - 16, Screen.height / 2 - 16, 32, 32), cross_fire_texture);
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
