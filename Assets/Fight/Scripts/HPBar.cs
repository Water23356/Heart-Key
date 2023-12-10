using UnityEngine;
using UnityEngine.UI;

public class HPBar:ER.UI.ValueImageBar
{
    public Image image2;
    private float value2;
    /// <summary>
    /// 进度
    /// </summary>
    public float Value2
    {
        get
        {
            return value2;
        }
        set
        {
            image2.fillAmount = value;
            this.value2 = value;
        }
    }
}