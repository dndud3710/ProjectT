using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMove : ThrowSkill
{
    
    
    CircleCollider2D cr;
    protected override void Start()
    {
        base.Start();
        cr = GetComponent<CircleCollider2D>();
        AudioManager.Instance.AudioPlaying(AudioManager.Instance.SFXSound[(int)AudioManager.SFXList.¸ÁÄ¡´øÁü]);
        setAU_(AudioManager.SFXList.¸ÁÄ¡¸ÂÀ½);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("SkillWall"))
        {
            ColliderDistance2D cd = cr.Distance(collision);

            forwardgo = Vector2.Reflect(forwardgo.normalized, cd.normal);
        }
        else if (collision.CompareTag("Monster"))
        {
            ColliderDistance2D cd = cr.Distance(collision);

            forwardgo = Vector2.Reflect(forwardgo.normalized, cd.normal);
        }
    }
}
