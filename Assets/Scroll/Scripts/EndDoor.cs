using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EndDoor : MonoBehaviour
{
    private PlayerController player;
    [SerializeField]
    private ScrollSystem system;
    private bool over = false;
    public EndPrinter printer;
    public GameObject staff;

    public SpriteRenderer image_back;
    public SpriteRenderer image_color;
    public SceneHeart sceneHeart;
    public string[] he =
    {
        "你回到家",
        "你回想起今天发生的一切...",
        "一股甜蜜涌上心头",
        "你向镜中的自己说着今天的美好",
        "\"真不会认为有人愿意与你交朋友吧\"",
        "你不想理会他",
        "\"那位陌生人内心可不愿意照顾你这个麻烦\"",
        "你继续无视了他",
        "\"你的母亲更是认为你是个累赘\"",
        "你不再理他",
        "你感到一阵放松",
        "打开门",
        "原来门的里面就是外面的世界啊",
        "你突然相信",
        "你一定会好起来的",
        "也不会再让母亲担心"
    };
    public string[] ne =
    {
        "你回到家",
        "你回想起今天发生的一切...",
        "可能...没有那么坏？",
        "你试图向镜中的自己寻求认可",
        "\"真不会认为有人愿意与你交朋友吧\"",
        "...",
        "\"那位陌生人内心可不愿意照顾你这个麻烦\"",
        "...",
        "\"你的母亲更是认为你是个累赘\"",
        "...",
        "你试图反驳",
        "但却找不出反驳的点",
        "你对自己产生了深深的怀疑",
        "打开门",
        "门的里面还是自己的房间",
        "...",
        "兜兜转转",
        "还是回到了最初的原点了呢"
    };
    public string[] be =
    {

        "你回到家",
        "你回想起今天发生的一切...",
        "今天的一切令你感到绝望",
        "你试图向镜中的自己寻求认可",
        "\"真不会认为有人愿意与你交朋友吧\"",
        "...",
        "\"那位陌生人内心可不愿意照顾你这个麻烦\"",
        "...",
        "\"你的母亲更是认为你是个累赘\"",
        "...",
        "你的内心似乎在认同着这些话语",
        "你感到深深的绝望",
        "打开门",
        "门的里面一片漆黑",
        "你想要离开这里面",
        "回头发现门已上锁",
        "恐惧...害怕...",
        "...",
        "一切都结束了"
    };
    /// <summary>
    /// 事件索引
    /// </summary>
    public int event_index = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (over) return;
        string tag = collision.tag;
        if (tag == GameText.TAG_PLAYER)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log($"接触玩家:{player != null}");
            EnterEvent();
            over = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
    public float timer = 2f;
    private int state = 0;
    private int index = 0;
    private void EnterEvent()
    {
        HeartLevel = Heart.Instance.HeartLevel;
        Heart.Instance.TimerActive = false;
        printer.OpenPanel();
    }

    private float HeartLevel;
    private void Update()
    {
        if(over)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                index = 0;
                if (HeartLevel < -10)//低
                {
                    state = 1;
                    printer.SetText(be[index] + "\n", Act);
                    index++;
                }
                else if (HeartLevel <= 3)//中
                {
                    state = 2;
                    printer.SetText(ne[index] + "\n", Act);
                    index++;
                }
                else//高
                {
                    state = 3;
                    printer.SetText(he[index]+"\n", Act);
                    index++;
                }
                enabled = false;
            }
        }
    }

    private void Act()
    {
        bool next = false;
        switch(state)
        {
            case 1:
                if (index < be.Length)
                {
                    printer.AppendText(be[index]+"\n", Act);
                    index++;
                }
                else
                {
                    printer.DisplayTitle(state,Staff);
                }
                break;
            case 2:
                if (index < ne.Length)
                {
                    printer.AppendText(ne[index] + "\n", Act);
                    index++;
                }
                else
                {
                    printer.DisplayTitle(state, Staff);
                }
                break;
            case 3:
                if (index < he.Length)
                {
                    printer.AppendText(he[index] + "\n", Act);
                    index++;
                }
                else
                {
                    printer.DisplayTitle(state, Staff);
                }
                break;
        }

        
    }

    private void Staff()
    {
        staff.SetActive(true);
        image_color.gameObject.SetActive(true);
        image_back.gameObject.SetActive(true);
        sceneHeart.enabled = true;
    }
}