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


    //UI전용 트랜스폼
    RectTransform rt;
    protected Vector2 forwardgo;
    AudioClip au_;
    protected void setAU_(AudioManager.SFXList sfxlist)
    {
        au_ = AudioManager.Instance.SFXSound[(int)sfxlist];
    }
  
    protected virtual void Start()
    {
        Destroy(gameObject, ClearPrefabsTime);
        if (PointType.UI == pt)
        {
            rt = GetComponent<RectTransform>();
            //시작 방향전환 나중에 추가
            int d = Random.Range(0, 2);
            switch (d)
            {
                case 0:
                    forwardgo = new Vector2(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f)) * Speed;
                    break;
                case 1:
                    forwardgo = new Vector2(Random.Range(-1f, -0.1f), Random.Range(-1f, 0.1f)) * Speed;
                    break;
            }
        }
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
        else if(collision.CompareTag("ItemChest"))
        {
            collision.TryGetComponent<ItemChest>(out ItemChest chest);
            AudioManager.Instance.AudioPlaying(au_);
            chest.detroy();
        }

    }
}
