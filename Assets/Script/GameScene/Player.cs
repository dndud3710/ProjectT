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
    [Header("플레이어 정보")]
    public int Level;
    public int[] maxExp;
    public int curExp;
    public int maxHP;
    public int currentHP;
    public float speed;
    public int damage;

    [Header("컴포넌트")]
    public Rigidbody2D rb;
    public SpriteRenderer spr;
    public Slider HpBar;
    public Animator animator;
    public Transform ShotPointAngle;
    public Transform ShotPoint;
    public CircleCollider2D getItemCircle;
    public ParticleSystem healparticle;

    //선택한 스킬들은 자식오브젝트인, haveSkills에 들어간다
    //haveskills에 들어가면서 List에 드렁가는데 패시브는 얻자마자 효과가 발동되고,
    public Transform HaveSkill;
    List<IngameSkill> skillList;

    private void Awake()
    {

    }
    private void Start()
    {
        Init();
        //Skills.Add(DataManager.Instance.getActiveSkillObject(EActiveSkillType.SwordSlash));
        //GameObject g = Instantiate(Skills[0]);
        //g.GetComponent<ActiveSkills>().Use();ㄴ

       


        //플레이어가 어떤 무기를 장착했는지에 따라 초기 스킬을 설정
        //일단 gamemanager에서 weapon만 따로 List에 담은 상태, 여기서 특정 웨폰들이 어떻게 특정스킬을 지목하게 할 것인가
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
    //레벨업 시에 SkillManage스크립트에서 랜덤 스킬을 3개생성하는데, 여기서 캐릭터가 가지고 있는 스킬레벨이 5이상인경우가 있으면 true
    public bool getSkillLevel(EActiveSkillType es_)
    {
        GameObject g_ = DataManager.Instance.getActiveSkillObject(es_);
        string skillname_ =  g_.GetComponent<IngameSkill>().ESkillName;
        foreach (IngameSkill i in skillList)
        {
            if (skillname_.Equals(i.ESkillName))
            {
                if(i.ESkillLevel>=5)
                    return true;
            }
        }
        return false;
    }
    void Init()
    {
        int[] i_ = GameManager.Instance.getPlayerStat();
        damage = i_[0];
        maxHP = i_[1];

        curExp = 0;
        maxExp = new int[11] { 30,40,55,70,100,110,120,130,200,250,500 };
        HpBar.maxValue = maxHP;
        HpBar.value = maxHP;
        currentHP = maxHP;
        //경험치바 맥스 초기화
        StageManager.Instance.StateUI.setExpBar(true);
        skillList = new List<IngameSkill>();
    }
    private void FixedUpdate()
    {
        //TODO: 나중에 항상 실행되지 않게 막아야 함
        rb.velocity = Dir * speed;
        print("dpd");
    }
    private void Update()
    {
        ShotPointMove();
    }

    //.움직임
    public void Move(Vector2 moveVec)
    {
        Dir = moveVec;
        if (Dir.x < 0)
            spr.flipX = true;
        else if (Dir.x > 0)
            spr.flipX = false;
    }
    public Vector2 getVector()
    {
        return Dir;
    }

    public void ShotPointMove()
    {
        if (StageManager.Instance.JoyStick.activeSelf)
        {
            ShotPoint.gameObject.SetActive(true);
            // atan2를 사용하여 라디안으로 각도를 계산하고, 도(degree)로 변환
            float Shotangle = (Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg) - 90;

            // Z축을 중심으로 회전 적용
            ShotPointAngle.rotation = Quaternion.Euler(new Vector3(0, 0, Shotangle));
        }
        else
        {
            //TODO:이것은 계속 실행되므로 나중에 처리해야함
            ShotPoint.gameObject.SetActive(false);
        }
    }

    //activeskill에서 프리팹들이 캐릭터가 보고있는 방향으로 회전시키기 위한 get함수
    public Transform getShotPointAngle()
    {
        return ShotPointAngle;
    }


    #region 전투
    /// <summary>
    /// 전투 관련
    /// </summary>
    public void TakeDamage(int Damage_)
    {
        currentHP -= Damage_;
        animator.SetTrigger("Hit");
        HpBarUpdate();
        Dead();
    }
    public void TakeHeal(float heal_)
    {
        currentHP += (int)heal_;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        HpBarUpdate();
        
        healparticle.Play();
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
        if (currentHP <= 0)
        {
            currentHP = 0;
            StageManager.Instance.Lose();
        }
    }

    //주변 AllCast
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
        if (Attackmonster != null)
        {
            Vector2 vec = Attackmonster.transform.position - transform.position;
            float Shotangle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 90;
            return Quaternion.Euler(new Vector3(0, 0, Shotangle));
        }
        Quaternion q = Quaternion.identity;
        return q;
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
                //이미 있을경우
                print("스킬레벨업!");
                i.SkillLevelUp();
                return;
            }
        }
        GameObject gg_ = Instantiate(g_, HaveSkill.transform);
        igs_ = gg_.GetComponent<IngameSkill>();

        skillList.Add(igs_);
        igs_.Use();
    }
    
}
