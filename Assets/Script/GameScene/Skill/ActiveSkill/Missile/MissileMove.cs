using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : ThrowSkill
{
    public ParticleSystem p;
    public GameObject bombParticle;

    protected override void Start()
    {
        base.Start();
        p.Play();
    }
    private void OnDisable()
    {
        GameObject g = Instantiate(bombParticle);
        g.transform.position = transform.position;
        ParticleSystem part = g.GetComponent<ParticleSystem>();
        part.Play();
        Destroy(g, 0.5f);
    }
    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            DurationZero();
        }
    }
}
