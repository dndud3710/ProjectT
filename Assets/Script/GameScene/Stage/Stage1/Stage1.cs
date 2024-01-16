using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour,IStages
{
    public GameObject[] Monsters;
    public GameObject MapTile;
    public string StageName;
    public Sprite StageImage;
    public GameObject[] getMonsters()
    {
        return Monsters;
    }
    public GameObject getMapTile()
    {
        return MapTile;
    }
    public string getStageName()
    {
        return StageName;
    }
    public Sprite getStageImage()
    {
        return StageImage;
    }


}
