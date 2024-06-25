using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkills : MonoBehaviour , IngameSkill
{


    // 1. ������ ������ ��ų ������Ʈ�� ���� List
    private List<GameObject> spawnAttackObjects;
    // 2. ��
    [Tooltip("��ų ������Ʈ ��ȯ������")]
    public GameObject SpawnPrefab;

    [Flags]
    public enum AttackOption
    {
        None = 0,
        LockOnAttack =1 <<0, //���󰡴� �������� �ƴ���
        LevelupCountUp = 1 <<1, //�������� ������ �����ϴ� ������
        LevelupSizeup = 1 <<2, //�������� ũ�Ⱑ Ŀ���°�����
        otherwayAttack = 1<<3,//�ݴ���� �������� �ƴ���
    }
    public AttackOption attackOption;

    //RectTransform������ UI, Transform�������� �����̴°� World
    [Tooltip("UI���� ��ǥ���� World���� ��ǥ����")]
    public PointType pointtype;
    [Tooltip("��ų�� � �������� ����")]
    public int count = 1;
    [Tooltip("��ų ��Ÿ��")]
    public float coolDown = 4f;
    [Tooltip("������Ʈ ���� Ƚ�� (-10�ϰ�� ����)")]
    public int Duration = 20;
    [Tooltip("������Ʈ �ı� �ð�")]
    public float ClearPrefabsTime = 20f;
    [Tooltip("������Ʈ�� ���󰡴� �ӵ�")]
    public float Speed = 2f;
    [Tooltip("��ų�� ��ȯ�Ǵ� ������")]
    public float skillAtackDelay = 2f;
    private int SkillLevel;
    //���� UI�� World�� canvas skill�� �θ���Ұ���, �÷��̾��� skill�� �θ�� �Ұ���
    protected Transform ParentTransform;

    Transform PlayerTF;
    Transform PlayerRot;
    //�ڵ�ȭ �����ؼ� �׳� ���� ��
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
            Debug.Log($"count ����@@@@@@@@@   : {count}");
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
            //��ų ���� �ð�

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


                    //�ݴ�������� �����
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
