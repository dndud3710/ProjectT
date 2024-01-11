using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    //임시 이미지
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
        // 자료구조 초기화
        PlayerInfoInit();
        //프리팹 같은 로딩
        //씬마다 다른 로딩은 함수에서

    }
    public void PlayerInfoInit()
    {
        PlayerPrefs.SetInt("Stage", 1);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetString("Name", "우럭");
        PlayerPrefs.SetInt("PlayerCurEXP", 0);
        PlayerPrefs.SetInt("PlayerCurCoin", 20);
        PlayerPrefs.SetInt("PlayerMaxCoin", 20);
        PlayerPrefs.Save();

    }


    #region 씬 초기화
    void MainSceneInit()
    {

    }
    void GameSceneInit()
    {
        
    }
    #endregion

    private void Update()
    {
        //초기화 영역
        if(SceneNum < 255)
        {
            switch(SceneNum)
            {
                case 0: //메인 씬이 되는 순간
                    MainSceneInit();
                    break;
                case 1:
                    GameSceneInit();
                    break;

            }
            // 다시 실행하지 못하도록
            SceneNum = 256;
        }
    }

    
    #region 자료구조 및 변수 초기화
    void varInit()
    {
        Stagelevel = 1;
    }
    void DictionaryInit()
    {
        
        StageName = new Dictionary<int, string>();
        StageSprite = new Dictionary<string, Sprite>();

        StageName.Add(1, "야생 거리");
        StageName.Add(2, "도시 공원");
        StageName.Add(3, "지하 주차장");

        StageSprite.Add(StageName[1], sprite1);
        StageSprite.Add(StageName[2], sprite2);
        StageSprite.Add(StageName[3], sprite3);
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
    #endregion
    
}
