
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{
    /// <summary>
    /// �÷��̾��� ���ݷ�, ü��, ��ų�߰�ȿ��, ��, ���� �� ���ξ����� �ʿ��� �÷��̾� ����
    /// �� �������� �ΰ������� �� �� stage�� �ʿ��� ���������� gamemanager�� ���� ����
    /// </summary>

    private int[] maxEXP = { 100, 200, 300, 400, 500 };

    private int damage;
    private int Maxhealth;

    private EquipItem[] equips;

    private void Start()
    {
        GameManager.Instance.getItem("��");
        GameManager.Instance.getItem("��");
    }

    public void setEquipItem(EquipItem e_)
    {
        if (equips[(int)e_.type] == null)
        {
            equips[(int)e_.type] = e_;
            GameManager.Instance.deleteItem(e_.gameObject);
        }
        else
        {
            //�̹� �����ϰ��մ� �������� �κ��丮�� �߰��ϰ� �ٲ㳦
            GameManager.Instance.getItem(equips[(int)e_.type].ItemName);
            equips[(int)e_.type] = e_;
        }
        //������ �ֽ�ȭ
        
    }

   public   void StartGameCoin()
    {
        deCreaseCurCoin(5);
    }

    //Stage
    public int getCurStage()
    {
        return PlayerPrefs.GetInt("Stage");
    }
    public void setCurStage(int stage)
    {
        PlayerPrefs.SetInt("Stage", stage);
    }
    //NAME
    public string getName()
    {
        return PlayerPrefs.GetString("Name");
    }
    public void setName(string name)
    {
        PlayerPrefs.SetString("Name", name);
        PlayerPrefs.Save();
    }

    //Level
    public int getLevel()
    {
        return PlayerPrefs.GetInt("Level");
    }
    public void setLevel(int Level)
    {
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.Save();
    }

    //Money
    public int getMoney()
    {
        return PlayerPrefs.GetInt("Money");
    }
    public void setMoney(int Money)
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();
    }
    public void inCreaseMoney(int Money)
    {
        int m = PlayerPrefs.GetInt("Money");
        m += Money;
        PlayerPrefs.SetInt("Money", m);
        PlayerPrefs.Save();
    }
    public void deCreaseMoney(int Money)
    {
        int m = PlayerPrefs.GetInt("Money");
        m -= Money;
        PlayerPrefs.SetInt("Money", m);
        PlayerPrefs.Save();
    }

    //EXP
    public int getEXP()
    {
        return PlayerPrefs.GetInt("PlayerCurEXP");
    }
    public int getCurMaxExp()
    {
        int l = PlayerPrefs.GetInt("Level");
        return maxEXP[l - 1];
    }
    public void inCreaseCurEXP(int exp)
    {
        int curEXP = PlayerPrefs.GetInt("PlayerCurEXP");
        int lev = PlayerPrefs.GetInt("Level");
        curEXP += exp;
        if (maxEXP[lev - 1] >= curEXP)
        {
            curEXP -= maxEXP[lev - 1];
            lev++;
            PlayerPrefs.SetInt("Level", lev);
            InitcurEXP(curEXP);
            PlayerPrefs.Save();
        }
    }
    public void InitcurEXP(int curexp)
    {
        PlayerPrefs.SetInt("PlayerCurEXP", curexp);
    }



    //Ȱ����
    public int getMaxCoin()
    {
        return PlayerPrefs.GetInt("PlayerMaxCoin");
    }
    public int getCurCoin()
    {
        return PlayerPrefs.GetInt("PlayerCurCoin");
    }
    public void deCreaseCurCoin(int coin)
    {
        int curPlayCoin = PlayerPrefs.GetInt("PlayerCurCoin");
        curPlayCoin -= coin;
        PlayerPrefs.SetInt("PlayerCurCoin", curPlayCoin);
        PlayerPrefs.Save();
    }
    public void inCreaseMaxCoin(int coin)
    {
        int maxPlayCoin = PlayerPrefs.GetInt("PlayerMaxCoin");
        maxPlayCoin += coin;
        PlayerPrefs.SetInt("PlayerMaxCoin", maxPlayCoin);
        PlayerPrefs.Save();
    }
}
