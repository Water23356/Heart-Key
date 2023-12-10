using UnityEngine;
/// <summary>
/// 玩家判定点
/// </summary>
public class PlayerRegion:MonoBehaviour
{
    [SerializeField]
    private Collider2D cd;
    [SerializeField]
    private FightSystem system;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject grazeRegion;
    /// <summary>
    /// 无敌时间
    /// </summary>
    private float invincible = 0f;
    private float def_invincible = 2f;
    /// <summary>
    /// 是否无视攻击(非无敌状态)
    /// </summary>
    [SerializeField]
    private bool defence = false;

    public float Invincible
    {
        get => invincible;
        set
        {
            invincible = value;
            if(invincible > 0)
            {
                if (grazeRegion.activeSelf) { grazeRegion.SetActive(false); }
            }
            else
            {
                if (!grazeRegion.activeSelf) { grazeRegion.SetActive(true); }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Invincible > 0) return;//无敌忽略判定

        string tag = collision.tag;
        if(tag == GameText.TAG_BULLET || tag == GameText.TAG_BULLET_GRAZED)//玩家中弹
        {
            if(!defence)
            {
                Bullet bullet = collision.GetComponent<Bullet>();
                if (bullet != null)
                {
                    system.GetDamage(bullet.Damage);
                    Invincible = def_invincible;
                    animator.SetBool("active", true);
                    bullet.Destroy();
                }
            }
        }
        if(tag == GameText.TAG_DEFENCE)
        {
            Debug.Log("开启防御");
            defence = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Invincible > 0) return;//无敌忽略判定

        string tag = collision.tag;
        if (tag == GameText.TAG_BULLET || tag == GameText.TAG_BULLET_GRAZED)//玩家中弹
        {
            if (!defence)
            {
                Bullet bullet = collision.GetComponent<Bullet>();
                if (bullet != null)
                {
                    system.GetDamage(bullet.Damage);
                    Invincible = def_invincible;
                    animator.SetBool("active", true);
                    bullet.Destroy();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tag == GameText.TAG_DEFENCE)
        {
            defence = false;
        }
    }

    private void Awake()
    {
        defence = false;
        Invincible = 0f;
    }

    private void OnEnable()
    {
        defence = false;
        Invincible = 0f;
    }

    private void Update()
    {
        if(Invincible > 0)//无敌状态计时, 以及动画关闭逻辑
        {
            Invincible -= Time.deltaTime;
            if (Invincible <= 0)
            {
                animator.SetBool("active", false);
            }
        }
    }
}