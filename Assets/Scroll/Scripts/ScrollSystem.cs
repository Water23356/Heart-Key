using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ScrollSystem:MonoBehaviour
{
    [SerializeField]
    private DialogBoxMain dialogBox;//对话器
    private List<string[]> wait_list = new List<string[]>();//等待执行的效果队列
    private ScrollEvent eventInfo;
    [SerializeField]
    private ChoosePanel choosePanel;//选择栏

    public bool[] puted;//已经放置的随机事件标记

    public FightSystem fight;

    public void Awake()
    {
        puted = new bool[9];
        for(int i=0;i<puted.Length;i++)
        {
            puted[i] = false;
        }
    }

    public void IinitEvent(ScrollEvent info)//加载交互事件
    {
        wait_list.Clear();
        eventInfo = info;
        string[] effects = eventInfo.effects;
        foreach (string ef in effects)
        {
            wait_list.Add(ef.Split(':'));
            Debug.Log(ef);
        }
        ActNext();
    }

    /// <summary>
    /// 打开对话器
    /// </summary>
    public void OpenDialogBox()
    {
        if (!dialogBox.Opened)
            dialogBox.OpenPanel();
    }

    /// <summary>
    /// 关闭对话器
    /// </summary>
    public void CloseDialogBox()
    {
        if (dialogBox.Opened)
            dialogBox.ClosePanel();
    }

    /// <summary>
    /// 设置对话
    /// </summary>
    /// <param name="text"></param>
    /// <param name="callback"></param>
    public void SetDialog(string text, string name, Action callback = null)
    {
        if (name == null)
        {
            dialogBox.SetSpriteDisplay(false);
        }
        else
        {
            dialogBox.NameDisplay = true;
            dialogBox.SetNameText(name);
        }
        dialogBox.SetText(text, callback);
    }

    private void ActNext()//执行接下来的行动效果
    {
        bool loop = false;
        Debug.Log("执行指令");
        do
        {
            if (wait_list.Count == 0)
            {
                break;
            }
            Debug.Log("当前指令:" + wait_list[0][0] + " ");
            string[] cmd = wait_list[0];//获取指令串
            wait_list.RemoveAt(0);
            switch (cmd[0])//解析指令头
            {
                case ScrollEventStore.TEXT:
                    OpenDialogBox();
                    SetDialog(cmd[1], null, ActNext);
                    Debug.Log("输出消息");
                    loop = false;
                    break;

                case ScrollEventStore.DIALOG:
                    OpenDialogBox();
                    SetDialog(cmd[1], cmd[2], ActNext);
                    loop = false;
                    break;
                case ScrollEventStore.END_DIALOG:
                    CloseDialogBox();
                    loop = true;
                    break;

                case ScrollEventStore.HEART:
                    float[] change = new float[6];
                    change[0] = float.Parse(cmd[1]);
                    change[1] = float.Parse(cmd[2]);
                    change[2] = float.Parse(cmd[3]);
                    change[3] = float.Parse(cmd[4]);
                    change[4] = float.Parse(cmd[5]);
                    change[5] = float.Parse(cmd[6]);
                    Heart.Instance.SetHeart(change);
                    loop = true;
                    break;

                case ScrollEventStore.ITEM:
                    loop = true;
                    break;

                case ScrollEventStore.CHOOSE:
                    choosePanel.OpenPanel(WaitChoose);
                    choosePanel.SetText(eventInfo.items[0].text, eventInfo.items[1].text);//设置选项文本
                    loop = false;
                    break;
                case ScrollEventStore.FIGHT:
                    int index = int.Parse(cmd[1]);
                    switch(index)
                    {
                        case 0:
                            fight.InitFight(FightEventStore.CHAPTER_1);
                            break;
                        case 1:
                            fight.InitFight(FightEventStore.CHAPTER_2);
                            break;
                        case 2:
                            fight.InitFight(FightEventStore.CHAPTER_3);
                            break;
                    }
                    loop = false;
                    break;

                default:
                    Debug.LogError($"未知行动指令:{cmd[0]}");
                    loop = false;
                    break;
            }
        }
        while (loop);
    }

    private void WaitChoose(int index)//等待选项响应
    {
        Debug.Log("选项完成响应 :"+index);
        string[] effects;
        switch (index)
        {
            case 0:
                effects = eventInfo.items[0].effects;
                break;
            case 1:
                effects = eventInfo.items[1].effects;
                break;
            default:
                effects = eventInfo.items[0].effects;
                break;
        }
        List<string[]> wcds = new List<string[]>();
        foreach (string ef in effects)
        {
            wcds.Add(ef.Split(':'));
            Debug.Log(ef);
        }
        foreach (string[] ef in wait_list)
        {
            wcds.Add(ef);
        }
        wait_list = wcds;//添加新的指令
        ActNext();
    }
}