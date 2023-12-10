using UnityEngine;

public class FightDoor:MonoBehaviour
{
    private PlayerController player;
    [SerializeField]
    private FightSystem fightSystem;
    /// <summary>
    /// 战斗索引
    /// </summary>
    public int fight_index = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == GameText.TAG_PLAYER)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log($"接触玩家:{player != null}");
            player.interactive += Fight; ;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == GameText.TAG_PLAYER)
        {
            player.interactive -= Fight;
        }
    }
    private void Fight()
    {
        switch(fight_index)
        {
            case 0:
                fightSystem.InitFight(FightEventStore.CHAPTER_1);
                break;
            case 1:
                fightSystem.InitFight(FightEventStore.CHAPTER_2);
                break;
            case 2:
                fightSystem.InitFight(FightEventStore.CHAPTER_3);
                break;
        }
    }
}