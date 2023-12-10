// Ignore Spelling: Dialog
#define TESTs

using ER.Control;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Dir
{
    Up,
    Down,
    Left,
    Right
}

/// <summary>
/// 战斗系统
/// </summary>
public class FightSystem : MonoControlPanel
{
    private string enemy_name;//敌人名称
    private int def_health_enemy;//敌人默认生命值
    [SerializeField]
    private int health_enemy;//敌人生命值
    private int defence_enemy;//敌人防御力

    private string player_name;//玩家名称
    [SerializeField]
    private int health_player;//玩家当前生命值
    private int def_health_player;//玩家默认生命值
    [SerializeField]
    private int defence_player;//玩家防御力
    [SerializeField]
    private int power;//玩家火力
    private int graze;//擦弹数
    private readonly int[] graze_level = { 3, 6, 10, 15, 20, 30, 40, 50, 65, 80, 100 };
    private readonly int[] graze_hp = { 1, 2, 2, 3, 3, 3, 4, 4, 5, 5, 5, 5 };
    private int graze_level_free = 130;

    public AudioClip[] bgms;
    public AudioClip[] sounds;

    public AudioSource BGM;
    public AudioSource soundEffects;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int graze_index;//当前擦弹等级

    private int mercy_level;//当前说服等级

    [SerializeField]
    private int option_index;//选项 索引

    public int OptionIndex
    {
        get => option_index;
        set
        {
            option_index = value;
            cursor.AimPos = new Vector3(option_index*340-420,-430,0);
        }
    }

    private bool mercy;//是否可说服
    public FightEvent eventInfo;//事件信息
    [SerializeField]
    private Image EnemyImage;

    [SerializeField]
    private GameObject RestartPanel;//重开

    [SerializeField]
    private TMP_Text Name_Text;

    [SerializeField]
    private HPBar HPBar;

    [SerializeField]
    private ER.UI.ValueImageBar PowerBar;

    [SerializeField]
    private ER.UI.ValueImageBar HPEnemyBar;

    [SerializeField]
    private TMP_Text Graze_Text;

    [SerializeField]
    private PlayerAttack AttackPanel;

    [SerializeField]
    private DialogBox dialogBox;

    [SerializeField]
    private EnemyAttack EnemyAttackPanel;

    [SerializeField]
    private PlayerAction ActionPanel;

    [Header("选项")]
    public CursorMove cursor;
    [SerializeField]
    private Image BTAttack;

    [SerializeField]
    private Image BTAction;

    [SerializeField]
    private Image BTItem;

    [SerializeField]
    private Image BTMercy;

    [Header("精灵图")]
    [SerializeField]
    private Sprite boss_1;
    [SerializeField]
    private Sprite boss_2;
    [SerializeField]
    private Sprite boss_3;

    /// <summary>
    /// 敌人名称
    /// </summary>
    public string NameEnemy => enemy_name;

    /// <summary>
    /// 敌人防御力
    /// </summary>
    public int DefenceEnemy
    {
        get => defence_enemy;
        set => defence_enemy = value;
    }

    /// <summary>
    /// 玩家名称
    /// </summary>
    public string Name
    {
        get => player_name;
        set
        {
            player_name = value;
            Name_Text.text = player_name;
        }
    }

    /// <summary>
    /// 玩家生命值
    /// </summary>
    public int HP
    {
        get => health_player;
        set
        {
            if(value < health_player)
            {
                soundEffects.PlayOneShot(sounds[2]);
            }
            health_player = value;
            if (health_player > def_health_player * 2)//防止护盾溢出
            {
                health_player = def_health_player * 2;
            }
            HPBar.Text = $"{health_player}/{def_health_player}";
            if (health_player > def_health_player)//溢出部分为护盾
            {
                HPBar.Value = 1f;
                HPBar.Value2 = (float)(health_player - def_health_player) / def_health_player;//溢出护盾的显示
            }
            else
            {
                HPBar.Value = (float)health_player / def_health_player;
                HPBar.Value2 = 0f;
            }
        }
    }

    /// <summary>
    /// 玩家火力
    /// </summary>
    public int Power
    {
        get => power;
        set
        {
            power = value;
            if (power > 4)
            {
                power = 4;
            }
            PowerBar.Value = (int)power / 4f;
            PowerBar.Text = $"{power}/4";
        }
    }

    /// <summary>
    /// 敌人生命值
    /// </summary>
    public int HPEnemy
    {
        get => health_enemy;
        set
        {
            health_enemy = value;
            HPEnemyBar.Value = (float)health_enemy / def_health_enemy;
            HPEnemyBar.Text = $"{health_enemy} / {def_health_enemy}";
        }
    }

