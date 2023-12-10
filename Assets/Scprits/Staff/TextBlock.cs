using TMPro;
using UnityEngine;
/// <summary>
/// staff歌词块
/// </summary>
public class TextBlock:MonoBehaviour
{
    [SerializeField]
    private TMP_Text text_tmp;
    [SerializeField]
    private Animator animator;
    /// <summary>
    /// 文本内容
    /// </summary>
    public string Text
    {
        get=>text_tmp.text;
        set=>text_tmp.text = value;
    }

    public void Display()
    {
        animator.SetTrigger("display");
    }
    public void Hide()
    {
        animator.SetTrigger("hide");
    }
}