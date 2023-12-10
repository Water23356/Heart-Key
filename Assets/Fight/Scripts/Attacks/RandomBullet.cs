using ER;
using UnityEngine;

/// <summary>
/// 随机直线弹
/// </summary>
public class RandomBullet : Water
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 dir;
    [SerializeField]
    private Bullet self;
    public float overTime = 10f;
    [SerializeField]
    private float timer = 0;

    public float waitTime;

    public float Speed { get => speed; set => speed = value; }
    public Vector2 Dir { get => dir; set => dir = value; }
    public Bullet SelfBullet => self;

    public override void ResetState()
    {
        timer = 0;
        tag = GameText.TAG_BULLET;
    }

    protected override void OnHide()
    {

    }

    private void Update()
    {
        if(waitTime>0)
        {
            waitTime-=Time.deltaTime;
        }
        else
        {
            timer += Time.deltaTime;
            if (speed != 0 && dir.magnitude > 0)
            {
                transform.localPosition += (Vector3)dir.normalized * speed * Time.deltaTime;
            }
            if (timer >= overTime)
            {
                base.Destroy();
            }
            Vector2 pos = transform.localPosition;
            if (pos.x < -960 || pos.x > 960 || pos.y > 540 || pos.y < -540)
            {
                base.Destroy();
            }
            float angle = Vector2.down.ClockAngle(dir);
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }

}