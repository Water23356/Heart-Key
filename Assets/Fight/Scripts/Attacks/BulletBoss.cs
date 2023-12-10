using ER;
using System;
using UnityEngine;

public class BulletBoss:Water
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

    public EAttack_QIANGZHIAI owner;

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
    private float cd = 0f;
    private void Update()
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
        if (pos.x < -960 || pos.x > 960)
        {
            dir = new Vector2(dir.x*-1,dir.y);
        }
        if (pos.y > 540|| pos.y < -540)
        {
            dir = new Vector2(dir.x, dir.y *-1);
        }
        if(cd>0)
        {
            cd -= Time.deltaTime;
        }
        else
        {
            cd = 0.5f;
            Vector2 dir = new Vector2(ER.RandomNumber.RangeF(), ER.RandomNumber.RangeF());
            RandomBullet lb = (RandomBullet)ObjectPoolManager.Instance.GetObject("RandomBullet");
            lb.ResetState();
            lb.SelfBullet.Damage = 4;
            lb.transform.SetParent(owner.transform);
            lb.transform.localPosition = transform.localPosition;
            lb.waitTime = 2;
            lb.overTime = 10;
            lb.Dir = dir;
            lb.Speed = 200;
            owner.shooted_rd.Add(lb);
            dir *= -1;
            lb = (RandomBullet)ObjectPoolManager.Instance.GetObject("RandomBullet");
            lb.ResetState();
            lb.SelfBullet.Damage = 4;
            lb.transform.SetParent(owner.transform);
            lb.transform.localPosition = transform.localPosition;
            lb.waitTime = 2;
            lb.overTime = 10;
            lb.Dir = dir;
            lb.Speed = 200;
            owner.shooted_rd.Add(lb);
        }
        float angle = Vector2.down.ClockAngle(dir);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}