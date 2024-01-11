using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector2 Dir;
    [Header("�÷��̾� ����")]
    public int Level;
    public int maxExp;
    public int curExp;
    public int maxHP;
    public int currentHP;
    public float speed;
    public int damage;

    [Header("������Ʈ")]
    public Rigidbody2D rb;
    public SpriteRenderer spr;
    public Slider HpBar;
    public Animator animator;
    public Transform ShotPointAngle;
    public Transform ShotPoint;

    public List<GameObject> Skills;

    public BoxCollider2D col;
    private void Awake()
    {

    }
    private void Start()
    {
        Init();
        Skills.Add(DataManager.Instance.getActiveSkillObject(EActiveSkillType.SwordSlash));
        GameObject g = Instantiate(Skills[0]);
        g.GetComponent<ActiveSkills>().Use();
        Invoke("starttt", 0.8f);
        float verticalSize = Camera.main.orthographicSize * 2.0f;
        float horizontalSize = verticalSize * Camera.main.aspect;
        col.size = new Vector2(horizontalSize, verticalSize);
    }
    void starttt()
    {
        Skills.Add(DataManager.Instance.getActiveSkillObject(EActiveSkillType.BubbleBomb));
        GameObject g = Instantiate(Skills[1]);
        g.GetComponent<ActiveSkills>().Use();
    }
    void Init()
    {
        HpBar.maxValue = maxHP;
        HpBar.value = maxHP;
        currentHP = maxHP;
    }
    private void FixedUpdate()
    {
        //TODO: ���߿� �׻� ������� �ʰ� ���ƾ� ��
        rb.velocity = Dir * speed;
    }
    private void Update()
    {
        ShotPointMove();
    }

    //.������
    public void Move(Vector2 moveVec)
    {
        Dir = moveVec;
        if (Dir.x < 0)
            spr.flipX = true;
        else if (Dir.x > 0)
            spr.flipX = false;
    }

    public void ShotPointMove()
    {
        if (StageManager.Instance.JoyStick.activeSelf)
        {
            ShotPoint.gameObject.SetActive(true);
            // atan2�� ����Ͽ� �������� ������ ����ϰ�, ��(degree)�� ��ȯ
            float Shotangle = (Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg) - 90;

            // Z���� �߽����� ȸ�� ����
            ShotPointAngle.rotation = Quaternion.Euler(new Vector3(0, 0, Shotangle));
        }
        else
        {
            //TODO:�̰��� ��� ����ǹǷ� ���߿� ó���ؾ���
            ShotPoint.gameObject.SetActive(false);
        }
    }

    //activeskill���� �����յ��� ĳ���Ͱ� �����ִ� �������� ȸ����Ű�� ���� get�Լ�
    public Transform getShotPointAngle()
    {
        return ShotPointAngle;
    }


    #region ����
    /// <summary>
    /// ���� ����
    /// </summary>
    public void TakeDamage(int Damage_)
    {
        currentHP -= Damage_;
        animator.SetTrigger("Hit");
        HpBarUpdate();
        Dead();
    }

    public int getDamage()
    {
        return damage;
    }
    void HpBarUpdate()
    {
        HpBar.value = currentHP;
    }

    private void Dead()
    {
        if (currentHP < 0)
        {
            currentHP = 0;
            //�������� ���� UI
        }
    }

    //�ֺ� AllCast
    public Quaternion CastAround()
    {
        Collider2D[] monsters=  Physics2D.OverlapCircleAll(transform.position, 5f,LayerMask.GetMask("Monster"));
        float distance=100f;
        float dist;
        GameObject Attackmonster = null;
       
        foreach(Collider2D monster in monsters)
        {
            
            dist = Mathf.Abs(Vector2.Distance(transform.position , monster.transform.position));
            if(dist<distance)
            {
                distance = dist;
                Attackmonster = monster.gameObject;
            }
        }
        Vector2 vec = Attackmonster.transform.position - transform.position;
        float Shotangle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 90;
        return Quaternion.Euler(new Vector3(0, 0, Shotangle));
    }

    #endregion
}
