using UnityEngine;
/// <summary>
/// 擦弹区域
/// </summary>
public class GrazeRegion:MonoBehaviour
{
    [SerializeField]
    private Collider2D cd;
    [SerializeField]
    private FightSystem system;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //bug.Log("擦弹进入");

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        //bug.Log("擦弹离开");
        string tag = collision.collider.tag;
        if (tag == GameText.TAG_BULLET)//只对没有擦的弹幕进行计数
        {
            collision.collider.gameObject.tag = GameText.TAG_BULLET_GRAZED;//标记, 防止重复计算
            system.Graze++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("擦弹进入");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //bug.Log("擦弹离开");
        string tag = collision.tag;
        if (tag == GameText.TAG_BULLET)//只对没有擦的弹幕进行计数
        {
            collision.gameObject.tag = GameText.TAG_BULLET_GRAZED;//标记, 防止重复计算
            system.Graze++;
        }

    }
}