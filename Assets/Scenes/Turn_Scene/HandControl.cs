using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{

    [SerializeField] GameObject ray_origin;
    [SerializeField] GameObject ray_point1;
    [SerializeField] GameObject ray_point2;
    [SerializeField] GameObject ray_point3;

    Vector3 hand_target;
    Vector3 last_hand_target;

    float weight_ik;

    Animator _anim;

    private void Awake()
    {
        weight_ik = 0;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 direction;

        Vector3 point = Vector3.zero;
        float distance = float.MaxValue;

        direction = ray_point1.transform.position - ray_origin.transform.position;
        if (Physics.Raycast(ray_origin.transform.position, direction, out hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                if (hit.distance < distance)
                {
                    distance = hit.distance;
                    point = hit.point;
                }
            }
        }
        direction = ray_point2.transform.position - ray_origin.transform.position;
        if (Physics.Raycast(ray_origin.transform.position, direction, out hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                if (hit.distance < distance)
                {
                    distance = hit.distance;
                    point = hit.point;
                }
            }
        }
        direction = ray_point3.transform.position - ray_origin.transform.position;
        if (Physics.Raycast(ray_origin.transform.position, direction, out hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                if (hit.distance < distance)
                {
                    distance = hit.distance;
                    point = hit.point;
                }
            }
        }

        hand_target = point;
        if (hand_target != Vector3.zero)
        {
            if (last_hand_target == Vector3.zero)
            {
                last_hand_target = hand_target;
            }
            else
            {
                last_hand_target = Vector3.Lerp(last_hand_target,hand_target, 10f * Time.deltaTime);
            }
            
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (hand_target != Vector3.zero)
        {
            weight_ik = Mathf.Lerp(weight_ik, 1f, Time.deltaTime);
        }
        else
        {
            weight_ik = Mathf.Lerp(weight_ik, 0f, 5f * Time.deltaTime);
        }

        if (weight_ik <= 0.1f)
        {
            last_hand_target = Vector3.zero;
        }

        if (last_hand_target != Vector3.zero)
        {
            _anim.SetIKPosition(AvatarIKGoal.LeftHand, last_hand_target);
        }
        
        _anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.LookRotation(Vector3.up));
        _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight_ik);
        _anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight_ik);

    }

}
