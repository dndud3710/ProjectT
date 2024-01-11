using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianMove : ThrowSkill
{
    // Start is called before the first frame update
    CircleCollider2D cr;
    Vector2 forwardgo;
    protected override void Start()
    {
        base.Start();
        cr=GetComponent<CircleCollider2D>();
        forwardgo =  new Vector2(1,1) * Speed;
    }
    protected override void Move()
    {
        transform.Rotate(new Vector3(0, 0, 4f));
        transform.Translate(forwardgo * Time.deltaTime * Speed , Space.World);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        print(" F");
        if (collision.CompareTag("SkillWall"))
        {
            ColliderDistance2D cd = cr.Distance(collision);

            forwardgo = Vector2.Reflect(forwardgo.normalized, cd.normal);
            
        }
    }
}
