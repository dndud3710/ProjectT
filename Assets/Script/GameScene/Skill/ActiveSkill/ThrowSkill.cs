using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSkill : MonoBehaviour
{
    
    private PointType pt;
    private int damage;
    [Tooltip("������Ʈ ���� Ƚ�� (-10�ϰ�� ����)")]
    private int Du;
    [Tooltip("������Ʈ �ı� �ð�")]
    private float ClearPrefabsTime;
    [Tooltip("������Ʈ�� ���󰡴� �ӵ�")]
    protected float Speed;


    //UI���� Ʈ������
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
            //���� ������ȯ ���߿� �߰�
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
    /// ���� ����
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
