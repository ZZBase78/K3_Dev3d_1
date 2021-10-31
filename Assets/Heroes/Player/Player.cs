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

    private void Awake()
    {
        playerCustom = GetComponent<PlayerCustom>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

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

        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(x, 0, z).normalized;

        if (direction == Vector3.zero) _anim.SetBool("Walk", false); else _anim.SetBool("Walk", true);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _anim.SetBool("Sprint", true);
            transform.Translate(direction * speed * 2 * Time.deltaTime);
        }
        else
        {
            _anim.SetBool("Sprint", false);
            transform.Translate(direction * speed * Time.deltaTime);
        }

        

        float mx = Input.GetAxis("Mouse X");

        Vector3 euler = new Vector3(0, mx * mouse_speed * Time.deltaTime, 0);

        transform.Rotate(euler);
    }
}
