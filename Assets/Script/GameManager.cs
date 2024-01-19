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
    /// �ٽ� : GameManager�� ���� ����ǵ� �ı����� �ʴ´�, �� Main���� UI�� ���⼭ �����ϸ� �ȵɰ�
    /// Ȥ�� �����ߴ��ϸ� mainscene���� UI�� Ŭ���ϴ� �̺�Ʈ���� ó������ UI�� �����Ұ�
    /// </summary>

    public static GameManager Instance;
    //Scene ����
    short SceneNum;
    //UI�� init�̺�Ʈ : ���ξ����������� �ٲ�� �͵�
    public UnityAction initEvent;

    /// <summary>
    /// é�� ���� ����
    /// </summary>
    int Stagelevel; //���� é�Ͱ� �������� Main Scene���� ����/Ȯ�� �� �����ϸ� ������ é�ͷ� ���ӽ����� �ϸ� stageManager�� ���� �������� ������ ����
    public int SetStageLevel { get { return Stagelevel; } set { Stagelevel = value; } }

    Dictionary<int, string> StageName; //ex) Key : 1 , Value : �߻� �Ÿ�
    Dictionary<string, Sprite> StageSprite;


    public EquipUI equipUI;

    public GameObject SlotPrefab;
    public GameObject[] EquipItemPrefabs; //������ �����յ�

    [HideInInspector] public EquipItem[] equips;
    List<GameObject> EquipInventory;
    Dictionary<string, GameObject> EquipItemsDictionary;
    [HideInInspector]
    public List<EquipItem> EquipWeaponsList { get; private set; }
    //���ξ��� �÷��̾� ������ �޾Ƽ� �ΰ��� �÷��̾ �Ѱ��ִ� �Լ� : ���߿� ����ü���޷� �ٲ㵵 �ɵ�
    private int[] playerstat;// �ΰ������� �ű� �÷��̾� ����
    private int[] prevplayerstat; // �ΰ����� ������ �ٽ� ����ȭ������ ���޵� �÷��̾� ����

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
        // �ڷᱸ�� �ʱ�ȭ

        
        Starting();
       
        GameManager.Instance.getItem("��");
        GameManager.Instance.getItem("��");
        //������ ���� �ε�
        //������ �ٸ� �ε��� �Լ�����
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
                GameManager.Instance.getItem("��");
                break;
                case 1:
                GameManager.Instance.getItem("��");
                break;
                case 2:
                GameManager.Instance.getItem("��������");
                break;
                case 3:
                GameManager.Instance.getItem("���׹���");
                break;
            case 4:
                GameManager.Instance.getItem("�����");
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
        PlayerPrefs.SetString("Name", "�췰");
        PlayerPrefs.SetInt("PlayerCurEXP", 0);
        PlayerPrefs.SetInt("PlayerCurCoin", 20);
        PlayerPrefs.SetInt("PlayerMaxCoin", 20);
        PlayerPrefs.Save();
        
    }
    #region �κ��丮

    public void getItem(string s)
    {
        GameObject g_ = getItemObject(s);
        //�κ��丮�� ȹ��

        if (g_.GetComponent<EquipItem>())
        {
            EquipInventory.Add(g_);
            equipUI.AddSlot(g_);
        }
    }
    //t_�� true�϶��� object�� ������ ����
    public void deleteItem(GameObject g_)
    {
        if (g_.GetComponent<EquipItem>())
        {
            //slots ����Ʈ �ȿ��ִ� slot������Ʈ�� slotscript��ũ��Ʈ�� item�� g_�� ���ٸ�
            GameObject gg_ = equipUI.getSlot(g_);
            equipUI.Slots.Remove(gg_);
            EquipInventory.Remove(g_);
            Destroy(gg_);
        }
    }
    #endregion

    #region �� �ʱ�ȭ
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

    
    #region �ڷᱸ�� �� ���� �ʱ�ȭ
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


    #region dictionary �ܺ� ���� �Լ�
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
