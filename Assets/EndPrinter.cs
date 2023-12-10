using ER.Control;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndPrinter:MonoControlPanel
{

    [SerializeField]
    private TMP_Text text;

    /// <summary>
    /// 回调函数
    /// </summary>
    private Action callback;

    [SerializeField]
    private Animator animator;

    public Animator titleAnimator;

    public TMP_Text title;//结束标题
    public TMP_Text description;//标题描述

    private int length = 0;
    private string tmp;
    private int cd = 2;
    private float timer = 0;
    private bool over = true;
    private bool intitle = false;//是否进入标题状态
    private bool opened = false;
    /// <summary>
    /// 面板是否打开
    /// </summary>
    public bool Opened => opened;
    /// <summary>
    /// 设置新消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="callback"></param>
    public void SetText(string text, Action callback = null)
    {
        this.callback = callback;
        Debug.Log("重设文本");
        this.text.text = string.Empty;
        tmp = text;
        length = 0;
        timer = cd;
        over = false;
    }

    public void AppendText(string text,Action callback=null)
    {
        this.callback = callback;
        tmp += text;
        timer = cd;
        over = false;
    }

    /// <summary>
    /// 确定消息
    /// </summary>
    public void Next()
    {
        Debug.Log("消息确认完毕");
        callback?.Invoke();
    }

    public void OpenPanel()
    {
        over = true;
        opened = true;
        gameObject.SetActive(true);
        animator.SetTrigger("open");
        ControlManager.Instance.RegisterPower(this);
    }

    public void DisplayTitle(int index,Action _callback)
    {
        switch(index)
        {
            case 1:
                title.text = "渊黑色的绝望之匙";
                description.text = "一切都结束了,黑暗摧毁了一切";
                break;
            case 2:
                title.text = "凋叶色的迷惘之匙";
                description.text = "回环往复,遗失从前的出口";
                break;
            case 3:
                title.text = "金绀色的希望之匙";
                description.text = "继续前行吧";
                break;
        }
        text.gameObject.SetActive(false);
        titleAnimator.gameObject.SetActive(true);
        titleAnimator.SetTrigger("open");
        intitle = true;
        callback = _callback;
    }
    private void OverTitle()
    {
        titleAnimator.SetTrigger("close");
        intitle = false;
        Invoke("End",2f);
    }
    private void End()
    {
        ClosePanel();
        callback?.Invoke();
    }

    public void ClosePanel()
    {
        over = true;
        opened = false;
        animator.SetTrigger("close");
        ControlManager.Instance.UnregisterPower(this);
    }

    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (over) return;
        timer--;
        if (timer <= 0)
        {
            timer = cd;
            text.text = tmp.Substring(0, length);
            length++;
            if (length > tmp.Length)
            {
                over = true;
            }
        }
    }

    private float submit_cd = 0;//确认cd,防止快速确认直接跳过剧情
    private void Update()
    {
        submit_cd -= Time.deltaTime;
        if (IsEnable)
        {
            if (Input.anyKey)
            {
                if (submit_cd <= 0)
                {
                    if (Input.GetButtonDown("Submit"))
                    {
                        if(intitle)
                        {
                            OverTitle();
                        }
                        else
                        {
                            if (over)
                            {
                                submit_cd = 0.2f;
                                Next();
                            }
                            else
                            {
                                text.text = tmp;
                                over = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        ControlManager.Instance.UnregisterPower(this);
    }
}