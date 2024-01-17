using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EXP : InGameItem
{
    //EXP아이템은 색깔별로 level이 다르며 받는 경험치도 다름
    public ParticleSystem starts; //반짝반짝
    public int Level;
    private bool stars;
    private bool stars_;
    Vector2 vec;
    Vector2 Mypos;
    float xx=0, yy=0;
    float sp=1f;
    private void Start()
    {
        starts.Play();
        Mypos = transform.position;
    }
    private void FixedUpdate()
    {

        if (stars)
        {
            float xxx = Mathf.SmoothDamp(transform.position.x, vec.x, ref xx, 0.4f);
            float yyy = Mathf.SmoothDamp(transform.position.y, vec.y, ref yy, 0.4f);
            transform.position = new Vector2(xxx, yyy);
        }
        
        if (Mathf.Abs(vec.x - transform.position.x) <= 0.11f)
        {
            stars_ = true;
            stars = false;
        }
    }
    protected override void Update()
    {
        base.Update();
        if (stars_)
        {
            sp += 1f;
            if (sp > 200) Destroy(gameObject);
            transform.Translate((StageManager.Instance.Player.transform.position - transform.position) * sp * Time.deltaTime);
        }
    }
    public override void Use()
    {
        if (!Magnet)
        {
            vec = transform.position - StageManager.Instance.Player.transform.position;
            vec = vec.normalized;
            vec *= 1.4f;
            vec += Mypos;
            stars = true;
        }
    }
    private void OnDestroy()
    {
        StageManager.Instance.playerScript.getEXP(StageManager.Instance.getItemExp(Level));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(stars_ || Magnet)
            {
                print("사라짐!");
                Destroy(gameObject);
            }
        }
    }

}
