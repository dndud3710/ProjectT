using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    Follow,
    Skill1,
    Skill2
}
public class BossGhost : Monster
{
    State state;
    Rigidbody2D rb;

    protected override void Start()
    {
        
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        state = State.Follow;
    }

    void FixedUpdate()
    {
        if (state == State.Follow)
            rb.velocity = (Player_tf.position - transform.position).normalized * Speed;
        else if(state == State.Skill1)
        {
            Vector2 targetPos = (StageManager.Instance.Player.transform.position - transform.position).normalized * Speed *2f;

        }
    }
    IEnumerator pattern()
    {
        int patt;
        while (true)
        {
            yield return new WaitForSeconds(5f);
            patt = Random.Range(0, 3);
            switch (patt)
            {
                case 0:
                    yield return new WaitForSeconds(1f);
                    state = State.Skill1;
                    yield return new WaitForSeconds(2f);
                    break;
                case 1:
                    yield return new WaitForSeconds(1f);
                    state = State.Skill2;
                    break;
            }
            state = State.Follow;
        }
    }
}
