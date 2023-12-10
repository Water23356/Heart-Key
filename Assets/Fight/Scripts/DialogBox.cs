// Ignore Spelling: Dialog

using ER.Control;
using System;
using System.Buffers;
using TMPro;
using UnityEngine;

/// <summary>
/// 对话器
/// </summary>
public class DialogBox : MonoControlPanel
{
    [SerializeField]
    private TMP_Text name_text;
    [SerializeField]
    public GameObject name_object;

    [SerializeField]
    private TMP_Text text;

    /// <summary>
    /// 回调函数
    /// </summary>
    private Action callback;

    [SerializeField]
    private Animator animator;

    private int length = 0;
    private string tmp;
    private int cd = 2;
    private float timer = 0;
    private bool over = true;
    private bool name_display;//名称是否显示
    private bool opened = false;
    /// <summary>
    /// 面板是否打开
    /// </summary>
    public bool Opened => opened;
    /// <summary>
    /// 是否显示名称框
    /// </summary>
    public bool NameDisplay
    {
        get => name_display;
        set
        {
            Debug.Log($"名称显示状态:{value}");
            name_display = value;
            name_object.SetActive(value);
        }
    }
    /// <summary>
    /// 设置名称文本
    /// </summary>
    /// <param name="text"></param>
    public void SetNameText(string text)
    {
        name_text.text = text;
    }
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
                if(submit_cd <=0)
                {
                    if (Input.GetButtonDown("Submit"))
                    {
                        Debug.Log("对话器:submit");
                        if (over)
                        {
                            submit_cd = 0.2f;
                            Next();
                        }
                        else
                        {
                            Debug.Log("跳过消息动画");
                            text.text = tmp;
                            over = true;
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