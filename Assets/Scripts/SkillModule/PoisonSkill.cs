using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSkill : Skill
{
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
            enemy.GetComponent<EnemyHealth>().PoisonDamage(5);
        }
        
        isSkillReady = false;
        timeDuration = 0f;
    }
}
