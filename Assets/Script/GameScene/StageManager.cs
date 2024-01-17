using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    //프리팹
    public GameObject playerPrefs;

    public GameObject Player;
    public Player playerScript;


    /// <summary>
    /// UI 관련 오브젝트 및 컴포넌트
    /// </summary>
    public GameObject JoyStick;
    public CinemachineVirtualCamera VirtualCameara_;
    public BoxCollider2D[] CameraWall;//순서대로 0;좌,1:우,2:위,3:아래

    public Transform UISkillParent;
    public Transform WorldSkillParent;

    public Failed FailedPanel;//실패했을 시 나타나는 UI 
    public GameObject deadPArticle;
    public GameState StateUI;//시간,exp바,획득골드,킬상태를 보여주는 UI관리오브젝트
    public SkillManage skillmanager;
    public Failed failedUI;
    public WinU winUI;
    public GameObject BossRoom;

    Stopwatch st;
    const short  zero_ = 0;
    private int Killnum;
    private int Coins;
    public GameObject ItemChests;

    /// <summary>
    /// 아이템 관련
    /// </summary>
    Dictionary<int, int> ExpItemGetExp; //색마다 다른 exp 얻는 값 정함
    Dictionary<int, GameObject> Items;
    public GameObject[] IngameItems;
    List<InGameItem> stageInItems; //스테이지에 떨어져 있는 아이템들, 자석아이템먹으면 전부 먹게되게만듦
    //여기서 ingameItem클래스로 list를 둔 이유는 gameobject로 하면 자석아이템을 먹었을때 많은 아이템들을 Use하고 destroy할때
    //getcomponent IngameItem을 순간적으로 많이해야되기 때문이다, ingameitem클래스를가지고 있는 gameobject를 찾아야할때도 있지만
    //자석만큼 많이 getcomponent를 할때가 오지않아서 이렇게 하였다
    

    //파티클 나중에 바꿀거.
    public void deadPrticlePlay(Transform TF)
    {
       GameObject g= Instantiate(deadPArticle);
        g.transform.position = TF.position;
        ParticleSystem pa = g.GetComponent<ParticleSystem>();
        pa.Play();
        StartCoroutine(partidelete(pa));
    }
    IEnumerator partidelete(ParticleSystem pa)
    {
        yield return new WaitForSeconds(pa.main.duration);
        Destroy(pa.gameObject);
    }

    //카메라 벽 초기화
    void CameraWallInit()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        // 상단 및 하단 콜라이더 설정
        CameraWall[2].size = new Vector2(width, 0.1f);
        CameraWall[3].size = new Vector2(width, 0.1f);
        CameraWall[2].offset = new Vector2(0, Camera.main.orthographicSize);
        CameraWall[3].offset = new Vector2(0, -Camera.main.orthographicSize);

        // 좌우 콜라이더 설정
        CameraWall[0].size = new Vector2(0.1f, height);
        CameraWall[1].size = new Vector2(0.1f, height);
        CameraWall[0].offset = new Vector2(-width / 2, 0);
        CameraWall[1].offset = new Vector2(width / 2, 0);
    }

    private void Awake()
    {
        Instance = this;
        Killnum = 0;
        Coins = 0;
        Player = Instantiate(playerPrefs);
        Player.transform.position = Vector2.zero;
        InitItem();
    }
    private void Start()
    {
        VirtualCameara_.Follow = Player.transform;
        st = new Stopwatch();
        st.Start();
        playerScript = Player.GetComponent<Player>();
        StartCoroutine(MonsterRegen());

        CameraWallInit();
        Instantiate(DataManager.Instance.getMapTile(PlayerPrefs.GetInt("Stage")));
        
    }
   
    public void MakeBossRoom()
    {
        GameObject g_ = Instantiate(BossRoom);
        g_.transform.position = Player.transform.position;
    }
    


    IEnumerator MonsterRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 3; i++)
                ObjectPool.Instance.AliveMonster(0);
            if (st.Elapsed.Seconds > 5)
                for (int i = 0; i < 2; i++)
                    ObjectPool.Instance.AliveMonster(1);
            if (st.Elapsed.Seconds >=30)
            {
                ObjectPool.Instance.BossStart();
                for (int i = 0; i < 3; i++)
                {
                    StateUI.SetCount(i);
                    yield return new WaitForSeconds(1f);
                }
                StateUI.bosscount.gameObject.SetActive(false);
                MakeBossRoom();
                ObjectPool.Instance.makeBoss();
                break;
            }
        }
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Scene");
    }


    public void Win()
    {
        winUI.Win();
        winUI.setText(Killnum, Coins);
        st.Stop();
    }
    public void Lose()
    {
        failedUI.Fail();
        failedUI.setText(Killnum,st);
        st.Stop();
    }


    public void Monsterkill()
    {
        Killnum++;
        StateUI.setKillText(Killnum);
    }
    public void getCoins(int coins_)
    {
        Coins += coins_;
        StateUI.setMoneyText(Coins);
    }
    public Stopwatch getTime() //인게임 stateUI에서 가져올 시간 stopwatch
    {
        return st;
    }

    #region 아이템 관련
    //아이템 관련
    void InitItem()
    {
        ExpItemGetExp = new Dictionary<int, int>();
        Items = new Dictionary<int, GameObject>();
        stageInItems = new List<InGameItem>();
        ExpItemGetExp.Add(1, 4);
        ExpItemGetExp.Add(2, 8);
        ExpItemGetExp.Add(3, 14);

        for (int i = 0; i < IngameItems.Length; i++)
        {
            Items.Add(i, IngameItems[i]);
        }
    }

    public int getItemExp(int expitemLevel)
    {
        if (expitemLevel < 0 && expitemLevel > 3)
            return 0;
        return ExpItemGetExp[expitemLevel];
    }
    
    //몬스터가 disable될때 거기서 호출
    public void makeItem(Transform t_,EInGameItemType e_)
    {
        GameObject g_ = Instantiate(Items[(int)e_], ObjectPool.Instance.transform); //그냥 오브젝트풀에다한거
        g_.transform.position = t_.position;
        stageInItems.Add(g_.GetComponent<InGameItem>());
    }

    //플레이어가 아이템을 먹었을시에 발동 ,몬스터가 죽는건 오브젝트풀링판정이니 오브젝트풀에서 작동
    public void deleteInGameItemList(GameObject g_)
    {
        InGameItem igi_ = g_.GetComponent<InGameItem>();
        stageInItems.Remove(igi_);
    }
    
    public List<InGameItem> MagenetItem()
    {
        List<InGameItem> magnetList = new List<InGameItem> ();
        magnetList.Capacity = stageInItems.Count;
        foreach(InGameItem item_ in stageInItems)
        {
            magnetList.Add(item_);
        }
        return magnetList;
    }

    #endregion

    public void Ending(bool d)
    {
        //승리하면 골드랑 경험치 보상획득
        if (d)
        {
            SceneManager.LoadScene("Main Scene");
        }
        else //실패하면 무 없으ㅜㅁ
            SceneManager.LoadScene("Main Scene");
        AudioManager.Instance.MenuBeepPlay();
    }
}
