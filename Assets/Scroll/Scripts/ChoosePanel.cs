using ER.Control;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.VirtualTexturing;
//选择框
public class ChoosePanel:MonoControlPanel
{
    [SerializeField]
    private CursorMove cursor;
    [SerializeField]
    private TMP_Text option_1;
    [SerializeField]
    private TMP_Text option_2;
    [SerializeField]
    private Animator animator;//自身动画机
    [SerializeField]
    private int index;//当前索引

    private Action<int> callback;

    private int OptionIndex
    {
        get => index;
        set
        {
            index = value;
            if (index >= 0)
            {
                cursor.AimPos = new Vector2(840, -60 - index * 110);
            }
            else
            {
                cursor.AimPos = new Vector2(840, -60);
            }


        }
    }

    public void OpenPanel(Action<int> _callback)
    {
        callback = _callback;
        gameObject.SetActive(true);
        OptionIndex = 0;
        animator.SetTrigger("open");
        ControlManager.Instance.RegisterPower(this);
        enabled = true;
    }

    public void SetText(string text_1,string text_2)
    {
        option_1.text = text_1;
        option_2.text = text_2;
    }
    public void ClosePanel()
    {
        animator.SetTrigger("close");
        ControlManager.Instance.UnregisterPower(this);
    }
    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Up"))
        {
            OptionIndex = Math.Max(0, index - 1);
        }
        if (Input.GetButtonDown("Down"))
        {
            OptionIndex = Math.Min(1, index + 1);
        }
        if (Input.GetButtonDown("Submit"))
        {
            callback?.Invoke(OptionIndex);
            callback = null;
            enabled = false;
            ClosePanel();
        }
    }

}