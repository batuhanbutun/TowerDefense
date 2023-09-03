using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedEnemyAnim : MonoBehaviour
{
    public Animator myAnim;
    private static readonly int İdle = Animator.StringToHash("idle");

    private void Start()
    {
        myAnim.SetTrigger(İdle);
    }
}
