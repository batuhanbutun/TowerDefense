using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedEnemyAnim : MonoBehaviour
{
    public Animator myAnim;

    private void Start()
    {
        myAnim.SetTrigger("idle");
    }
}