    /// <summary>
    /// 擦弹值
    /// </summary>
    public int Graze
    {
        get => graze;
        set
        {
            graze = value;
            Graze_Text.text = $"Graze:{graze}";
            if (graze_index < graze_level.Length)
            {
                Debug.Log($"当前擦弹等级:{graze_index},当前:{graze}, 需求:{graze_level[graze_index]}, 恢复:{graze_hp[graze_index]}");
                if (graze > graze_level[graze_index])//达到擦弹等级升级线
                {
                    Debug.Log($"恢复 {graze_hp[graze_index]} HP");
                    HP += graze_hp[graze_index];
                    graze_index++;
                }
            }
            else
            {
                if (graze > graze_level_free)
                {
                    HP += graze_hp[graze_index];
                    graze_level_free += 30;
                }
            }
        }
    }

    /// <summary>
    /// 仁慈等级
    /// </summary>
    public int MercyLevel
    {
        get => mercy_level;
        set => mercy_level = value;
    }

    /// <summary>
    /// 是否可说服对方
    /// </summary>
    public bool Mercy
    {
        get => mercy;
        set
        {
            mercy = value;
            if(mercy)
            {
                BTMercy.color = Color.green;
            }
            else
            {
                BTMercy.color = Color.gray;
            }
        }
    }

    private bool restart_option = false;//重开选项状态

    public void Restart()
    {
        RestartPanel.SetActive(false);
        restart_option = false;
        InitFight(eventInfo);
    }

    [ContextMenu("初始化")]
    public void InitFight()
    {
        /*等待完善系统
        health_player = (int)(Heart.Instance.Joy/10);
        power = (int)Heart.Instance.Anger;
        */
        graze = 0;
        graze_index = 0;

        //测试使用
        def_health_enemy = 100;
        def_health_player = 20;
        HPEnemy = 100;
        HP = 20;
        power = 1;
    }

    public void InitFight(FightEvent fe)
    {
        Heart.Instance.TimerActive = false;
        int joy = (int)Heart.Instance.Joy;
        int anger = (int)Heart.Instance.Anger;
        int saha = (int)Heart.Instance.SaHa;
        def_health_enemy = fe.def_health_enemy;
        HPEnemy = fe.health_enemy;
        defence_enemy = fe.defence_enemy;
        enemy_name = fe.name_enemy;
        switch(enemy_name)
        {
            case "盖尔":
                EnemyImage.sprite = boss_1;
                BGM.clip = bgms[0];
                BGM.Play();
                break;
            case "陌生人":
                EnemyImage.sprite = boss_2;
                BGM.clip = bgms[1];
                BGM.Play();
                break;
            case "妈妈":
                EnemyImage.sprite = boss_3;
                BGM.clip = bgms[2];
                BGM.Play();
                break;
        }
        eventInfo = fe;
        if (joy < 10)
        {
            def_health_player = 20;
            HP = 20;
        }
        else if (joy < 20)
        {
            def_health_player = 40;
            HP = 40;
        }
        else if (joy < 30)
        {
            def_health_player = 60;
            HP = 60;
        }
        else
        {
            def_health_player = 80;
            HP = 80;
        }
        if (anger < 10)
        {
            Power = 1;
        }
        else if (anger < 20)
        {
            Power = 2;
        }
        else if (anger < 30)
        {
            Power = 3;
        }
        else
        {
            Power = 4;
        }

        if(saha >= 10)
        {
            defence_player = 1;
        }
        else if(saha <= -10)
        {
            defence_player = -1;
        }
        graze = 0;
        Graze_Text.text = "Graze:0";
        graze_index = 0;
        mercy_level = 0;
        Mercy = false;
        ControlManager.Instance.RegisterPower(this);
        OptionIndex = 0;
        gameObject.SetActive(true);
        animator.SetTrigger("open");
    }

    private void EnemyAttack()
    {
#if TEST
        EnemyAttackPanel.OpenPanel(eventInfo.attacks[2]);
#else
        EnemyAttackPanel.OpenPanel(eventInfo.get_attack(this));
#endif
    }

    /// <summary>
    /// 选择攻击, 打开攻击面板
    /// </summary>
    private void OpenAttackPanel()
    {
        AttackPanel.OpenPanel(() =>
        {
            EnemyAttack();
        });
        Debug.Log("打开攻击面板");
    }

    /// <summary>
    /// 选择行动,打开行动面板
    /// </summary>
    private void OpenActionPanel()
    {
        Debug.Log("打开行动面板");
        if (mercy_level >= eventInfo.actions.Length)
        {
            ActionPanel.SetActions(null);
        }
        else
        {
            ActionPanel.SetActions(eventInfo.actions[mercy_level]);
        }
        ActionPanel.OpenPanel((back) =>
        {
            if (back)
            {
                EnemyAttack();
            }
        });
    }

    /// <summary>
    /// 打开道具面板
    /// </summary>
    private void OpenItemsPanel()
    {
        Debug.Log("打开物品面板");
    }

