using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject p;
    private GameObject g;
    private int a;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aa());
        a = 0;
    }

    IEnumerator aa()
    {
        g = null;
        while (true)
        {
            if (g == null)
            {
                yield return new WaitForSeconds(4f);
                print("»ý¼º!");
                g = Instantiate(p);
                
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
