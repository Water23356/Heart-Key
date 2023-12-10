using UnityEngine;

public class FightTest:MonoBehaviour
{
    public FightSystem fightSystem;
    public void Start()
    {
        fightSystem.InitFight(FightEventStore.CHAPTER_3);
    }
} 