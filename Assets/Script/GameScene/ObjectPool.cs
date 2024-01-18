
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// 게임을 만들면서 느낀점
    /// objectpool의 설계를 잘해야할 것 같다
    /// 예를들어 몬스터는 잘 만들었지만, exp아이템이라던지 많이 나올거같은 것들까지 신경쓰면서 설계했었어야햇다
    /// 다음에는 오브젝트 풀은 몬스터 리스트 관리만. 실제로 생성 및 죽는것은 스테이지에서 할 예정
    /// 너무 개판이다 진로를 바꿀까
    /// </summary>
    public static ObjectPool Instance;
    //스테이지 프리팹을 가져와서 해당 몬스터들을 소환
    public GameObject[] StageMonsters;
    public GameObject BossMonster;
    // public Gameobject

    //몬스터 리스트들
    List<List<GameObject>> DeadMonsters;
    List<List<GameObject>> AliveMonsters;
    // 0~1 일반웨이브 2 보스

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
        StageMonsters = DataManager.Instance.getStageMonsters(PlayerPrefs.GetInt("Stage"));
        BossMonster = DataManager.Instance.BossMonster(PlayerPrefs.GetInt("Stage"));
        int j = 0;
        foreach(GameObject p in StageMonsters)
        {
            //monster타입을 가져오는건데 흠,... 방법을 나중에 수정해야할듯
            Monster monscript = p.GetComponent<SmallMonster>();
                    DeadMonsters.Add(new List<GameObject>());
                    AliveMonsters.Add(new List<GameObject>());
                    DeadMonsters[j].Capacity = 100;
                    AliveMonsters[j].Capacity = 100;
                    for (int i = 0; i < 200; i++)
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
            StageManager.Instance.makeItem(monsterobj.transform,monsterobj.GetComponent<Monster>().rootItem);
            AliveMonsters[wave].Remove(monsterobj);
            DeadMonsters[wave].Add(monsterobj);
            monsterobj.SetActive(false);
        }
    }
    public void BossStart()
    {
        int len;
        for(int i=0;i<2;i++)
        {
            len = AliveMonsters[i].Count;
            for (int j = 0; j < len; j++) {
                DeadMonsters[i].Add(AliveMonsters[i][0]);
                AliveMonsters[i][0].SetActive(false);
                AliveMonsters[i].Remove(AliveMonsters[i][0]);
            }
        }
        
    }
    public void makeBoss()
    {
        GameObject g_ = Instantiate(BossMonster);
        g_.transform.position = new Vector3(StageManager.Instance.Player.transform.position.x+1, StageManager.Instance.Player.transform.position.y +1, 0);
        StageManager.Instance.StateUI.BossStart(g_.GetComponent<Monster>().maxHp);
    }
    
    public Vector2 MonsterRegeneratorRange()
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