using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SceneHeart:MonoBehaviour
{
    public SpriteRenderer[] front;

    public SpriteRenderer back_color;

    public SpriteRenderer back;

    public Sprite[] sprites_h;
    public Sprite[] sprites_l;

    private int state = 0;

    private void Update()
    {
        float value = Heart.Instance.HeartLevel;
        if (value < -3)//低
        {
            if(state!= 0)
            {
                state = 0;
                Change();
            }
        }
        else if (value <= 3)//中
        {
            if (state != 1)
            {
                state = 1;
                Change();
            }
        }
        else//高
        {
            if (state != 2)
            {
                state = 2;
                Change();
            }
        }

    }

    private void Change()
    {
        switch(state)
        {
            case 0:
                back_color.color = new Color(0.3352439f, 0.3860964f, 0.4056604f);
                back.color = new Color(0f, 0.647f, 1f);
                foreach (var sp in front)
                {
                    sp.sprite = sprites_l[Random.Range(0, sprites_l.Length - 1)];
                }
                break;
            case 1:
                back_color.color = new Color(0.5387505f, 0.5566038f, 0.5436597f);
                back.color = new Color(0.5728821f, 1f, 0f);
                if (Random.value > 0.5f)
                {
                    foreach (var sp in front)
                    {
                        sp.sprite = sprites_l[Random.Range(0, sprites_l.Length - 1)];
                    }
                }
                else
                {
                    foreach (var sp in front)
                    {
                        sp.sprite = sprites_l[Random.Range(0, sprites_h.Length - 1)];
                    }
                }
                break;
            case 2:
                back_color.color = new Color(1f, 0.9632653f, 0.9f);
                back.color = Color.red;
                foreach (var sp in front)
                {
                    sp.sprite = sprites_l[Random.Range(0, sprites_h.Length - 1)];
                }
                break;

        }
    }
}