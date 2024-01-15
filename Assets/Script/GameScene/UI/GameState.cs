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
       //���߿� �ʰ� �ٲ�� �ٲٴ°ɷ� �����ؾ���
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
    //�÷��̾ ���� ���� �ÿ� expbar�� �������ش�
    //t�� �������ÿ� true�� ����
    public void setExpBar(bool t)
    {
        if(t)
            EXPBar.maxValue = StageManager.Instance.playerScript.maxExp[StageManager.Instance.playerScript.Level - 1];
        EXPBar.value = StageManager.Instance.playerScript.curExp;
    }

}
