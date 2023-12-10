using System;
using System.Collections.Generic;
using System.Reflection;
/// <summary>
/// 行动信息
/// </summary>
public struct ActionInfo
{
    /// <summary>
    /// 显示文本
    /// </summary>
    public string text;

    /// <summary>
    /// 效果
    /// </summary>
    public string[] effects;

    /* 效果说明:
     * text: 旁白
     * dialog: 敌方说话
     * next: 下一个仁慈等级
     * mercy: 达成仁慈条件
     * enemy_defence:敌方防御力变化
     */

}

/// <summary>
/// 战斗事件
/// </summary>
public class FightEvent
{
    /*数值变化:
     * 0  1    2     3       4        5
     * 喜 怒 哀/乐 临时喜 临时怒 临时哀/乐
     */

    /// <summary>
    /// 击败心境变化值
    /// </summary>
    public float[] heart_kill;

    /// <summary>
    /// 和平心境变化值
    /// </summary>
    public float[] heart_peace;

    /// <summary>
    /// 战斗结束后获得道具的名称:{击败线,和平线}
    /// </summary>
    public string[] get_item;
    /// <summary>
    /// 敌人名称
    /// </summary>
    public string name_enemy;

    /// <summary>
    /// 敌人生命值
    /// </summary>
    public int health_enemy;

    /// <summary>
    /// 敌人默认生命值
    /// </summary>
    public int def_health_enemy;

    /// <summary>
    /// 敌人防御力
    /// </summary>
    public int defence_enemy;

    /// <summary>
    /// 玩家在不同说服等级下的行动信息[阶段等级][动作编号]
    /// </summary>
    public ActionInfo[][] actions;
    /// <summary>
    /// 敌人的攻击方式
    /// </summary>
    public string[] attacks;

    /// <summary>
    /// 敌人被说服等级
    /// </summary>
    public int mercy_level;
    /// <summary>
    /// 说服的效果文本
    /// </summary>
    public string[] mercy_effects;
    /// <summary>
    /// 击杀的效果文本
    /// </summary>
    public string[] kill_effects;
    /// <summary>
    /// 获取下一次攻击名称
    /// </summary>
    public Func<FightSystem,string> get_attack;
    /// <summary>
    /// 标记接口
    /// </summary>
    public Dictionary<string, object> marks;
}