    /// <summary>
    /// 打开仁慈面板
    /// </summary>
    private void OpenMercyPanel()
    {
        string[] effects = eventInfo.mercy_effects;
        foreach (string ef in effects)
        {
            wait_list.Add(ef.Split(':'));
            Debug.Log(ef);
        }
        ActNext();
    }

    /// <summary>
    /// 对敌人攻击
    /// </summary>
    /// <param name="hits">攻击次数</param>
    public void Attack(int hits)
    {
        HPEnemy -= hits * Mathf.Max(0,power-defence_enemy);
        soundEffects.PlayOneShot(sounds[1]);
    }
    /// <summary>
    /// 玩家受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void GetDamage(int damage)
    {
        HP -= damage - defence_player;
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

    public bool DialogBoxStatus()
    {
        return dialogBox.Opened;
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
            Debug.Log("隐藏名称");
            dialogBox.NameDisplay = false;
        }
        else
        {
            dialogBox.NameDisplay = true;
            dialogBox.SetNameText(name);
        }
        dialogBox.SetText(text, callback);
    }

    private void JudgeEnd()//判断战斗结束
    {
        if (health_enemy <= 0)
        {
            string[] effects = eventInfo.kill_effects;
            foreach (string ef in effects)
            {
                wait_list.Add(ef.Split(':'));
                Debug.Log(ef);
            }
            ActNext();
        }
        else if (health_player <= 0)
        {
            RestartPanel.SetActive(true);
            cursor.AimPos = new Vector2(-90,0);
            restart_option = true;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ControlManager.Instance.RegisterPower(this);//获取控制权
    }

    private void Update()
    {
        if (isEnable)//拥有控制权
        {
            if (restart_option)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("Submit"))
                    {
                        Restart();
                    }
                }
            }
            else
            {
                JudgeEnd();
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("Left"))
                    {
                        OptionIndex = Math.Max(0, OptionIndex - 1);
                        soundEffects.PlayOneShot(sounds[0]);
                    }
                    if (Input.GetButtonDown("Right"))
                    {
                        if (mercy)
                        {
                            OptionIndex = Math.Min(3, OptionIndex + 1);
                        }
                        else
                        {
                            OptionIndex = Math.Min(2, OptionIndex + 1);
                        }
                        soundEffects.PlayOneShot(sounds[0]);
                    }
                    if (Input.GetButtonDown("Submit"))
                    {
                        switch (OptionIndex)
                        {
                            case 0:
                                OpenAttackPanel();
                                break;

                            case 1:
                                OpenActionPanel();
                                break;

                            case 2:
                                OpenItemsPanel();
                                break;

                            case 3:
                                OpenMercyPanel();
                                break;

                            default:
                                Debug.LogError($"选项索引不存在:{OptionIndex}");
                                break;
                        }
                    }
                }
            }
        }
    }

    [ContextMenu("打开对话器")]
    private void Test_OpenDialog()
    {
        dialogBox.OpenPanel();
    }

    [ContextMenu("关闭对话器")]
    private void Test_CloseDialog()
    {
        dialogBox.ClosePanel();
    }

    [ContextMenu("设置对话")]
    private void Test_AddDialog()
    {
        dialogBox.SetText("帽子先生是我的好朋友~");
    }

    private List<string[]> wait_list = new List<string[]>();//等待执行的效果队列

    private void ActNext()//执行接下来的行动效果
    {
        bool loop = false;
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
                case FightEventStore.TEXT:
                    OpenDialogBox();
                    SetDialog(cmd[1], null, ActNext);
                    loop = false;
                    break;

                case FightEventStore.DIALOG:
                    OpenDialogBox();
                    SetDialog(cmd[1], NameEnemy, ActNext);
                    loop = false;
                    break;

                case FightEventStore.DEFENCE_ENEMY:
                    int append = int.Parse(cmd[1]);
                    DefenceEnemy += append;
                    loop = true;
                    break;

                case FightEventStore.NEXT:
                    MercyLevel++;
                    loop = true;
                    break;

                case FightEventStore.MERCY:
                    Mercy = true;
                    MercyLevel++;
                    loop = true;
                    break;

                case FightEventStore.END_DIALOG:
                    CloseDialogBox();
                    loop = true;
                    break;

                case FightEventStore.END_MERCY://结束战斗
                    Heart.Instance.SetHeart(eventInfo.heart_peace);
                    ClosePanel();
                    loop = false;
                    break;

                case FightEventStore.END_KILL://结束战斗
                    Heart.Instance.SetHeart(eventInfo.heart_kill);
                    ClosePanel();
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

    public void ClosePanel()//结束战斗
    {
        animator.SetTrigger("close");
        ControlManager.Instance.UnregisterPower(this);
        dialogBox.gameObject.SetActive(false);
        Heart.Instance.TimerActive = true;
    }

    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ControlManager.Instance.UnregisterPower(this);
    }
}