using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector2 Dir;
    [Header("�÷��̾� ����")]
    public int Level;
    public int[] maxExp;
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

    //������ ��ų���� �ڽĿ�����Ʈ��, haveSkills�� ����
    //haveskills�� ���鼭 List�� �巷���µ� �нú�� ���ڸ��� ȿ���� �ߵ��ǰ�,
    public Transform HaveSkill;
    private List<IngameSkill> skillList;

    private void Awake()
    {

    }
    private void Start()
    {
        Init();
        //Skills.Add(DataManager.Instance.getActiveSkillObject(EActiveSkillType.SwordSlash));
        //GameObject g = Instantiate(Skills[0]);
        //g.GetComponent<ActiveSkills>().Use();��


        //�÷��̾ � ���⸦ �����ߴ����� ���� �ʱ� ��ų�� ����
        //�ϴ� gamemanager���� weapon�� ���� List�� ���� ����, ���⼭ Ư�� �������� ��� Ư����ų�� �����ϰ� �� ���ΰ�
        if (GameManager.Instance.equips[0] != null)
        {
            foreach(EquipItem eq_ in GameManager.Instance.EquipWeaponsList) 
            { 
                if (GameManager.Instance.equips[0] == eq_)
                {
                    GetSkill(DataManager.Instance.getActiveSkillObject(eq_.gameObject.GetComponent<WeaponSkillType>().getType()));
                }    

            }

        }


    }
    void Init()
    {
        curExp = 0;
        maxExp = new int[6] { 100, 200, 300, 400, 500, 600 };
        HpBar.maxValue = maxHP;
        HpBar.value = maxHP;
        currentHP = maxHP;
        //����ġ�� �ƽ� �ʱ�ȭ
        StageManager.Instance.StateUI.setExpBar(true);
        skillList = new List<IngameSkill>();
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

    #region EXP
    public void getEXP(int gexp)
    {
        curExp += gexp;
        StageManager.Instance.StateUI.setExpBar(false);
        LevelUp();
    }
    void LevelUp()
    {
        if (curExp >= maxExp[Level-1])
        {
            curExp = maxExp[Level - 1] - curExp;
            Level++;
            StageManager.Instance.StateUI.setExpBar(true);
            StageManager.Instance.StateUI.setLevelText(Level);
            StageManager.Instance.skillmanager.OnLevelUpSelectSkills();
        }
    }
    #endregion

    public void GetSkill(GameObject g_)
    {
        IngameSkill igs_ = g_.GetComponent<IngameSkill>();
        foreach (IngameSkill i in skillList)
        {
            if(igs_.ESkillName == i.ESkillName)
            {
                //�̹� �������
                print("��ų������!");
                i.SkillLevelUp();
                return;
            }
        }
        print("���ο� ��ų ȹ��");
        GameObject gg_ = Instantiate(g_, HaveSkill.transform);
        igs_ = gg_.GetComponent<IngameSkill>();

        skillList.Add(igs_);
        igs_.Use();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<InGameItem>(out InGameItem igi_))
        {
            //INgameItem�� ���� �δ� ����, �̷��� �������϶� �Ծ����� Use�ѹ����Ϸ���
            igi_.Use();
        }
    }
}