/// <summary>
/// 战斗事件仓库
/// </summary>
public static class FightEventStore
{
    public const string TEXT = "text";
    public const string DIALOG = "dialog";
    public const string NEXT = "next";
    public const string MERCY = "mercy";
    public const string DEFENCE_ENEMY = "enemy_defence";
    public const string END_DIALOG = "end_dialog";//终止对话
    public const string END_MERCY = "end_mercy";//以仁慈结束战斗
    public const string END_KILL = "end_kill";//以击杀结束战斗
    /// <summary>
    /// 第一章boss战
    /// </summary>
    public static FightEvent CHAPTER_1
    {
        get
        {
            FightEvent fe = new FightEvent();
            fe.name_enemy = "盖尔";
            fe.heart_kill = new float[6] { -5f, 10f, -5f, 10f, 5f, -5f };
            fe.heart_peace = new float[6] { 5f, 0f, 2f, -5f, -5f, 10f };
            fe.get_item = new string[2] { "玩具刀", "二头身玩具" };
            fe.health_enemy = 24;
            fe.def_health_enemy = 24;
            fe.defence_enemy = 0;
            fe.actions = new ActionInfo[6][];
            fe.actions[0] = new ActionInfo[2]
            {
                new ActionInfo
                {
                    text = "质问",
                    effects = new string[4]
                    {
                        TEXT+":你质问着对方的身份",
                        DIALOG+":连我也忘记了吗？",
                        NEXT,
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "逃跑",
                    effects = new string[2]
                    {
                        TEXT+":讽刺的是，双腿此时拒绝移动",
                        END_DIALOG
                    }
                }
            };
            fe.actions[1] = new ActionInfo[3]
            {
                new ActionInfo
                {
                    text = "质问",
                    effects = new string[3]
                    {
                        TEXT+":你质问着对方的身份",
                        TEXT+":对方缄口不言",
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "对峙",
                    effects = new string[6]
                    {
                        TEXT+":你摆出迎战的姿态",
                        TEXT+":对方同样摆出一副熟悉的架势",
                        TEXT+":对方的防御上升了！",
                        DEFENCE_ENEMY+":1",
                        NEXT,
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "恐吓",
                    effects = new string[3]
                    {
                        TEXT+":你威胁对方让你通行",
                        TEXT+":这显然没有作用",
                        END_DIALOG
                    }
                }
            };
            fe.actions[2] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "回忆",
                    effects = new string[3]
                    {
                        DIALOG+":想起来吧，那些我们并肩战斗的日子",
                        NEXT,
                        END_DIALOG
                    }
                },
            };
            fe.actions[3] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "回忆",
                    effects = new string[3]
                    {
                        DIALOG+":直面内心的黑暗，然后将他们击碎",
                        NEXT,
                        END_DIALOG
                    }
                },
            };
            fe.actions[4] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "回忆",
                    effects = new string[4]
                    {
                        TEXT+":记忆如潮水般涌来，你记起来了眼前这位不该出现在现实世界的老友盖尔，儿时崇拜的NPC，你们曾并肩作战，击败无数劲敌",
                        TEXT+":即便此时此景，忆起光辉往昔，温暖的能量充盈内心",
                        MERCY,
                        END_DIALOG
                    }
                },
            };
            fe.actions[5] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "回忆",
                    effects = new string[2]
                    {
                        DIALOG+":是时候打声招呼了",
                        END_DIALOG
                    }
                }
            };
            fe.attacks = new string[2] 
            {
                "肃穆",
                "月弧",
            };
            fe.mercy_effects = new string[3]
            {
                DIALOG+":久违了，老友",
                END_DIALOG,
                END_MERCY
            };
            fe.kill_effects = new string[4]
            {
                DIALOG+":吾友……切莫堕入黑暗……",
                TEXT+":战胜了奇怪的敌人，带来的懊恼却大于喜悦",
                END_DIALOG,
                END_KILL,
            };
            fe.get_attack = RandomAttack;
            fe.marks = new Dictionary<string, object>();
            return fe;
        }
    }
    public static FightEvent CHAPTER_2
    {
        get
        {
            FightEvent fe = new FightEvent();
            fe.name_enemy = "陌生人";
            fe.heart_kill = new float[6] { 5f, 0f, 3f, -5f,0f, 10f };
            fe.heart_peace = new float[6] { 0f, 5f, -5f, -5f, -3f, 5f };
            fe.get_item = new string[2] { "糖果", string.Empty };

            fe.health_enemy = 36;
            fe.def_health_enemy = 36;
            fe.defence_enemy = 0;
            fe.actions = new ActionInfo[5][];
            fe.actions[0] = new ActionInfo[3]
            {
                new ActionInfo
                {
                    text = "威胁",
                    effects = new string[4]
                    {
                        TEXT+":你试图让它不要靠近，可收效甚微",
                        DIALOG+":怎么啦，小朋友？是遇到什么困难了吗",
                        NEXT,
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "逃跑",
                    effects = new string[2]
                    {
                        TEXT+":你试图逃离，可你酸软的双腿不允许你那样做",
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "恐吓",
                    effects = new string[3]
                    {
                        TEXT+":你试图让自己的表情变得凶一点",
                        TEXT+":但在对方眼里只觉得可爱",
                        END_DIALOG
                    }
                }
            };
            fe.actions[1] = new ActionInfo[2]
            {
                new ActionInfo
                {
                    text = "威胁",
                    effects = new string[4]
                    {
                        TEXT+":你继续阻止它的靠近",
                        DIALOG+":有什么困难可以和姐姐说哦",
                        NEXT,
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "对峙",
                    effects = new string[3]
                    {
                        TEXT+":你想要告诉它不需要帮助",
                        TEXT+":但并没有什么用",
                        END_DIALOG,
                    }
                },
            };
            fe.actions[2] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "对峙",
                    effects = new string[4]
                    {
                        TEXT+":你努力说服它你没有什么事",
                        DIALOG+":小孩子不可以闹脾气哦，来姐姐这里",
                        NEXT,
                        END_DIALOG
                    }
                },
            };
            fe.actions[3] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "威胁",
                    effects = new string[3]
                    {
                        TEXT+":你发现它的那只可怖的手已经要触碰到你的肩膀了",
                        NEXT,
                        END_DIALOG
                    }
                },
            };
            fe.actions[4] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "逃跑",
                    effects = new string[3]
                    {
                        TEXT+":你最终精神崩溃，逃离了这里",
                        END_DIALOG,
                        END_MERCY
                    }
                },
            };
            fe.attacks = new string[3]
            {
                "拥抱",
                "关怀",
                "善意接近",
            };
            fe.mercy_effects = new string[3]
            {
                DIALOG+":不要害怕哟",
                END_DIALOG,
                END_MERCY
            };
            fe.kill_effects = new string[3]
            {
                DIALOG+":怎，怎么了，小朋友",
                TEXT+":见到头套里是名温柔的大姐姐，你感到非常惊讶",
                END_KILL,
            };
            fe.get_attack = GetAttack_2;
            fe.marks = new Dictionary<string, object>();
            return fe;
        }
    }
    public static FightEvent CHAPTER_3
    {
        get
        {
            FightEvent fe = new FightEvent();
            fe.name_enemy = "妈妈";
            fe.heart_kill = new float[6] { -5f, 10f, 2f, 5f, -10f, -2f };//待修改
            fe.heart_peace = new float[6] { 8f, 5f, -2f, 5f, -5f, 2f };//待修改
            fe.get_item = new string[2] { "糖果", string.Empty };//待修改

            fe.health_enemy = 36;
            fe.def_health_enemy = 36;
            fe.defence_enemy = 1;
            fe.actions = new ActionInfo[6][];
            fe.actions[0] = new ActionInfo[2]
            {
                new ActionInfo
                {
                    text = "对峙",
                    effects = new string[4]
                    {
                        TEXT+":你和母亲强调你没有抑郁症",
                        DIALOG+":好什么好，你这几天都快把我愁死了",
                        NEXT,
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "说服",
                    effects = new string[2]
                    {
                        TEXT+":此时偏执的母亲听不进去任何话语",
                        END_DIALOG
                    }
                },
            };
            fe.actions[1] = new ActionInfo[2]
            {
                new ActionInfo
                {
                    text = "对峙",
                    effects = new string[4]
                    {
                        TEXT+":你继续强调自己没有病",
                        DIALOG+":没事的孩子，这里太危险，和我回家好吗？",
                        NEXT,
                        END_DIALOG
                    }
                },
                new ActionInfo
                {
                    text = "逃跑",
                    effects = new string[2]
                    {
                        TEXT+":你本想逃离这里，但看到爸爸妈妈担心的眼神又停下了脚步",
                        END_DIALOG,
                    }
                },
            };
            fe.actions[2] = new ActionInfo[3]
            {
                new ActionInfo
                {
                    text = "对峙",
                    effects = new string[4]
                    {
                        TEXT+":你想让母亲说清楚你哪里有抑郁症",
                        DIALOG+":听话，你这孩子病又犯了是不是！？",
                        NEXT,
                        END_DIALOG
                    }
                },
                 new ActionInfo
                {
                    text = "质问",
                    effects = new string[3]
                    {
                        TEXT+":你质问母亲她有没有好好关心过你",
                        TEXT+":母亲并不想回答你",
                        END_DIALOG
                    }
                },
                  new ActionInfo
                {
                    text = "说服",
                    effects = new string[2]
                    {
                        TEXT+":你的说服效果甚微",
                        END_DIALOG
                    }
                },
            };
            fe.actions[3] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "对峙",
                    effects = new string[4]
                    {
                        TEXT+":你继续强调自己很正常，并见到了老朋友",
                        DIALOG+":哪个老朋友？",
                        NEXT,
                        END_DIALOG
                    }
                },
            };
            fe.actions[4] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "说服",
                    effects = new string[3]
                    {
                        TEXT+":你亮出了玩具模型",
                        NEXT,
                        END_DIALOG,
                    }
                },
            };
            fe.actions[5] = new ActionInfo[1]
            {
                new ActionInfo
                {
                    text = "说服",
                    effects = new string[4]
                    {
                        DIALOG+":你...唉，算了",
                        TEXT+":此时爸爸出现，带着母亲与你回了家",
                        MERCY,
                        END_DIALOG,
                    }
                },
            };
            fe.attacks = new string[3]
            {
                "唠叨",
                "牵手",
                "强制爱",
            };
            fe.mercy_effects = new string[3]
            {
                DIALOG+":孩子，和我回家吧",
                END_DIALOG,
                END_MERCY
            };
            fe.kill_effects = new string[4]
            {
                TEXT+":你对妈妈说了段狠话",
                DIALOG+":你，你这孩子...唉",
                TEXT+":你击败了母亲，可并不感觉开心",
                END_KILL,
            };
            fe.get_attack = RandomAttack;
            fe.marks = new Dictionary<string, object>();
            return fe;
        }
    }
    public static FightEvent CHAPTER_4
    {
        get
        {
            return null;
        }
    }

    public static string RandomAttack(FightSystem system)
    {
        int index = (int)ER.RandomNumber.RangeF(0, system.eventInfo.attacks.Length);
        return system.eventInfo.attacks[index];
    }
    public static string GetAttack_2(FightSystem system)
    {
        if(system.HP <= 5 || system.eventInfo.marks.ContainsKey("mercy"))
        {
            system.eventInfo.marks["mercy"] = true;
            return "善意接近";
        }
        else
        {
            int index = (int)ER.RandomNumber.RangeF(0, system.eventInfo.attacks.Length);
            return system.eventInfo.attacks[index];
        }
    }
    public static string RandomLast(FightSystem system)
    {
        float number = ER.RandomNumber.RangeF();
        if(number < 0.2f)
        {
            return system.eventInfo.attacks[2];
        }
        else if(number < 0.6f)
        {
            return system.eventInfo.attacks[1];
        }
        else
        {
            return system.eventInfo.attacks[0];
        }
    }
}