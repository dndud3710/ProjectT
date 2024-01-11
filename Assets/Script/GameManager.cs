using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    //�ӽ� �̹���
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            varInit();
            DictionaryInit();
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        // �ڷᱸ�� �ʱ�ȭ
        PlayerInfoInit();
        //������ ���� �ε�
        //������ �ٸ� �ε��� �Լ�����

    }
    public void PlayerInfoInit()
    {
        PlayerPrefs.SetInt("Stage", 1);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetString("Name", "�췰");
        PlayerPrefs.SetInt("PlayerCurEXP", 0);
        PlayerPrefs.SetInt("PlayerCurCoin", 20);
        PlayerPrefs.SetInt("PlayerMaxCoin", 20);
        PlayerPrefs.Save();

    }


    #region �� �ʱ�ȭ
    void MainSceneInit()
    {

    }
    void GameSceneInit()
    {
        
    }
    #endregion

    private void Update()
    {
        //�ʱ�ȭ ����
        if(SceneNum < 255)
        {
            switch(SceneNum)
            {
                case 0: //���� ���� �Ǵ� ����
                    MainSceneInit();
                    break;
                case 1:
                    GameSceneInit();
                    break;

            }
            // �ٽ� �������� ���ϵ���
            SceneNum = 256;
        }
    }

    
    #region �ڷᱸ�� �� ���� �ʱ�ȭ
    void varInit()
    {
        Stagelevel = 1;
    }
    void DictionaryInit()
    {
        
        StageName = new Dictionary<int, string>();
        StageSprite = new Dictionary<string, Sprite>();

        StageName.Add(1, "�߻� �Ÿ�");
        StageName.Add(2, "���� ����");
        StageName.Add(3, "���� ������");

        StageSprite.Add(StageName[1], sprite1);
        StageSprite.Add(StageName[2], sprite2);
        StageSprite.Add(StageName[3], sprite3);
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
    #endregion
    
}
