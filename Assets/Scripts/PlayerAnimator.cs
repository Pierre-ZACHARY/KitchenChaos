using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Player player;
    private string IS_WALKING = "IsWalking";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
