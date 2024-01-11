using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    //스테이지
    public int StageLevel;

    //프리팹
    public GameObject playerPrefs;

    public GameObject Player;
    public Player playerScript;


    /// <summary>
    /// UI 관련 오브젝트 및 컴포넌트
    /// </summary>
    public GameObject JoyStick;
    public CinemachineVirtualCamera VirtualCameara_;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI KillText;
    private int Killnum;
    public TextMeshProUGUI LevelText;
    public BoxCollider2D[] CameraWall;//순서대로 0;좌,1:우,2:위,3:아래

    public Failed FailedPanel;//실패했을 시 나타나는 UI 
    public GameObject deadPArticle;


    /// <summary>
    /// 스킬들
    /// </summary>
    public GameObject[] Skill;



    Stopwatch st;
    const short  zero_ = 0;


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
        Player = Instantiate(playerPrefs);
        Player.transform.position = Vector2.zero;
    }
    private void Start()
    {
        VirtualCameara_.Follow = Player.transform;
        st = new Stopwatch();
        st.Start();
        playerScript = Player.GetComponent<Player>();
       // StartCoroutine(MonsterRegen());

        CameraWallInit();
    }
    private void Update()
    {
        TimeText.text = $"{st.Elapsed.ToString(@"mm\:ss").Replace(":", " : ")}";
    }



    IEnumerator MonsterRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            for(int i=0;i<5;i++)
                ObjectPool.Instance.AliveMonster(0);
        }
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void Monsterkill()
    {
        Killnum++;
        KillText.text = $"{Killnum}";
    }

}
