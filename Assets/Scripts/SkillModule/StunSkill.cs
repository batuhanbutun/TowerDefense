using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSkill : Skill
{
    public float stunDuration;

    private void Update()
    {
        SkillCooldown();
    }

    public override void CastSkill(Vector3 position)
    {
        transform.position = position;
        skillParticle.Play();
        DetectEnemy();
        foreach (var enemy in detectedEnemies)
        {
            enemy.GetComponent<EnemyMovement>().GetStun(stunDuration); 
        }
        isSkillReady = false;
        timeDuration = 0f;
    }
}
