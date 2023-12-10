using UnityEngine;

/// <summary>
/// 攻击方块
/// </summary>
public class PlayerAttackBlock : MonoBehaviour
{
    /// <summary>
    /// 攻击判定容错距离
    /// </summary>
    public static float attack_distance = 10f;
    [SerializeField]
    private PlayerAttack playerAttack;

    /// <summary>
    /// 移动方向
    /// </summary>
    private Dir move_dir;
    /// <summary>
    /// 移动距离
    /// </summary>
    private float move_distance = 200;

    private float speed = 200f;

    public Dir MoveDir
    {
        get => move_dir;
        private set
        {
            move_dir = value;
            switch (move_dir)
            {
                case Dir.Up:
                    transform.localPosition = new Vector3 (0, -1, 0) * move_distance;
                    transform.localEulerAngles = new Vector3(0,0,180);
                    break;

                case Dir.Down:
                    transform.localPosition = new Vector3(0, 1, 0) * move_distance;
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;

                case Dir.Right:
                    transform.localPosition = new Vector3(-1, 0, 0) * move_distance;
                    transform.localEulerAngles = new Vector3(0, 0, 90);
                    break;

                case Dir.Left:
                    transform.localPosition = new Vector3(1, 0, 0) * move_distance;
                    transform.localEulerAngles = new Vector3(0, 0, -90);
                    break;
            }
        }
    }

    public void Display(Dir md)
    {
        Debug.Log($"攻击方块显示!:{md}");
        MoveDir = md;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        switch (move_dir)
        {
            case Dir.Up:
                transform.localPosition += new Vector3(0, 1, 0) * speed * Time.deltaTime;
                if(transform.localPosition.y >= -attack_distance && playerAttack.PlayerDir == Dir.Down)
                {
                    playerAttack.Hits++;
                    //Debug.Log($"攻击触发:{gameObject.name}");
                    gameObject.SetActive(false);
                }
                if (transform.localPosition.y >= 0)
                {
                    gameObject.SetActive(false);
                }
                break;

            case Dir.Down:
                transform.localPosition += new Vector3(0, -1, 0)* speed * Time.deltaTime;
                if (transform.localPosition.y <= attack_distance && playerAttack.PlayerDir == Dir.Up)
                {
                    playerAttack.Hits++;
                    //Debug.Log($"攻击触发:{gameObject.name}");
                    gameObject.SetActive(false);
                }
                if (transform.localPosition.y <= 0)
                {
                    gameObject.SetActive(false);
                }
                break;

            case Dir.Left:
                transform.localPosition += new Vector3(-1, 0, 0)* speed * Time.deltaTime;
                if (transform.localPosition.x <= attack_distance && playerAttack.PlayerDir == Dir.Right)
                {
                    playerAttack.Hits++;
                    //Debug.Log($"攻击触发:{gameObject.name}");
                    gameObject.SetActive(false);
                }
                if (transform.localPosition.x <= 0)
                {
                    gameObject.SetActive(false);
                }
                break;

            case Dir.Right:
                transform.localPosition += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                if (transform.localPosition.x >= -attack_distance && playerAttack.PlayerDir == Dir.Left)
                {
                    //Debug.Log($"攻击触发:{gameObject.name}");
                    playerAttack.Hits++;
                    gameObject.SetActive(false);
                }
                if (transform.localPosition.x >= 0)
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}