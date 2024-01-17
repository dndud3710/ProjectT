using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class WinU : MonoBehaviour
{

    public GameObject[] d;
    public TextMeshProUGUI killtext;
    public TextMeshProUGUI goldtext;
    public void Win()
    {
        Time.timeScale = 0;
        foreach (GameObject go in d)
        {
            go.SetActive(true);
        }
    }

    public void setText(int kill, int gold)
    {
        killtext.text = kill.ToString();
        goldtext.text = gold.ToString();
    }
}
