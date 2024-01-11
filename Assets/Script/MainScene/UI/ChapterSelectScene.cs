using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectScene : MonoBehaviour
{

    /// <summary>
    /// 챕터를 바꿀수 있는 chapterselect UI
    /// </summary>
    [Header("변경되는 UI오브젝트")]
    public TextMeshProUGUI StageName;
    public TextMeshProUGUI StageDiscript;
    public Image StageImage;
    public PlayerInfo playerinfo;


    private int stage;
    private void OnEnable()
    {
        ChapterSelectInit();
    }
    /// <summary>
    /// 메인에서 이미지를 선택하면 이창으로 오는데 여기서 이 씬으로 왔을때 현재 챕터를 불러와서 정보를 띄움
    /// 버튼에 들어갈 것 (이것은 메인 씬에서 챕터를 선택하려고 이미지를 클릭했을때만 실행)
    /// </summary>
    void ChapterSelectInit()
    {
        stage = playerinfo.getCurStage();
        StageName.text = $"{stage}. {GameManager.Instance.getStageName(stage)}";
        StageImage.sprite = GameManager.Instance.getStageImage(stage);
    }

    /// <summary>
    /// 스테이지 이동
    /// </summary>
    public void prevStage()
    {
        //스테이지가 1보다 이상일때만 클릭 가능
        if (stage > 1)
        {
            stage--;
            StageName.text = $"{stage}. {GameManager.Instance.getStageName(stage)}";
            StageImage.sprite = GameManager.Instance.getStageImage(stage);
            
        }
        AudioManager.Instance.MenuBeepPlay();
    }
    public void nextStage()
    {
        //스테이지가 3보다 작을때만 클릭 가능
        if (stage < 3)
        {
            stage++;
            StageName.text = $"{stage}. {GameManager.Instance.getStageName(stage)}";
            StageImage.sprite = GameManager.Instance.getStageImage(stage);
            
        }
        AudioManager.Instance.MenuBeepPlay();
    }

    public void SelectStage()
    {
        playerinfo.setCurStage(stage);
        LoadSceneManager.Instance.MainScene();
        AudioManager.Instance.MenuBeepPlay();
    }
}
