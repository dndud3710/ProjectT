using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    /// <summary>
    /// 메인 씬에서 제일 위쪽에 위치한 UI : 플레이어 정보 UI
    /// </summary>

    public Image PlayerIcon;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI PlayerName;
    public Slider PlayerEXPBar;
    public TextMeshProUGUI PowerText;
    public TextMeshProUGUI CashText;
    public TextMeshProUGUI MoneyText;

    public PlayerInfo playerinfo;

    private void Start()
    {
    }

    private void Init()
    {
        PlayerName.text = playerinfo.getName();
        LevelText.text = playerinfo.getLevel().ToString();
        PlayerEXPBar.maxValue = playerinfo.getCurMaxExp();
        PlayerEXPBar.value = playerinfo.getEXP();
        PowerText.text = $"{playerinfo.getCurCoin()} / {playerinfo.getMaxCoin()}";
        MoneyText.text = $"{playerinfo.getMoney()}";
    }

    public void Subscribe()
    {
        GameManager.Instance.initEvent += Init;
    }
}
