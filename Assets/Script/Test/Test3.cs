using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public GameObject[] tests;

    // Start is called before the first frame update
    void Start()
    {

        foreach (var test in tests)
        {
            test.SendMessage("Augh",3f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
