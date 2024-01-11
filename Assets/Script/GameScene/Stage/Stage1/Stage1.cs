using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour,IStages
{
    public GameObject[] Monsters;

    public GameObject[] getMonsters()
    {
        return Monsters;
    }


}
