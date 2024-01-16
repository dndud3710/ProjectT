using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;
    // 이 클래스는 Main Scene의 컨텐츠들을 시작할 수 있는 버튼을 누르면 Scene이 이동되게끔 하는 클래스
    // 이 함수들은 주로 button에 등록되어있다
    AsyncOperation operation;
    public GameObject loadingpanel;
    private Color panelAlpha;
    private Image panelImage;


    public GameObject PlayerStateUI;
    public GameObject MainGameObject;
    public GameObject ChapterSelectGameObject;

    public Transform CameraTF;
    public Transform MainTF;
    public Transform ChapterSelectTF;
    public Transform EquipTF;

    public PlayerInfo palyerinfo;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        panelAlpha = loadingpanel.GetComponent<Image>().color;
        panelImage = loadingpanel.GetComponent<Image>();
    }
    public void GameSceneLoad()
    {
        operation = SceneManager.LoadSceneAsync("Game Scene");
        loadingpanel.SetActive(true);
        AudioManager.Instance.BGMPlay(AudioManager.BGMList.전투음악);
        AudioManager.Instance.MenuBeepPlay();
        palyerinfo.StartGameCoin();
        GameManager.Instance.setPlayerStat(palyerinfo.getDamage(), palyerinfo.getHealth());
        StartCoroutine(LoadCoroutine());
    }



    public float progress;





    IEnumerator LoadCoroutine()
    {
       while(operation.isDone == false)
        {
            progress = operation.progress;
            if (progress > 0.8f)
            {
                panelAlpha.a += 1f;
                panelImage.color = panelAlpha;
            }
            yield return null;
        }

       //로드 버튼 활성화
    }




    public void ChapterSelectScene()
    {
        CameraTF.position =new Vector3(ChapterSelectTF.position.x,ChapterSelectTF.position.y,CameraTF.position.z);
        PlayerStateUI.SetActive(false);
        MainGameObject.SetActive(false);
        ChapterSelectGameObject.SetActive(true);
        AudioManager.Instance.MenuBeepPlay();
    }
    public void MainScene()
    {
        CameraTF.position = new Vector3(MainTF.position.x, MainTF.position.y, CameraTF.position.z);
        MainGameObject.SetActive(true);
        PlayerStateUI.SetActive(true);
        ChapterSelectGameObject.SetActive(false);
        GameManager.Instance.initEvent?.Invoke();
        AudioManager.Instance.MenuBeepPlay();
        GameManager.Instance.equipUI.gameObject.SetActive(false);
    }
    //장비창으로 넘어가기
    public void EquipScene()
    {
        CameraTF.position = new Vector3(EquipTF.position.x, MainTF.position.y, CameraTF.position.z);
        MainGameObject.SetActive(false);
        GameManager.Instance.equipUI.gameObject.SetActive(true);
        AudioManager.Instance.MenuBeepPlay();
    }
}
