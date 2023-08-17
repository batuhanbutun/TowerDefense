using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : Skill
{
    public int skillDamage;
    
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
           enemy.GetComponent<EnemyHealth>().GetDamage(skillDamage); 
        }
        isSkillReady = false;
        timeDuration = 0f;
    }
}
