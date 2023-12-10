using ER.Control;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 玩家攻击模块
/// </summary>
public class PlayerAttack : MonoControlPanel
{
    /// <summary>
    /// 战斗系统
    /// </summary>
    [SerializeField]
    private FightSystem system;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private PlayerAttackBlock[] attack_blocks;

    private Dir player_dir;//玩家当前朝向

    /// <summary>
    /// 玩家当前朝向
    /// </summary>
    public Dir PlayerDir
    {
        get => player_dir;
        private set
        {
            player_dir = value;
            switch (player_dir)
            {
                case Dir.Up:
                    player.localEulerAngles = new Vector3(0,0,180);
                    break;

                case Dir.Down:
                    player.localEulerAngles = new Vector3(0, 0, 0);
                    break;

                case Dir.Left:
                    player.localEulerAngles = new Vector3(0, 0, -90);
                    break;

                case Dir.Right:
                    player.localEulerAngles = new Vector3(0, 0, 90);
                    break;
            }
        }
    }

    private Dir[] attacks = new Dir[3];//攻击方向组
    private float[] attack_times = new float[3];//攻击时刻组
    private float start_attack_time = 1f;//攻击前的空白时间
    private float attack_end = 6f;//攻击结束时间
    private int attack_index=0;//当前攻击阶段索引
    [SerializeField]
    private float timer = 0f;
    private int hits = 0;//成功命中次数
    private Action callback;//回调函数
    /// <summary>
    /// 成功命中次数
    /// </summary>
    public int Hits
    {
        get => hits;
        set => hits = value;
    }

    public void OpenPanel(Action _callback = null)
    {
        gameObject.SetActive(true);
        callback = _callback;
        animator.SetTrigger("open");
        PlayerDir = Dir.Down;
        attacks[0] = GetRandomDir();
        attacks[1] = GetRandomDir();
        attacks[2] = GetRandomDir();
        attack_times[0] = Random.Range(start_attack_time, start_attack_time + 1);
        attack_times[1] = Random.Range(attack_times[0], start_attack_time + 1);
        attack_times[2] = Random.Range(attack_times[1], attack_end-2f); ;
        timer = 0f;
        attack_index = 0;
        hits = 0;
        enabled = true;
        ControlManager.Instance.RegisterPower(this);
    }
    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }
    public void ClosePanel()
    {
        enabled = false;
        animator.SetTrigger("close");
        ControlManager.Instance.UnregisterPower(this);
        callback?.Invoke();
    }
    /// <summary>
    /// 获取一个随机方向
    /// </summary>
    /// <returns></returns>
    private Dir GetRandomDir()
    {
        float num = ER.RandomNumber.RangeF();
        if (num < 0.25f)
        {
            return Dir.Down;
        }
        else if(num<0.5f)
        {
            return Dir.Left;
        }
        else if(num < 0.75f)
        {
            return Dir.Up;
        }
        else
        {
            return Dir.Right;
        }
    }
    /// <summary>
    /// 生成攻击方块
    /// </summary>
    private void Attack()
    {
        attack_blocks[attack_index].Display(attacks[attack_index]);
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetButtonDown("Left"))
            {
                PlayerDir = Dir.Left;
            }
            if (Input.GetButtonDown("Right"))
            {
                PlayerDir = Dir.Right;
            }
            if (Input.GetButtonDown("Up"))
            {
                PlayerDir = Dir.Up;
            }
            if (Input.GetButtonDown("Down"))
            {
                PlayerDir = Dir.Down;
            }
        }
        timer += Time.deltaTime;

        if (timer > attack_end)//结束攻击行t
        {
            Debug.Log($"攻击阶段结束:{hits}");
            system.Attack(hits);
            ClosePanel();
        }
        if (attack_index >= 3) return;

        if(timer > attack_times[attack_index])
        {
            Attack();//执行攻击
            attack_index++;
            Debug.Log("执行攻击");
        }
    }
}