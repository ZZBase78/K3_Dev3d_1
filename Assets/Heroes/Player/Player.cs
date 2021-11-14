using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject _male;
    [SerializeField] GameObject _male_head;
    [SerializeField] GameObject _female;
    [SerializeField] GameObject _female_head;
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

    Collider[] colliders;

    GameObject go_interes;
    GameObject last_interes;

    float look_at_weight;

    bool pressing_use;
    float pressing_use_weight;

    void SetCameraTarget()
    {
        cameraControl.target = normal_cam_position;
    }

    

    private void Awake()
    {

        pressing_use_weight = 0f;
        look_at_weight = 0f;

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            playerCustom.Change_IsSword(!playerCustom.isSword);
        }

        pressing_use = Input.GetKey(KeyCode.E);


        bool mouse0 = Input.GetMouseButtonDown(0);
        bool attackState = _anim.GetCurrentAnimatorStateInfo(0).IsName("Male Attack 1") || _anim.GetCurrentAnimatorStateInfo(0).IsName("Female Sword Attack 1");

        if (mouse0 && !attackState && playerCustom.isSword)
        {
            _anim.SetTrigger("Attack");
            //Debug.Log("Attack");
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

        CheckInteres();

    }

    void CheckInteres()
    {

        Vector3 my_position;

        if (_ismale)
        {
            my_position = _male_head.transform.position;
        }
        else
        {
            my_position = _female_head.transform.position;
        }
        colliders = Physics.OverlapSphere(my_position, 2f);

        GameObject go = null;
        float min_distance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            IInteres interes = collider.gameObject.GetComponent<IInteres>();
            if (interes != null)
            {
                float distance = (collider.transform.position - my_position).sqrMagnitude;
                if (distance < min_distance)
                {
                    min_distance = distance;
                    go = collider.gameObject;
                }
            }
        }

        go_interes = go;

    }

    public void AnimatorIK(Animator animator, int layerIndex)
    {
        if (go_interes != null)
        {
            last_interes = go_interes;
            look_at_weight = Mathf.Lerp(look_at_weight, 1f, Time.deltaTime);
            animator.SetLookAtPosition(go_interes.transform.position);
            animator.SetLookAtWeight(look_at_weight);
        }
        else
        {
            look_at_weight = Mathf.Lerp(look_at_weight, 0f, Time.deltaTime);
            if (last_interes != null)
            {
                animator.SetLookAtPosition(last_interes.transform.position);
            }
            animator.SetLookAtWeight(look_at_weight);
        }
    }

    public void AnimatorIK_Hands(Animator animator, GameObject left_hand, GameObject right_hand, int layerIndex)
    {
        //pressing_use = true;
        if (go_interes != null && !playerCustom.isSword)
        {
            if (pressing_use)
            {
                pressing_use_weight = Mathf.Lerp(pressing_use_weight, 1f, Time.deltaTime);
            }
            else
            {
                pressing_use_weight = Mathf.Lerp(pressing_use_weight, 0f, Time.deltaTime);
            }
        }
        else
        {
            pressing_use_weight = Mathf.Lerp(pressing_use_weight, 0f, Time.deltaTime);
        }

        if (last_interes != null)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, last_interes.transform.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, last_interes.transform.position);
            //animator.SetIKRotation(AvatarIKGoal.RightHand, last_interes.transform.rotation);
        }
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, pressing_use_weight);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, pressing_use_weight);
        //animator.SetIKRotationWeight(AvatarIKGoal.RightHand, pressing_use_weight);
    }

    private void OnDestroy()
    {
        Global.SetCursor(true);
    }
}
