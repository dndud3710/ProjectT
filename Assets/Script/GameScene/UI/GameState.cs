using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    
    public Slider EXPBar;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI getGoldText;
    public TextMeshProUGUI getKillText;
    public TextMeshProUGUI getTimeText;
    private void Start()
    {
        
    }
    private void Update()
    {
       //나중에 초가 바뀌면 바꾸는걸로 정리해야함
        getTimeText.text = $"{StageManager.Instance.getTime().Elapsed.ToString(@"mm\:ss").Replace(":", " : ")}";
    }

    public void setKillText(int killnum)
    {
        getKillText.text = $"{killnum}";
    }
    public void setMoneyText(int Money)
    {
        getGoldText.text = $"{Money}";
    }
    public void setLevelText(int level_)
    {
        LevelText.text = $"{level_}";
    }
    //플레이어가 렙업 했을 시에 expbar를 변경해준다
    //t는 레벨업시에 true로 전달
    public void setExpBar(bool t)
    {
        if(t)
            EXPBar.maxValue = StageManager.Instance.playerScript.maxExp[StageManager.Instance.playerScript.Level - 1];
        EXPBar.value = StageManager.Instance.playerScript.curExp;
    }

}
