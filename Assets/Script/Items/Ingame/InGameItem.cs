using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InGameItem : MonoBehaviour
{
    public string Name;
    private bool Magnet;
    private float speed=3;
    public virtual void Use() { }
    private void Update()
    {
        if (Magnet)
        {
            speed += 0.1f;
            transform.Translate((StageManager.Instance.Player.transform.position - transform.position)* speed * Time.deltaTime);
        }

    }
    private void OnDestroy()
    {
        StageManager.Instance.deleteInGameItemList(gameObject);
    }
    public void MagnetOn()
    {
        Magnet = true;
    }
}
