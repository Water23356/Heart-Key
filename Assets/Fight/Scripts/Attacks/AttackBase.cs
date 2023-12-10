using System;
/// <summary>
/// 敌人攻击接口
/// </summary>
public interface IEnemyAttack
{
    /// <summary>
    /// 开始攻击演出
    /// </summary>
    public void StartAttack(EnemyAttack _owner,Action _callback);
    /// <summary>
    /// 停止攻击演出
    /// </summary>
    public void StopAttack();
}