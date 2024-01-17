using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum State
{
    Follow,
    Skill1,
    Skill2,
    stop
}
public class BossGhost : Monster
{
    State state;
    Rigidbody2D rb;
    public GameObject Ghost;
    Vector2 targetPos;
    private bool attackfalse;
    protected override void Start()
    {
        attackfalse = false;
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        state = State.Follow;
        StartCoroutine(pattern());
    }

    void FixedUpdate()
    {
        if (state == State.Follow || state==State.Skill2)
            rb.velocity = (Player_tf.position - transform.position).normalized * Speed;
        else if(state == State.Skill1)
        {
            rb.velocity = targetPos.normalized * Speed * 9f;
        }
        else if (state == State.stop) {
            rb.velocity =  new Vector2(0,0);
        }
    }
    public override void TakeDamage(int damage_)
    {
        base.TakeDamage(damage_);
        StageManager.Instance.StateUI.setBossHP(curHp);
    }
    IEnumerator pattern()
    {
        int patt;
        while (true)
        {
            yield return new WaitForSeconds(4f);
            patt = Random.Range(0, 2);
            switch (patt)
            {
                case 0:
                    state = State.stop;
                    yield return new WaitForSeconds(1f);
                    targetPos = (StageManager.Instance.Player.transform.position);
                    Vector2 mypos = new Vector2(transform.position.x, transform.position.y);
                    targetPos = (targetPos - mypos).normalized;
                    state = State.Skill1;
                    yield return new WaitForSeconds(1f);
                    break;
                case 1:
                    state = State.stop;
                    yield return new WaitForSeconds(1.5f);
                    state = State.Skill2;
                    Summon();
                    break;
            }
            state = State.Follow;
        }
    }
    public override void Dead()
    {
        if (curHp <= 0)
        {
            Destroy(gameObject);
            StageManager.Instance.Win();
        }
    }
    private void Summon()
    {
        for(int i=0;i<10;i++)
        {
            GameObject g_ = Instantiate(Ghost);
            g_.transform.position = transform.position;
        }
    }
}
