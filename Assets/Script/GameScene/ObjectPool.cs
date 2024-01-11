
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    //스테이지 프리팹을 가져와서 해당 몬스터들을 소환
    public GameObject[] StageMonsters;
    // public Gameobject

    //몬스터 리스트들
    List<List<GameObject>> DeadMonsters;
    List<List<GameObject>> AliveMonsters;
    // 0~2 일반,중간보스 , 2~5 상위섞임, 5분 보스, 5~8 개체량 두배, 8 중간보스, 8~10 최상위종, 10분 최종보스

    Transform player;

    //사운드
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
            //monster타입을 가져오는건데 흠,... 방법을 나중에 수정해야할듯
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
                vec  = new Vector2(Random.Range(-6, 6), Random.Range(3, 6));//북;
                break;
            case 1:
                vec = new Vector2(Random.Range(3, 6), Random.Range(-6, 6));//동
                break;
            case 2:
                vec = new Vector2(Random.Range(-6, -3), Random.Range(-6, 6));//서
                break;
            case 3:
                vec = new Vector2(Random.Range(-6, 6), Random.Range(-6, -3));//남
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
            //monster타입을 가져오는건데 흠,... 방법을 나중에 수정해야할듯
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