using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ParticleSystem p1;
    public ParticleSystem p2;

    // Start is called before the first frame update
    void Start()
    {
        p1.Play();
        p2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * 3);
    }   
}
