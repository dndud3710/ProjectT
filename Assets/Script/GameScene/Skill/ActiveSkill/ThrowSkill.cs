using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSkill : MonoBehaviour
{
    private int damage;
    [Tooltip("������Ʈ ���� Ƚ�� (-10�ϰ�� ����)")]
    private int Du;
    [Tooltip("������Ʈ �ı� �ð�")]
    private float ClearPrefabsTime;
    [Tooltip("������Ʈ�� ���󰡴� �ӵ�")]
    protected float Speed;


    protected virtual void Start()
    {
        Destroy(gameObject, ClearPrefabsTime);
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed );
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        if (Du <= 0 && Du!=-10)
            Destroy(gameObject);
    }
    public void setThrowSkills(int damage,int dur, float t , float speed)
    {
        this.damage = damage;
        this.Du = dur;
        ClearPrefabsTime = t;
        this.Speed = speed;
    }
    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (Du != -10)
            {
                Du--;
                monster.TakeDamage(damage);
            }
            else
            {

                monster.TakeDamage(damage);
            }
        }
    }
}
