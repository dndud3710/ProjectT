using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainChapterSelectInit : MonoBehaviour
{
    [Header("변경되는 UI오브젝트")]
    public TextMeshProUGUI StageName;
    public TextMeshProUGUI ClearName;
    public Image StageImage;
    public PlayerInfo playerinfo;
    private void Start()
    {
        GameManager.Instance.initEvent += Init;
    }
    public void Init()
    {
        
        StageName.text = $"{playerinfo.getCurStage()}. {GameManager.Instance.getStageName(playerinfo.getCurStage())}";
        StageImage.sprite= GameManager.Instance.getStageImage(playerinfo.getCurStage());
        
    }
}
