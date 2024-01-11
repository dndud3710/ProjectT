
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    //�������� �������� �����ͼ� �ش� ���͵��� ��ȯ
    public GameObject[] StageMonsters;
    // public Gameobject

    //���� ����Ʈ��
    List<List<GameObject>> DeadMonsters;
    List<List<GameObject>> AliveMonsters;
    // 0~2 �Ϲ�,�߰����� , 2~5 ��������, 5�� ����, 5~8 ��ü�� �ι�, 8 �߰�����, 8~10 �ֻ�����, 10�� ��������

    Transform player;

    //����
    public GameObject getEXPSound;
    public GameObject AttackSound;

    private void Awake()
    {
        Instance = this;
        DeadMonsters = new List<List<GameObject>>();
        
        AliveMonsters = new List<List<GameObject>>();

    }
    private void Start()
    {
        StageMonsters = DataManager.Instance.getStageMonsters(GameManager.Instance.SetStageLevel);
        int j = 0;
        foreach(GameObject p in StageMonsters)
        {
            //monsterŸ���� �������°ǵ� ��,... ����� ���߿� �����ؾ��ҵ�
            Monster monscript = p.GetComponent<SmallMonster>();
                    DeadMonsters.Add(new List<GameObject>());
                    AliveMonsters.Add(new List<GameObject>());
                    DeadMonsters[j].Capacity = 100;
                    AliveMonsters[j].Capacity = 100;
                    for (int i = 0; i < 100; i++)
                    {
                        DeadMonsterInit(j, Instantiate(p, transform));
                    }
            j++;
        }
        player = StageManager.Instance.Player.transform;
    }

    public void DeadMonsterInit(int wave, GameObject monsterobject)
    {
        SmallMonster monscript = monsterobject.GetComponent<SmallMonster>();
        monscript.Wave = wave;
        monsterobject.transform.position = new Vector2(0, 0);
        monsterobject.SetActive(false);
        DeadMonsters[wave].Add(monsterobject);
    }

    public void AliveMonster(int wave)
    {
        if (DeadMonsters[wave].Count > 0)
        {
            GameObject mon = DeadMonsters[wave][0];
            mon.SetActive(true);
            DeadMonsters[wave].Remove(mon);
            mon.transform.position = MonsterRegeneratorRange();
            
            AliveMonsters[wave].Add(mon);
        }
    }
    public void DeadMonster(int wave,GameObject monsterobj)
    {
        if (AliveMonsters[wave].Count > 0)
        {
            
            AliveMonsters[wave].Remove(monsterobj);
            DeadMonsters[wave].Add(monsterobj);
            monsterobj.SetActive(false);
        }
    }
    
    Vector2 MonsterRegeneratorRange()
    {
        int r = Random.Range(0, 4);
        Vector2 vec = new Vector2(0,0);
        Vector2 pvec = player.transform.position;
        switch (r)
        {
            case 0:
                vec  = new Vector2(Random.Range(-6, 6), Random.Range(3, 6));//��;
                break;
            case 1:
                vec = new Vector2(Random.Range(3, 6), Random.Range(-6, 6));//��
                break;
            case 2:
                vec = new Vector2(Random.Range(-6, -3), Random.Range(-6, 6));//��
                break;
            case 3:
                vec = new Vector2(Random.Range(-6, 6), Random.Range(-6, -3));//��
                break;
        }
        return pvec + vec;
    }
}


/*private void Start()
    {
        Instantiate (StageMonsters);
        int j = 0;
        foreach(GameObject p in stage1_3_Monsters)
        {
            //monsterŸ���� �������°ǵ� ��,... ����� ���߿� �����ؾ��ҵ�
            Monster monscript = p.GetComponent<SmallMonster>();
            if ((int)monscript.stage == GameManager.Instance.SetStageLevel)
            {
                    DeadMonsters.Add(new List<GameObject>());
                    AliveMonsters.Add(new List<GameObject>());
                    DeadMonsters[j].Capacity = 100;
                    AliveMonsters[j].Capacity = 100;
                    for (int i = 0; i < 100; i++)
                    {
                        DeadMonsterInit(j, Instantiate(p, transform));
                    }
            }
            else if((int)monscript.stage > GameManager.Instance.SetStageLevel)
            {
                break;
            }
            j++;
            
        }
        player = StageManager.Instance.Player.transform;
    }

    public void DeadMonsterInit(int wave, GameObject monsterobject)
    {
        SmallMonster monscript = monsterobject.GetComponent<SmallMonster>();
        monscript.Wave = wave;
        monsterobject.transform.position = new Vector2(0, 0);
        monsterobject.SetActive(false);
        
        DeadMonsters[wave].Add(monsterobject);
    }

    public void AliveMonster(int wave)
    {
        if (DeadMonsters[wave].Count > 0)
        {
            GameObject mon = DeadMonsters[wave][0];
            DeadMonsters[wave].Remove(mon);
            mon.transform.position = MonsterRegeneratorRange();
            mon.SetActive(true);
            AliveMonsters[wave].Add(mon);
        }
    }
    public void DeadMonster(int wave,GameObject monsterobj)
    {
        if (AliveMonsters[wave].Count > 0)
        {
            AliveMonsters[wave].Remove(monsterobj);
            monsterobj.SetActive(false);
            DeadMonsters[wave].Add(monsterobj);
        }
    }
    */