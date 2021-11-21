using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    Animator _anim;
    [SerializeField] Player player;
    [SerializeField] GameObject left_hand;
    [SerializeField] GameObject right_hand;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        player.AnimatorIK(_anim, layerIndex);
        player.AnimatorIK_Hands(_anim, left_hand, right_hand, layerIndex);
    }

}
