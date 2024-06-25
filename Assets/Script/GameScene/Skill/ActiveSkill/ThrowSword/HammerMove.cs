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
   
    }
    
}
