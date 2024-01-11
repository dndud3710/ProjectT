using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform JoyStickTF;
    public GameObject StartPointOB;
    public GameObject CircleColOB;
    public Transform Controller;


    private Transform StartPoint;
    private CircleCollider2D Circlecol;
    private Player player;

    Vector3 cameraPos;
    Vector3 v;
    float maxDistance;

    bool ClickOn;

    private void Start()
    {
        StartPoint = StartPointOB.GetComponent<Transform>();
        Circlecol = CircleColOB.GetComponent<CircleCollider2D>();
        player = StageManager.Instance.Player.GetComponent<Player>();
        

        ClickOn = false;
        maxDistance = 100; 
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetCameraPos();
        ClickOn = true;
        JoyStickTF.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetCameraPos();
        if (Controller.localPosition.magnitude > maxDistance)
        {
            Controller.localPosition = Controller.localPosition.normalized * maxDistance;
        }
        player.Move(Controller.localPosition.normalized);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        player.Move(Vector2.zero);
        Controller.localPosition = new Vector2(0, 0);
        ClickOn = false;
        JoyStickTF.gameObject.SetActive(false);
    }

    void GetCameraPos()
    {
        cameraPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        v = Camera.main.ScreenToWorldPoint(cameraPos);
        if (ClickOn)
        {
            v.z = Controller.position.z;
            Controller.position = v;
        }
        else
        {
            v.z = JoyStickTF.position.z;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetCameraPos();
            JoyStickTF.position = v;
        }
    }
}

