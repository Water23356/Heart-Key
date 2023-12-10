using UnityEngine;

public class AutoEventDoor:MonoBehaviour
{
    private PlayerController player;
    [SerializeField]
    private ScrollSystem system;
    private bool over = false;
    /// <summary>
    /// 事件索引
    /// </summary>
    public int event_index = 0;
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
        switch (event_index)
        {
            case 0:
                system.IinitEvent(ScrollEventStore.MainEvent_1);
                break;
            case 1:
                system.IinitEvent(ScrollEventStore.MainEvent_2);
                break;
            case 2:
                system.IinitEvent(ScrollEventStore.MainEvent_3);
                break;
            case 3:
                system.IinitEvent(ScrollEventStore.RandomEvent_1);
                break;
            case 4:
                system.IinitEvent(ScrollEventStore.RandomEvent_2);
                break;
            case 5:
                system.IinitEvent(ScrollEventStore.RandomEvent_3);
                break;
            case 6:
                system.IinitEvent(ScrollEventStore.RandomEvent_4);
                break;
            case 7:
                system.IinitEvent(ScrollEventStore.RandomEvent_5);
                break;
            case 8:
                system.IinitEvent(ScrollEventStore.RandomEvent_6);
                break;
        }
    }
}