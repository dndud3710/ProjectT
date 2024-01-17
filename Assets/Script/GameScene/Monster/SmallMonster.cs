using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonster : Monster
{
    enum movepatter
    {
        slow,
        fast
    }
    movepatter mp;
    bool attackOnOff;
    Coroutine c,d;
    Rigidbody2D rb;
    SpriteRenderer sp;
    new void Start()
    {
        base.Start();
        c=StartCoroutine(AttackCoroutine());
        rb = GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponentInChildren<SpriteRenderer>();
        mp = movepatter.slow;
    }


    private void OnEnable()
    {
        c=StartCoroutine(AttackCoroutine());
        d = StartCoroutine(movePattern());
    }
    private void OnDisable()
    {
        curHp = maxHp;
        StopCoroutine(c);
        StopCoroutine(d);
        if (sp != null)
        {
            Color d = sp.color;
            d.a = 255;
            sp.color = d;
        }
        
    }
    void FixedUpdate()
    {
        if (mp==movepatter.slow)
        {
            rb.velocity = (Player_tf.position - transform.position).normalized * (Speed*0.6f);
        }
        else if(mp==movepatter.fast)
        {
            rb.velocity = (Player_tf.position - transform.position).normalized * Speed;
        }
    }
    IEnumerator movePattern()
    {
        while (true)
        {
            mp = movepatter.fast;
            yield return new WaitForSeconds(2f);
            mp= movepatter.slow;
            yield return new WaitForSeconds(2f);
        }
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
    public override void TakeDamage(int damage_)
    {
        base.TakeDamage(damage_);
        transform.Translate((transform.position - StageManager.Instance.playerScript.transform.position).normalized * 0.1f);
      
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
