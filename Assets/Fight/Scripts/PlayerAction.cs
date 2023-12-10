using ER.Control;
using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

/// <summary>
/// 玩家行动模块
/// </summary>
public class PlayerAction : MonoControlPanel
{
    [SerializeField]
    private FightSystem system;
    [SerializeField]
    private TMP_Text[] texts;//选项文本
    [SerializeField]
    private int index;//当前索引

    private int Index
    {
        get => index;
        set
        {
            index = value;
            if (index >= 0)
            {
                system.cursor.AimPos = new Vector2(-80 + (index % 2) * 300, 50 - 75 * (int)(index / 2));
            }
            else
            {
                system.cursor.AimPos = new Vector2(190, -210);
            }

            
        }
    }

    [SerializeField]
    private Animator animator;//自身动画机

    private List<ActionInfo> actions = new List<ActionInfo>();//行动消息
    private Action<bool> callback;//回调函数, bool 表示是否成功执行动作, 跳出玩家回合
    private bool skip = false;//关闭面板时是否跳过玩家回合

    /// <summary>
    /// 设置行动列表
    /// </summary>
    /// <param name="acts"></param>
    public void SetActions(ActionInfo[] acts)
    {
        actions.Clear();
        if(acts != null)
        {
            foreach (ActionInfo act in acts)
            {
                actions.Add(act);
            }
        }
        for (int i = 0; i < texts.Length; i++)
        {
            if (i < actions.Count)
            {
                texts[i].gameObject.SetActive(true);
                texts[i].text = actions[i].text;
            }
            else
            {
                texts[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenPanel(Action<bool> cb = null)
    {
        gameObject.SetActive(true);
        callback = cb;
        skip = false;
        Index = 0;
        animator.SetTrigger("open");
        ControlManager.Instance.RegisterPower(this);
    }

    public void ClosePanel()
    {
        animator.SetTrigger("close");
        ControlManager.Instance.UnregisterPower(this);
        system.OptionIndex = system.OptionIndex;
    }
    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(IsEnable)
        {

            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("Left"))
                {
                    Index = Math.Max(0, Index - 1);
                    system.soundEffects.PlayOneShot(system.sounds[0]);
                }
                if (Input.GetButtonDown("Up"))
                {
                    Index = Math.Max(0, Index - 2);
                    system.soundEffects.PlayOneShot(system.sounds[0]);
                }
                if (Input.GetButtonDown("Right"))
                {
                    Index = Math.Min(actions.Count - 1, Index + 1);
                    system.soundEffects.PlayOneShot(system.sounds[0]);
                }
                if (Input.GetButtonDown("Down"))
                {
                    Index = Math.Min(actions.Count - 1, Index + 2);
                    system.soundEffects.PlayOneShot(system.sounds[0]);
                }
                if (Input.GetButtonDown("Submit"))
                {
                    if (Index < 0 || Index >= actions.Count)//返回选项
                    {
                        skip = true;
                        ClosePanel();
                        callback?.Invoke(false);
                    }
                    else//执行行动
                    {
                        string[] effects = actions[Index].effects;
                        foreach (string ef in effects)
                        {
                            wait_list.Add(ef.Split(':'));
                            Debug.Log(ef);
                        }
                        ActNext();
                        ClosePanel();
                    }
                }
                if (Input.GetButtonDown("Cancel"))
                {
                    Index = -1;
                }
            }
        }
    }
    private List<string[]> wait_list = new List<string[]>();//等待执行的效果队列
    private void ActNext()//执行接下来的行动效果
    {
        bool loop = false;
        do
        {
            if(wait_list.Count== 0)
            {
                callback?.Invoke(true);
                break;
            }
            string[] cmd = wait_list[0];//获取指令串
            wait_list.RemoveAt(0);
            switch (cmd[0])//解析指令头
            {
                case FightEventStore.TEXT:
                    system.OpenDialogBox();
                    system.SetDialog(cmd[1], null, ActNext);
                    loop = false;
                    break;
                case FightEventStore.DIALOG:
                    system.OpenDialogBox();
                    system.SetDialog(cmd[1], system.NameEnemy, ActNext);
                    loop = false;
                    break;
                case FightEventStore.DEFENCE_ENEMY:
                    int append = int.Parse(cmd[1]);
                    system.DefenceEnemy += append;
                    loop = true;
                    break;
                case FightEventStore.NEXT:
                    system.MercyLevel++;
                    loop = true;
                    break;
                case FightEventStore.MERCY:
                    system.Mercy = true;
                    system.MercyLevel++;
                    loop = true;
                    break;
                case FightEventStore.END_DIALOG:
                    system.CloseDialogBox();
                    loop = true;
                    break;
                case FightEventStore.END_MERCY://结束战斗
                    Heart.Instance.SetHeart(system.eventInfo.heart_peace);
                    system.ClosePanel();
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
}