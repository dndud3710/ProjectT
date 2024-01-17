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

    //������
    public GameObject playerPrefs;

    public GameObject Player;
    public Player playerScript;


    /// <summary>
    /// UI ���� ������Ʈ �� ������Ʈ
    /// </summary>
    public GameObject JoyStick;
    public CinemachineVirtualCamera VirtualCameara_;
    public BoxCollider2D[] CameraWall;//������� 0;��,1:��,2:��,3:�Ʒ�

    public Transform UISkillParent;
    public Transform WorldSkillParent;

    public Failed FailedPanel;//�������� �� ��Ÿ���� UI 
    public GameObject deadPArticle;
    public GameState StateUI;//�ð�,exp��,ȹ����,ų���¸� �����ִ� UI����������Ʈ
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
    /// ������ ����
    /// </summary>
    Dictionary<int, int> ExpItemGetExp; //������ �ٸ� exp ��� �� ����
    Dictionary<int, GameObject> Items;
    public GameObject[] IngameItems;
    List<InGameItem> stageInItems; //���������� ������ �ִ� �����۵�, �ڼ������۸����� ���� �԰ԵǰԸ���
    //���⼭ ingameItemŬ������ list�� �� ������ gameobject�� �ϸ� �ڼ��������� �Ծ����� ���� �����۵��� Use�ϰ� destroy�Ҷ�
    //getcomponent IngameItem�� ���������� �����ؾߵǱ� �����̴�, ingameitemŬ������������ �ִ� gameobject�� ã�ƾ��Ҷ��� ������
    //�ڼ���ŭ ���� getcomponent�� �Ҷ��� �����ʾƼ� �̷��� �Ͽ���
    

    //��ƼŬ ���߿� �ٲܰ�.
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

    //ī�޶� �� �ʱ�ȭ
    void CameraWallInit()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        // ��� �� �ϴ� �ݶ��̴� ����
        CameraWall[2].size = new Vector2(width, 0.1f);
        CameraWall[3].size = new Vector2(width, 0.1f);
        CameraWall[2].offset = new Vector2(0, Camera.main.orthographicSize);
        CameraWall[3].offset = new Vector2(0, -Camera.main.orthographicSize);

        // �¿� �ݶ��̴� ����
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
    public Stopwatch getTime() //�ΰ��� stateUI���� ������ �ð� stopwatch
    {
        return st;
    }

    #region ������ ����
    //������ ����
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
    
    //���Ͱ� disable�ɶ� �ű⼭ ȣ��
    public void makeItem(Transform t_,EInGameItemType e_)
    {
        GameObject g_ = Instantiate(Items[(int)e_], ObjectPool.Instance.transform); //�׳� ������ƮǮ�����Ѱ�
        g_.transform.position = t_.position;
        stageInItems.Add(g_.GetComponent<InGameItem>());
    }

    //�÷��̾ �������� �Ծ����ÿ� �ߵ� ,���Ͱ� �״°� ������ƮǮ�������̴� ������ƮǮ���� �۵�
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
        //�¸��ϸ� ���� ����ġ ����ȹ��
        if (d)
        {
            SceneManager.LoadScene("Main Scene");
        }
        else //�����ϸ� �� �����̤�
            SceneManager.LoadScene("Main Scene");
        AudioManager.Instance.MenuBeepPlay();
    }
}
