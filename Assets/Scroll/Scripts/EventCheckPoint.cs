using System.Collections.Generic;
using UnityEngine;

public class EventCheckPoint:MonoBehaviour
{
    private PlayerController player;
    [SerializeField]
    private ScrollSystem system;
    private bool over = false;
    public AutoEventDoor door;//目标事件容器

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (over) return;
        string tag = collision.tag;
        if (tag == GameText.TAG_PLAYER)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log($"接触玩家:{player != null}");
            EnterEvent();
            over = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
    private void EnterEvent()
    {
        float value = Heart.Instance.HeartLevel;
        List<int> numbers = new List<int>();
        if (value > 0)//高
        {
            if (!system.puted[3])
            {
                numbers.Add(3);
            }
            if (!system.puted[4])
            {
                numbers.Add(4);
            }
            if (!system.puted[5])
            {
                numbers.Add(5);
            }
        }
        else//低
        {
            if (!system.puted[6])
            {
                numbers.Add(6);
            }
            if (!system.puted[7])
            {
                numbers.Add(7);
            }
            if (!system.puted[8])
            {
                numbers.Add(8);
            }
        }
        int index = UnityEngine.Random.Range(0, numbers.Count);
        system.puted[numbers[index]] = true;
        Debug.Log($"count:{numbers.Count},index:{index}");
        door.event_index = numbers[index];
        Debug.Log($"随机事件索引:{numbers[index]}");
    }
}