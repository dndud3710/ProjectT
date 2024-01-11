using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectScene : MonoBehaviour
{

    /// <summary>
    /// é�͸� �ٲܼ� �ִ� chapterselect UI
    /// </summary>
    [Header("����Ǵ� UI������Ʈ")]
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
    /// ���ο��� �̹����� �����ϸ� ��â���� ���µ� ���⼭ �� ������ ������ ���� é�͸� �ҷ��ͼ� ������ ���
    /// ��ư�� �� �� (�̰��� ���� ������ é�͸� �����Ϸ��� �̹����� Ŭ���������� ����)
    /// </summary>
    void ChapterSelectInit()
    {
        stage = playerinfo.getCurStage();
        StageName.text = $"{stage}. {GameManager.Instance.getStageName(stage)}";
        StageImage.sprite = GameManager.Instance.getStageImage(stage);
    }

    /// <summary>
    /// �������� �̵�
    /// </summary>
    public void prevStage()
    {
        //���������� 1���� �̻��϶��� Ŭ�� ����
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
        //���������� 3���� �������� Ŭ�� ����
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
