using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianMove : ThrowSkill
{
    // Start is called before the first frame update

    

    protected override void Start()
    {
        base.Start();

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("SkillWall"))
        {
            
          
        }
    }
}
