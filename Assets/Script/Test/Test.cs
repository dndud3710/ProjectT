using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    public Transform a;
    public Transform b;
    Vector2 v;
    float xx=0f, yy=0f;
    bool d = false;
    Vector2 veve;
    // Start is called before the first frame update
    void Start()
    {
         v = transform.position;
        veve = transform.position - b.position;
        veve+=v;
    }
    private void FixedUpdate()
    {

        if (!d)
        {
            print(veve);
            float xxx = Mathf.SmoothDamp(transform.position.x,veve.x, ref xx, 0.4f);
            float yyy = Mathf.SmoothDamp(transform.position.y, veve.y, ref yy, 0.4f);
            transform.position = new Vector2(xxx, yyy);
        }
        else
        {
            float xxx = Mathf.SmoothDamp(transform.position.x, v.x, ref xx, 0.4f);
            float yyy = Mathf.SmoothDamp(transform.position.y, v.y, ref yy, 0.4f);
            transform.position = new Vector2(xxx, yyy);
        }
        if(Mathf.Abs( veve.x - transform.position.x) <= 0.11f)
        {
            d = true;
            
        }
    }
    private void Update()
    {
        // Vector2 vec = b.position - transform.position;
        //float time = Mathf.PingPong(Time.time * 1f, 1);
        //Vector3 position = Vector3.Lerp(v, b.position, time);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
