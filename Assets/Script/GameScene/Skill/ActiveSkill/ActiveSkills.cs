using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkills : MonoBehaviour , IngameSkill
{


    // 1. 여러개 생성될 스킬 오브젝트를 담을 List
    private List<GameObject> spawnAttackObjects;
    // 2. ㅂ
    [Tooltip("스킬 오브젝트 소환프리팹")]
    public GameObject SpawnPrefab;

    [Flags]
    public enum AttackOption
    {
        None = 0,
        LockOnAttack =1 <<0, //따라가는 공격인지 아닌지
        LevelupCountUp = 1 <<1, //레벨업시 개수가 증가하는 것인지
        LevelupSizeup = 1 <<2, //레벨업시 크기가 커지는것인지
        otherwayAttack = 1<<3,//반대방향 공격인지 아닌지
    }
    public AttackOption attackOption;

    //RectTransform기준은 UI, Transform기준으로 움직이는건 World
    [Tooltip("UI기준 좌표인지 World기준 좌표인지")]
    public PointType pointtype;
    [Tooltip("스킬이 몇개 나가는지 개수")]
    public int count = 1;
    [Tooltip("스킬 쿨타임")]
    public float coolDown = 4f;
    [Tooltip("오브젝트 관통 횟수 (-10일경우 무한)")]
    public int Duration = 20;
    [Tooltip("오브젝트 파괴 시간")]
    public float ClearPrefabsTime = 20f;
    [Tooltip("오브젝트가 날라가는 속도")]
    public float Speed = 2f;
    [Tooltip("스킬이 소환되는 딜레이")]
    public float skillAtackDelay = 2f;
    private int SkillLevel;
    //만약 UI면 World면 canvas skill을 부모로할건지, 플레이어의 skill을 부모로 할건지
    protected Transform ParentTransform;

    Transform PlayerTF;
    Transform PlayerRot;
    //자동화 실패해서 그냥 만든 것
    public Sprite SkillImage;
    public string SkillName;
    Vector3 LevelupScale;

    [TextArea(10, 5)]
    public string Description;
    public Sprite ESkillImage { get {  return SkillImage; } }

    public string ESkillName { get { return SkillName; } }

    public string EDiscription { get { return Description; } }

    public int ESkillLevel { get { return SkillLevel; } }

    private void Start()
    {
        SkillLevel = 1;
        PlayerTF = StageManager.Instance.Player.transform;
        PlayerRot = StageManager.Instance.playerScript.getShotPointAngle();
        if(pointtype == PointType.UI)
        {
            ParentTransform = StageManager.Instance.UISkillParent;
        }
        else if(pointtype ==PointType.World)
        {
            ParentTransform = StageManager.Instance.WorldSkillParent ;
        }
        LevelupScale = SpawnPrefab.transform.localScale;
        if (pointtype == PointType.UI && attackOption == AttackOption.LevelupCountUp) spawnAttackObjects = new List<GameObject>();

    }
    public void Use() { StartCoroutine(AttackStart()); }
    public void SkillLevelUp() { SkillLevel++;
        if ((attackOption & AttackOption.LevelupCountUp) != 0)
        {
            count++;
            Debug.Log($"count 증가@@@@@@@@@   : {count}");
        }
        if ((attackOption & AttackOption.LevelupSizeup) != 0)
        {
            LevelupScale += new Vector3(0.2f, 0.2f, 0);
        }
    }
    
    protected Transform getPlayerTF()
    {
        return PlayerTF;
    }
    protected Transform getPlayerRot()
    {
        return PlayerRot;
    }
    private Quaternion SearchMonster()
    {
        return StageManager.Instance.playerScript.CastAround();
    }

    IEnumerator AttackStart()
    {
        int c = count;

        Quaternion currentRotation = Quaternion.identity;
        int turn = 1;
        while (true)
        {
            //스킬 재사용 시간

            yield return new WaitForSeconds(coolDown);
            c = count;
            if ((attackOption & AttackOption.otherwayAttack) != 0) turn = 1;

            if (getPlayerRot() != null)
            {
                currentRotation = getPlayerRot().rotation;
            }
            currentRotation = getPlayerRot().rotation;
            if (pointtype != PointType.UI) {
                while (c > 0)
                {
                    c--;
                    yield return new WaitForSeconds(skillAtackDelay);
                    if (attackOption == AttackOption.LockOnAttack) //missile
                    {
                        currentRotation = SearchMonster();
                        if (currentRotation == Quaternion.identity)
                            break;
                    }
                    GameObject g = Instantiate(SpawnPrefab, ParentTransform);
                    ThrowSkill b = g.GetComponent<ThrowSkill>();
                    b.setThrowSkills(StageManager.Instance.playerScript.getDamage(),
                       Duration, ClearPrefabsTime, Speed, pointtype
                       );
                    g.transform.position = getPlayerTF().position;
                    g.transform.rotation = currentRotation;


                    //반대방향으로 만들기
                    if ((attackOption & AttackOption.otherwayAttack)!=0) // swordSlash
                    {
                        if (turn == -1)
                            g.transform.rotation = currentRotation * Quaternion.Euler(0, 0, 180);
                        turn *= -1;
                    }
                }
            }
            else
            {
                GameObject g = Instantiate(SpawnPrefab, ParentTransform);
                ThrowSkill b = g.GetComponent<ThrowSkill>();
                g.transform.position = getPlayerTF().position;
                b.setThrowSkills(StageManager.Instance.playerScript.getDamage() * 2,
                   Duration, ClearPrefabsTime, Speed, pointtype
                   );
                if (pointtype == PointType.UI && (attackOption & AttackOption.LevelupCountUp) != 0)
                { //Hammer
                    while (c > 0)
                    {
                        spawnAttackObjects.Add(g);
                        c--;
                        yield return new WaitForSeconds(0.1f);
                    }
                    if (c == 0)
                    {
                        yield return new WaitForSeconds(ClearPrefabsTime);
                        foreach(GameObject go in spawnAttackObjects)
                        {
                            if (go != null) Destroy(go);
                        }
                        spawnAttackObjects.Clear();
                    }
                }
                else //durian
                {
                    g.transform.localScale = LevelupScale;
                    yield return new WaitForSeconds(ClearPrefabsTime);
                }
            }
        }
    }
}
