using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 心境值系统
/// </summary>
public class Heart : ER.MonoSingletonAutoCreate<Heart>
{
    private float joy_base;//喜(基础)
    private float anger_base;//怒(基础)
    private float saha_base;//哀乐(基础)

    private float joy;//喜(变化值)
    private float anger;//怒(变化值)
    private float saha;//哀乐(变化值)

    private float restore_speed = 0.2f;//心境值复原速率

    public void SetHeart(float[] change)
    {
        joy_base += change[0];
        anger_base += change[1];
        saha_base += change[2];
        joy += change[3];
        anger += change[4];
        saha += change[5];
    }
    /// <summary>
    /// 当前[喜]值
    /// </summary>
    public float Joy => joy + joy_base;
    /// <summary>
    /// 当前[怒]值
    /// </summary>
    public float Anger => anger + anger_base;
    /// <summary>
    /// 当前[哀\乐]值
    /// </summary>
    public float SaHa => saha + saha_base;
    /// <summary>
    /// 当前心境值
    /// </summary>
    public float HeartLevel => Joy - 0.5f * Anger + SaHa;

    /// <summary>
    /// 计时器是否有效
    /// </summary>
    public bool TimerActive = true;

    void Start()
    {
        
    }

    void Update()
    {
        if(TimerActive)//变化值逐渐恢复为0
        {
            RestoreValue(ref joy,joy_base);
            RestoreValue(ref anger, anger_base);
            RestoreValue(ref saha, saha_base);
        }
    }

    public void Init()
    {
        joy_base = 5; 
        anger_base = 5;
        saha_base = 0;
        joy = 0;
        anger = 0;
        saha = 0;   
    }

    private void RestoreValue(ref float value,float aim)
    {
        if (Mathf.Abs(value) < 0.01f)
        {
            value = aim;
        }
        else if (joy < 0)
        {
            joy += Time.deltaTime * restore_speed;
        }
        else
        {
            joy -= Time.deltaTime * restore_speed;
        }
    }
}

