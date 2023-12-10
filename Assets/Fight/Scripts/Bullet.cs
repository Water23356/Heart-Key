// Ignore Spelling: collider

using UnityEngine;
/// <summary>
/// 弹幕类
/// </summary>
public class Bullet:MonoBehaviour
{
    [SerializeField]
    protected Collider2D self_collider;
    [SerializeField]
    [Tooltip("是否可销毁")]
    private bool destroyable = true;
    [SerializeField]
    private int damage;
    /// <summary>
    /// 子弹携带的伤害
    /// </summary>
    public int Damage
    {
        get => damage;
        set=>damage = value;
    }

    /// <summary>
    /// 销毁该子弹对象
    /// </summary>
    public virtual void Destroy()
    {
        if(destroyable)
            Destroy(gameObject);
    }

}