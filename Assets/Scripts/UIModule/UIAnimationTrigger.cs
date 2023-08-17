using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIAnimationTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<DOTweenAnimation>().DORestart();
    }
}
