using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject _male;
    [SerializeField] GameObject _female;
    [SerializeField] Animator _male_anim;
    [SerializeField] Animator _female_anim;
    Animator _anim;
    
    bool _ismale;
    PlayerCustom playerCustom;

    public float speed;
    public float mouse_speed;

    PlayerCustomGUI playerCustomGUI;

    GameObject cam;
    [SerializeField] GameObject normal_cam_position;
    CameraControl cameraControl;

    float sprint_value;

    void SetCameraTarget()
    {
        cameraControl.target = normal_cam_position;
    }

    

    private void Awake()
    {

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraControl = cam.GetComponent<CameraControl>();
        SetCameraTarget();

        Global.SetCursor(false);

        playerCustom = GetComponent<PlayerCustom>();

        //При старте отключим кнопку настроки
        playerCustomGUI = GetComponent<PlayerCustomGUI>();
        playerCustomGUI.enabled = false;

        sprint_value = 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public 

    void CheckAnimator()
    {
        if (playerCustom.male != _ismale)
        {
            _ismale = playerCustom.male;
            if (_ismale)
            {
                _anim = _male_anim;
            }
            else
            {
                _anim = _female_anim;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimator();

        SetCameraTarget();

        bool mouse0 = Input.GetMouseButtonDown(0);
        bool attackState = _anim.GetCurrentAnimatorStateInfo(0).IsName("Male Attack 1") || _anim.GetCurrentAnimatorStateInfo(0).IsName("Female Sword Attack 1");

        if (mouse0 && !attackState)
        {
            _anim.SetTrigger("Attack");
            Debug.Log("Attack");
        }

        float z = 0;
        float x = 0;

        if (!mouse0 && !attackState)
        {
            z = Input.GetAxis("Vertical");
            x = Input.GetAxis("Horizontal");
        }

        Vector3 direction = new Vector3(0, 0, z).normalized;

        if (direction == Vector3.zero) _anim.SetBool("Walk", false); else _anim.SetBool("Walk", true);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint_value = Mathf.Clamp(sprint_value + Time.deltaTime, 1f, 2f);
            _anim.SetBool("Sprint", true);
        }
        else
        {
            sprint_value = Mathf.Clamp(sprint_value - Time.deltaTime, 1f, 2f);
            _anim.SetBool("Sprint", false);
        }

        transform.Translate(direction * speed * sprint_value * Time.deltaTime);
        _anim.SetFloat("Run Blend", z / 2f * sprint_value);

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    _anim.SetBool("Sprint", true);
        //    transform.Translate(direction * speed * 2 * Time.deltaTime);
        //    _anim.SetFloat("Run Blend", z);
        //}
        //else
        //{
        //    _anim.SetBool("Sprint", false);
        //    transform.Translate(direction * speed * Time.deltaTime);
        //    _anim.SetFloat("Run Blend", z / 2f);
        //}
        

        

        float mx = Input.GetAxis("Mouse X");

        Vector3 euler = new Vector3(0, mx * mouse_speed * Time.deltaTime, 0);

        transform.Rotate(euler);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _anim.SetBool("Walk", false);
            _anim.SetBool("Sprint", false);
            playerCustomGUI.enabled = true;
            Global.SetCursor(true);
            this.enabled = false;
        }

    }

    private void OnDestroy()
    {
        Global.SetCursor(true);
    }
}
