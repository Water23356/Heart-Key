using ER.Control;
using System.Collections.Generic;
using UnityEngine;
using ER;
/// <summary>
/// 敌人攻击
/// </summary>
public class EnemyAttack:MonoControlPanel
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private FightSystem system;
    [SerializeField]
    private PlayerRegion region;

    private Dictionary<string,IEnemyAttack> attacks = new Dictionary<string,IEnemyAttack>();
    [Tooltip("敌人攻击预制体 - 物体名称需要和招式名称相同")]
    [SerializeField]
    private GameObject[] AttackPrefabs;
    [SerializeField]
    private float speed = 20;//自机速度

    /// <summary>
    /// 自机位置
    /// </summary>
    public Vector2 PlayerPos
    {
        get => region.transform.localPosition;
        set=> region.transform.localPosition= value;
    }
    public void OpenPanel(string attackName)
    {
        InitAttack(attackName);
        gameObject.SetActive(true);
        animator.SetTrigger("open");
        ControlManager.Instance.RegisterPower(this);
        enabled = true;
    }
    private void InitAttack(string name)
    {
        if(ContainsPrefab(name))
        {
            if(!attacks.ContainsKey(name))//判断是否已经实例化
            {
                GameObject obj = GameObject.Instantiate(GetPrefab(name), transform);
                attacks.Add(name, obj.GetComponent<IEnemyAttack>());
            }
            region.transform.localPosition = Vector3.zero;
            IEnemyAttack attack = attacks[name];
            attack.StartAttack(this,ClosePanel);
        }
        else
        {
            Debug.LogError("未找到该攻击模组");
            return;
        }
    }
    private GameObject GetPrefab(string name)
    {
        foreach (var attack in AttackPrefabs)
        {
            if (attack.name == name)
            {
                return attack;
            }
        }
        return null;
    }
    /// <summary>
    /// 判断指定攻击预制体是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool ContainsPrefab(string name)
    {
        foreach(var attack in AttackPrefabs)
        {
            if (attack.name == name)
            {
                return true;
            }
        }
        return false;
    }

    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }
    public void ClosePanel()
    {
        animator.SetTrigger("close");
        ControlManager.Instance.UnregisterPower(this);
        enabled = false;
    }

    private bool moving = false;
    private Dir moveDir = Dir.Up;

    private void Update()                          
    {
        if(isEnable)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            //Debug.Log($"move:({x},{y})");

            if(PlayerPos.x > -320 && PlayerPos.x <320 && PlayerPos.y >-180 && PlayerPos.y<180)
            {
                PlayerPos += new Vector2(x, y) * speed * Time.deltaTime;
            }
            else
            {
                if(PlayerPos.x <= -320)
                {
                    PlayerPos = new Vector2(-319, PlayerPos.y);
                }
                if (PlayerPos.x >= 320)
                {
                    PlayerPos = new Vector2(319, PlayerPos.y);
                }
                if (PlayerPos.y <= -180)
                {
                    PlayerPos = new Vector2(PlayerPos.x,-179);
                }
                if (PlayerPos.y >= 180)
                {
                    PlayerPos = new Vector2(PlayerPos.x, 179);
                }
            }
        }
    }
}