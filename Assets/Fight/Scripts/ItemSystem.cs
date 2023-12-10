/// <summary>
/// 物品系统
/// </summary>
public static class ItemSystem
{
    /// <summary>
    /// 获取指定道具的文本
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetText(string name)
    {
        switch(name)
        {
            case "玩具刀":
                return "每次 QTE成功判定后造成伤害加 1";
            case "二头身玩具":
                return "玩家每次攻击结算后总伤害加 1";
        }
        return "错误物品";
    }
}