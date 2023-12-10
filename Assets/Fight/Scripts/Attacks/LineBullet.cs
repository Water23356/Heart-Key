
using ER;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 直线弹
/// </summary>
public class LineBullet:Water
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
    [SerializeField]
    private Sprite[] Sprites;
    [SerializeField]
    private Image image; 

    public float Speed { get => speed; set => speed = value; }
    public Vector2 Dir { get => dir; set => dir = value; }
    public Bullet SelfBullet => self;

    public override void ResetState()
    {
        timer = 0;
        tag = GameText.TAG_BULLET;
        if(Sprites.Length > 0)
        {
            float number = ER.RandomNumber.RangeF(0, Sprites.Length);
            for(int i=0;i< Sprites.Length;i++)
            {
                if(number<(i+1))
                {
                    image.sprite = Sprites[i];
                    break;
                }
            }
        }
    }

    protected override void OnHide()
    {
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(speed !=0 && dir.magnitude > 0)
        {
            transform.localPosition += (Vector3)dir.normalized * speed * Time.deltaTime;
        }
        if(timer>=overTime)
        {
            base.Destroy();
        }
        Vector2 pos = transform.localPosition;
        if (pos.x < -960 || pos.x > 960 || pos.y>540||pos.y<-540)
        {
            base.Destroy();
        }
        float angle = Vector2.down.ClockAngle(dir);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

}