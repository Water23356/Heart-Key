using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// staff歌词列表
/// </summary>
public class StaffWordList : MonoBehaviour
{
    private float time_start = 0;
    [SerializeField]
    private float timer = 0f;
    private string[] jp =
    {
        "たゆたう時の流れ　定まらぬ明日は",
        "悲しみを写す水面に　広がる波紋のごとく",
        "見えない 未来に怯え 巡る季節は",
        "Ah　永久の夢",
        "幾千月日が流れ 忘れられても",
        "幾万幾夜の空は また巡る",
        "慈しむ 心を今 取り戻せるなら",
        "Never too late for Again！",
        "繋がる罪の連鎖　望むとは別に",
        "無意識を映す鏡は　誤魔化す術を知らない",
        "見えては　困る物さえ　ありふれるのは",
        "Ah　無常な真理",
        "幾千月日が流れ　巡り会おうとも",
        "幾万幾億の風　吹き荒れる",
        "偽りの無い笑顔を　取り戻したら",
        "You can find your way",
        "幸せなど　探すとして",
        "見つかるもの　などではないと　知ってても",
        "答えなどは　探すとして",
        "見つけられぬ　ものではないと　知ってても",
        "まだ…",
        "幾千月日が流れ　忘れられても",
        "幾万幾夜の空は　また巡る",
        "心に宿る闇さえ　超えて行けたら",
        "You can find your way"
    };
    private string[] zh =
    {
        "时光匆匆奔涌 风雨飘摇的明天",
        "将悲伤刻映在水面 宛如波纹粼粼扩散",
        "驻足于 无形的未来 季节循环往复",
        "啊 永恒的梦",
        "任万千岁月流转 即使如数遗忘",
        "繁星如洗的夜空 顾自日落月起",
        "只要此刻 重返那颗 溢满怜爱的心",
        "Never too late for Again！",
        "沉寂心间期待 系缚罪业的锁链",
        "辉映无意识的明镜 是否又是氤氲幻象",
        "鼓起勇气 眼前也弥漫着 不尽的困惑",
        "啊 无常的真理",
        "任万千岁月流转 即便得以交汇",
        "萦纡千回的晨风 依旧浩荡",
        "只要夺回 那天真无邪的笑靥",
        "You can find your way",
        "尽管 我很清楚",
        "所谓的幸福 绝非近在咫尺",
        "尽管 我很明白",
        "所谓的答案 绝非唾手可得",
        "依然……",
        "任万千岁月流转 即使如数遗忘",
        "繁星如洗的星空 顾自日落月起",
        "只要跨越 心中的漫漫长夜",
        "You can find your way"
    };
    private Vector2[] time_line =
    {
        new Vector2(26.5f,37.5f),
        new Vector2(38,51.5f),
        new Vector2(52,57.5f),
        new Vector2(58,64f),
        new Vector2(64.5f,70.5f),
        new Vector2(71,76.5f),
        new Vector2(77,83.5f),
        new Vector2(84,90.5f),
        new Vector2(103,114.5f),
        new Vector2(115,127.5f),
        new Vector2(128,134.5f),
        new Vector2(135,140.5f),
        new Vector2(141,147.5f),
        new Vector2(148,154f),
        new Vector2(154.5f,160.5f),
        new Vector2(161,166.5f),
        new Vector2(167,171.5f),
        new Vector2(172,178.5f),
        new Vector2(179,185.5f),
        new Vector2(186,191.5f),
        new Vector2(192,208.5f),
        new Vector2(223,228.5f),
        new Vector2(229,235.5f),
        new Vector2(236,242.5f),
        new Vector2(243,300.5f),
    };
    private int index;
    private bool playing = false;//记录当前歌词是否正在播放
    [Tooltip("日语词栏")]
    [SerializeField]
    private TextBlock jp_text;
    [Tooltip("中文词栏")]
    [SerializeField]
    private TextBlock zh_text;
    void Start()
    {
        index = 0;
        time_start = Time.fixedTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer = Time.fixedTime - time_start;
        if (!playing && timer >= time_line[index].x)//启动判断
        {
            jp_text.Text = jp[index];
            zh_text.Text = zh[index];
            jp_text.Display();
            zh_text.Display();
            playing = true;
        }
        if(playing && timer >= time_line[index].y)//截止判断
        {
            jp_text.Hide();
            zh_text.Hide();
            playing = false;
            index++;
            if(index>=time_line.Length)
                enabled = false;
        }
    }
}

