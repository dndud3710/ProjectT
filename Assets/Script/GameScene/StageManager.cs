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
    //��������
    public int StageLevel;

    //������
    public GameObject playerPrefs;

    public GameObject Player;
    public Player playerScript;


    /// <summary>
    /// UI ���� ������Ʈ �� ������Ʈ
    /// </summary>
    public GameObject JoyStick;
    public CinemachineVirtualCamera VirtualCameara_;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI KillText;
    private int Killnum;
    public TextMeshProUGUI LevelText;
    public BoxCollider2D[] CameraWall;//������� 0;��,1:��,2:��,3:�Ʒ�

    public Failed FailedPanel;//�������� �� ��Ÿ���� UI 
    public GameObject deadPArticle;


    /// <summary>
    /// ��ų��
    /// </summary>
    public GameObject[] Skill;



    Stopwatch st;
    const short  zero_ = 0;


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
