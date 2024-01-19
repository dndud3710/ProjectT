using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 핵심 : GameManager는 씬이 변경되도 파괴되지 않는다, 즉 Main씬의 UI를 여기서 참조하면 안될것
    /// 혹여 참조했다하면 mainscene에서 UI를 클릭하는 이벤트같은 처리에서 UI를 변경할것
    /// </summary>

    public static GameManager Instance;
    //Scene 관리
    short SceneNum;
    //UI들 init이벤트 : 메인씬에들어왔을때 바뀌는 것들
    public UnityAction initEvent;

    /// <summary>
    /// 챕터 레벨 관련
    /// </summary>
    int Stagelevel; //현재 챕터가 무엇인지 Main Scene에서 변경/확인 이 가능하며 선택한 챕터로 게임시작을 하면 stageManager에 현재 스테이지 레벨을 전달
    public int SetStageLevel { get { return Stagelevel; } set { Stagelevel = value; } }

    Dictionary<int, string> StageName; //ex) Key : 1 , Value : 야생 거리
    Dictionary<string, Sprite> StageSprite;


    public EquipUI equipUI;

    public GameObject SlotPrefab;
    public GameObject[] EquipItemPrefabs; //아이템 프리팹들

    [HideInInspector] public EquipItem[] equips;
    List<GameObject> EquipInventory;
    Dictionary<string, GameObject> EquipItemsDictionary;
    [HideInInspector]
    public List<EquipItem> EquipWeaponsList { get; private set; }
    //메인씬의 플레이어 스탯을 받아서 인게임 플레이어에 넘겨주는 함수 : 나중에 구조체전달로 바꿔도 될듯
    private int[] playerstat;// 인게임으로 옮길 플레이어 스탯
    private int[] prevplayerstat; // 인게임이 끝나면 다시 메인화면으로 전달될 플레이어 스탯

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            varInit();
            DictionaryInit();

            PlayerInfoInit();
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        // 자료구조 초기화

        
        Starting();
       
        GameManager.Instance.getItem("검");
        GameManager.Instance.getItem("총");
        //프리팹 같은 로딩
        //씬마다 다른 로딩은 함수에서
        int num;
        for (int i = 0; i < DataManager.Instance.stage.Length; i++)
        {
            num = i + 1;
            StageName.Add(num, DataManager.Instance.getStageName(num));
            StageSprite.Add(StageName[num], DataManager.Instance.getStageImage(num));
        }
        initEvent?.Invoke();
    }
    
    public void Bbolgi()
    {
        int n = UnityEngine.Random.Range(0, 5);
        switch (n)
        {
            case 0:
                GameManager.Instance.getItem("검");
                break;
                case 1:
                GameManager.Instance.getItem("총");
                break;
                case 2:
                GameManager.Instance.getItem("가죽투구");
                break;
                case 3:
                GameManager.Instance.getItem("가죽바지");
                break;
            case 4:
                GameManager.Instance.getItem("목걸이");
                break;
        }
        

    }
    public void PlayerInfoInit()
    {
        prevplayerstat = new int[2] { 0, 0 };
        playerstat = new int[2];
        PlayerPrefs.SetInt("Stage", 1);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetString("Name", "우럭");
        PlayerPrefs.SetInt("PlayerCurEXP", 0);
        PlayerPrefs.SetInt("PlayerCurCoin", 20);
        PlayerPrefs.SetInt("PlayerMaxCoin", 20);
        PlayerPrefs.Save();
        
    }
    #region 인벤토리

    public void getItem(string s)
    {
        GameObject g_ = getItemObject(s);
        //인벤토리에 획득

        if (g_.GetComponent<EquipItem>())
        {
            EquipInventory.Add(g_);
            equipUI.AddSlot(g_);
        }
    }
    //t_가 true일때는 object를 영구히 삭제
    public void deleteItem(GameObject g_)
    {
        if (g_.GetComponent<EquipItem>())
        {
            //slots 리스트 안에있는 slot오브젝트의 slotscript스크립트의 item이 g_와 같다면
            GameObject gg_ = equipUI.getSlot(g_);
            equipUI.Slots.Remove(gg_);
            EquipInventory.Remove(g_);
            Destroy(gg_);
        }
    }
    #endregion

    #region 씬 초기화
    public void MainSceneInit()
    {
        foreach(EquipItem e in  EquipWeaponsList)
        {
            getItem(e.ItemName);
        }
        print(equips.Length);
    }
    void GameSceneInit()
    {
        
    }
    #endregion

    private void Update()
    {
        
    }

    
    #region 자료구조 및 변수 초기화
    void varInit()
    {
        Stagelevel = 1;
        equips = new EquipItem[Enum.GetValues(typeof(EEquipItemType)).Length];
    }
    void DictionaryInit()
    {
        
        StageName = new Dictionary<int, string>();
        StageSprite = new Dictionary<string, Sprite>();
        EquipItemsDictionary = new Dictionary<string, GameObject>();
        EquipInventory = new List<GameObject>();
        EquipWeaponsList = new List<EquipItem>();
        int num;
        

        foreach(GameObject g_ in EquipItemPrefabs)
        {
            EquipItem etype_ = g_.GetComponent<EquipItem>();
            EquipItemsDictionary.Add(etype_.ItemName, g_);
            if(etype_.type == EEquipItemType.Weapon)
            {
                EquipWeaponsList.Add(etype_);
            }
        }
    }
    #endregion


    #region dictionary 외부 참조 함수
    public string getStageName(int a)
    {
        return StageName[a];
    }
    public Sprite getStageImage(int a)
    {
        return StageSprite[StageName[a]];
    }
    public GameObject getItemObject(string s)
    {
        return EquipItemsDictionary[s];
    }
    #endregion

    public void setPlayerStat(int damage_, int health_)
    {
        playerstat[0] = damage_ + 20;
        playerstat[1] = health_ + 200;
        prevplayerstat[0] = damage_;
        prevplayerstat[1] = health_;
    }
    public int[] getPlayerStat()
    {
        return playerstat;
    }
   public int[] getPrevPlayerStat()
    {
        return prevplayerstat;
    }
    public void getReward(int gold, int exp)
    {
        int gold_ = PlayerPrefs.GetInt("Money");
        int exp_ = PlayerPrefs.GetInt("PlayerCurEXP");
        gold_ += gold;
        exp_ += exp;
        PlayerPrefs.SetInt("Money", gold_);
        PlayerPrefs.SetInt("PlayerCurEXP", exp_);
        PlayerPrefs.Save();
    }
    
    void Starting()
    {
        LoadSceneManager.Instance.PlayerStateUI.GetComponent<PlayerInfoUI>().Subscribe();
        LoadSceneManager.Instance.mcsi.Subscribe();
    }
}
