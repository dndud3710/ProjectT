using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSkill : MonoBehaviour
{

    private PointType pt;
    private int damage;
    [Tooltip("오브젝트 관통 횟수 (-10일경우 무한)")]
    private int Du;
    [Tooltip("오브젝트 파괴 시간")]
    private float ClearPrefabsTime;
    [Tooltip("오브젝트가 날라가는 속도")]
    protected float Speed;

    [Tooltip("오디오 설정")]
    public AudioManager.SFXList StartSfx;
    public AudioManager.SFXList sfxlist;

    public enum option
    {
        None = 0,
        monsterReflect =1<<0 //몬스터 맞고 튕기는거
    }

    [Tooltip("실행할 파티클 선택")]
    public ParticleSystem Startparticle;
    public ParticleSystem AttackParticle;
    public GameObject AttackParticleobject;
    public option option_;

    //UI전용 트랜스폼
    RectTransform rt;
    protected Vector2 forwardgo;
    AudioClip au_;
    CircleCollider2D cr;

    protected virtual void Start()
    {
        Destroy(gameObject, ClearPrefabsTime);
        TryGetComponent<CircleCollider2D>(out cr);
        if (Startparticle != null)
        {
            Startparticle.Play();
        }
        if (AttackParticle != null)
        {
            GameObject g = Instantiate(AttackParticleobject);
            g.transform.position = transform.position;
            ParticleSystem part = g.GetComponent<ParticleSystem>();
            part.Play();
            Destroy(g, 0.5f);
        }
       
        if (PointType.UI == pt)
        {
            rt = GetComponent<RectTransform>();
            //시작 방향전환 나중에 추가
            int d = UnityEngine.Random.Range(0, 2);
            switch (d)
            {
                case 0:
                    forwardgo = new Vector2(UnityEngine.Random.Range(0.1f, 1f), UnityEngine.Random.Range(0.1f, 1f)) * Speed;
                    break;
                case 1:
                    forwardgo = new Vector2(UnityEngine.Random.Range(-1f, -0.1f), UnityEngine.Random.Range(-1f, 0.1f)) * Speed;
                    break;
            }
        }
        if(StartSfx != AudioManager.SFXList.메뉴선택)
        AudioManager.Instance.AudioPlaying(AudioManager.Instance.SFXSound[(int)StartSfx]);
        if (sfxlist != AudioManager.SFXList.메뉴선택) au_ = AudioManager.Instance.SFXSound[(int)sfxlist];
    }
    protected virtual void Move()
    {
       if(pt==PointType.World)
            transform.Translate(Vector2.up * Time.deltaTime * Speed );
        else if (pt == PointType.UI)
            rt.Translate(forwardgo * Time.deltaTime * Speed,Space.World);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        if (Du <= 0 && Du!=-10)
            Destroy(gameObject);
        if (pt == PointType.UI)
            transform.Rotate(0, 0, 3f);
    }
    
    public void DurationZero()
    {
        Du = -1;
    }
    public void setThrowSkills(int damage,int dur, float t , float speed,PointType pr)
    {
        this. damage= damage;
        this.Du = dur;
        ClearPrefabsTime = t;
        this.Speed = speed;
        pt = pr;
    }
    /// <summary>
    /// 공격 판정
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.TryGetComponent<Monster>(out Monster monster);
            if (Du != -10)
            {
                Du--;
                monster.TakeDamage(damage);
            }
            else
            {

                monster.TakeDamage(damage);
            }
            AudioManager.Instance.AudioPlaying(au_);
        }
        else if (collision.CompareTag("ItemChest"))
        {
            collision.TryGetComponent<ItemChest>(out ItemChest chest);
            AudioManager.Instance.AudioPlaying(au_);
            chest.detroy();
        }
        if (pt == PointType.UI)
        {
            if (collision.CompareTag("SkillWall"))
            {
                ColliderDistance2D cd = cr.Distance(collision);
                forwardgo = Vector2.Reflect(forwardgo.normalized, cd.normal);
            }
        }
        if(option_ == option.monsterReflect && collision.CompareTag("Monster"))
        {
            ColliderDistance2D cd = cr.Distance(collision);

            forwardgo = Vector2.Reflect(forwardgo.normalized, cd.normal);
        }

    }
}
