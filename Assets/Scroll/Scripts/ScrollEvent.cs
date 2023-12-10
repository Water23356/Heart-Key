
using System.Collections.Generic;
using System;

public struct ChooseInfo
{
    /// <summary>
    /// 显示文本
    /// </summary>
    public string text;

    /// <summary>
    /// 效果
    /// </summary>
    public string[] effects;
}
/// <summary>
/// 战斗事件
/// </summary>
public class ScrollEvent
{
    /// <summary>
    /// 效果文本
    /// </summary>
    public string[] effects;
    /// <summary>
    /// 选项
    /// </summary>
    public ChooseInfo[] items;
    /// <summary>
    /// 标记接口
    /// </summary>
    public Dictionary<string, object> marks;
}

public static class ScrollEventStore
{
    public const string TEXT = "text";
    public const string DIALOG = "dialog";
    public const string HEART = "heart";//心境值变化
    public const string ITEM = "item";//获取物品
    public const string CHOOSE = "choose";//开启选择
    public const string END_DIALOG = "end_dialog";//终止对话
    public const string FIGHT = "fight";//战斗 0:盖尔 1:陌生人 2:妈妈

    public static ScrollEvent MainEvent_1
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                DIALOG+":……:盖尔",
                DIALOG+":是你啊……:盖尔",
                DIALOG+":把你的黑暗灵魂给我…:盖尔",
                END_DIALOG,
                FIGHT+":0"
            };
            return se;
        }
    }
    public static ScrollEvent MainEvent_2
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                DIALOG+":怎么了:陌生人",
                DIALOG+":小朋友？:陌生人",
                DIALOG+":需要什么帮助吗？:陌生人",
                END_DIALOG,
                FIGHT+":1"
            };
            return se;
        }
    }
    public static ScrollEvent MainEvent_3
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                DIALOG+":小明！小明！:爸爸妈妈",
                DIALOG+":哎呦！我的宝贝孩子，你怎么出家门了，还跑到这里来了，这要是遇到什么危险可怎么办！:妈妈",
                DIALOG+":你这孩子，你的抑郁症这么严重了，为什么还要离开家—:妈妈",
                DIALOG+":孩子他妈！:爸爸",
                END_DIALOG,
                FIGHT+":2"
            };
            return se;
        }
    }
    public static ScrollEvent RandomEvent_1//老虎机 
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                TEXT+":老式老虎机上有一枚没人用的游戏币",
                CHOOSE,
                END_DIALOG,
            };
            se.items = new ChooseInfo[]
            {
                new ChooseInfo()
                {
                    text = "使用",
                    effects = new string[]
                    {
                        ITEM+":零食",
                        HEART+":3:0:2:0:0:-2",
                        TEXT+":获得 零食",
                    }
                },
                new ChooseInfo()
                {
                    text = "自己兜着",
                    effects = new string[]
                    {
                        ITEM+":游戏币",
                        HEART+":3:0:2:0:0:-2",
                        TEXT+":获得 游戏币",
                    }
                },
            };
            return se;
        }
    }
    public static ScrollEvent RandomEvent_2//魔方
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                TEXT+":一个未被还原的魔方，上面的一些磕痕好像反映着原主人的无能狂怒",
                CHOOSE,
                END_DIALOG,
            };
            se.items = new ChooseInfo[]
            {
                new ChooseInfo()
                {
                    text = "尝试还原这个魔方",
                    effects = new string[]
                    {
                        TEXT+":还原成功, 你感到很开心",
                        HEART+":5:0:1:-2:0:0",
                    }
                },
                new ChooseInfo()
                {
                    text = "丢弃",
                    effects = new string[]
                    {
                        HEART+":0:5:-1:-2:-3:0",
                    }
                },
            };
            return se;
        }
    }
    public static ScrollEvent RandomEvent_3//流浪猫
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                TEXT+":一只很想rua的猫猫，你的手蠢蠢欲动",
                CHOOSE,
                END_DIALOG,
            };
            se.items = new ChooseInfo[]
            {
                new ChooseInfo()
                {
                    text = "喂它些吃的",
                    effects = new string[]
                    {
                        TEXT+":被猫猫治愈了",
                        HEART+":3:0:2:2:0:3",
                    }
                },
                new ChooseInfo()
                {
                    text = "抱抱它",
                    effects = new string[]
                    {
                        TEXT+":被猫猫抓伤了",
                        HEART+":-3:0:-2:0:0:-3",
                    }
                },
            };
            return se;
        }
    }
    public static ScrollEvent RandomEvent_4//破易拉罐
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                TEXT+":一个脏兮兮的易拉罐，不知被哪个人随意丢在了路边",
                CHOOSE,
                END_DIALOG,
            };
            se.items = new ChooseInfo[]
            {
                new ChooseInfo()
                {
                    text = "踢走",
                    effects = new string[]
                    {
                        TEXT+":易拉罐在空中飞出了一条优美的弧线，你感到十分解压",
                        HEART+":3:0:0:2:0:0",
                    }
                },
                new ChooseInfo()
                {
                    text = "捡起来",
                    effects = new string[]
                    {
                        TEXT+":可能之后会有什么作用吧，但这易拉罐脏兮兮的你感到些许不适",
                        ITEM+":易拉罐",
                        HEART+":0:0:-2:-2:0:-3",
                        TEXT+":获得 易拉罐",
                    }
                },
            };
            return se;
        }
    }
    public static ScrollEvent RandomEvent_5//被泼脏水
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                TEXT+":今天真是倒霉，店家洗菜的水正好泼到你身上",
                CHOOSE,
                END_DIALOG,
            };
            se.items = new ChooseInfo[]
            {
                new ChooseInfo()
                {
                    text = "与店家理论",
                    effects = new string[]
                    {
                        TEXT+":虽然得到了赔偿，但心里还是很不开心",
                        ITEM+":赔偿",
                        HEART+":3:5:-3:-3:0:0",
                        TEXT+":获得 赔偿",
                    }
                },
                new ChooseInfo()
                {
                    text = "忍气吞声",
                    effects = new string[]
                    {
                        TEXT+":店家过意不去，赔偿了你一件干净的衣服，并郑重道歉，你感到很开心",
                        HEART+":3:0:0:0:0:0",
                    }
                },
            };
            return se;
        }
    }
    public static ScrollEvent RandomEvent_6//玩具盒
    {
        get
        {
            ScrollEvent se = new ScrollEvent();
            se.effects = new string[]
            {
                TEXT+":一个奇奇怪怪的玩具盒，不知道里面有什么",
                CHOOSE,
                END_DIALOG,
            };
            se.items = new ChooseInfo[]
            {
                new ChooseInfo()
                {
                    text = "打开它",
                    effects = new string[]
                    {
                        TEXT+":一个惊吓盒子，你成功被吓到了",
                        HEART+":0:2:2:-2:3:-5",
                    }
                },
                new ChooseInfo()
                {
                    text = "丢出去",
                    effects = new string[]
                    {
                        TEXT+":你成功砸到了其他人，不得不给他道歉，你感到很伤心",
                        HEART+":0:0:-5:0:0:-5",
                    }
                },
            };
            return se;
        }
    }
}