using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonster : Monster
{
    bool attackOnOff;
    Coroutine c;
    Rigidbody2D rb;
    SpriteRenderer sp;
    new void Start()
    {
        base.Start();
        c=StartCoroutine(AttackCoroutine());
        rb = GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponentInChildren<SpriteRenderer>();
        
    }


    private void OnEnable()
    {
        c=StartCoroutine(AttackCoroutine());
        
    }
    private void OnDisable()
    {
        curHp = maxHp;
        StopCoroutine(c);
        if (sp != null)
        {
            Color d = sp.color;
            d.a = 255;
            sp.color = d;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = (Player_tf.position - transform.position).normalized * Speed;
    }
    protected IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => attackOnOff);
            player_sc.TakeDamage(Damage);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            attackOnOff = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackOnOff = false;
        }
    }

}
