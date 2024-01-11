using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    public GameObject p;
    private RectTransform r;
    public RectTransform g;
    private int x;
    Vector2 d;
    Vector2 yy;
    int y=0;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<RectTransform>();
    }
    private void Update()
    {
        yy = new Vector2(0,y++);
        // 스크린 좌표를 캔버스의 로컬 좌표로 변환
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(g,yy, Camera.main,out d);

        // 로컬 좌표를 사용하여 오브젝트의 위치 업데이트
        r.Translate(yy*Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("와우");
        }
    }

}
