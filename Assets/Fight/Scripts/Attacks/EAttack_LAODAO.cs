using ER;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EAttack_LAODAO : MonoBehaviour, IEnemyAttack
{
    private int state = 0;//0: 攻击前空白 1:执行攻击阶段_1&攻击阶段2前准备 2:执行攻击阶段2 3:攻击后空白时间
    [SerializeField]
    private Bullet MainBullet;
    [SerializeField]
    private ObjectPool BulletPool;
    private EnemyAttack owner;
    private Action callback;
    private float timer = 0;
    [SerializeField]
    private float speed = 500f;
    [SerializeField]
    private float distance = 50f;
    private int times = 0;

    private List<LineBullet> shooted = new List<LineBullet>();

    private void Awake()
    {
        MainBullet.Damage = 20;
    }
    public void StartAttack(EnemyAttack _owner, Action _callback)
    {
        state = 0;
        owner = _owner;
        callback = _callback;
        timer = 1f;
        times = 0;
        MainBullet.gameObject.SetActive(true);
        gameObject.SetActive(true);
        enabled = true;
    }

    public void StopAttack()
    {
        gameObject.SetActive(false);
        enabled = false;
        for(int i=0;i< shooted.Count;i++)
        {
            if (shooted[i] !=null &&shooted[i].gameObject.activeSelf)
            {
                shooted[i].Destroy();
            }
        }
        callback?.Invoke();
    }

    private LineBullet GetBullet()
    {
        LineBullet lb = (LineBullet)BulletPool.GetObject(true);
        lb.transform.SetParent(transform);
        shooted.Add(lb);
        return lb;
    }

    private float cd = 0.05f;
    private int shoot_times = 0;
    private void Update()
    {
        if (timer>0)
        {
            timer -= Time.deltaTime;
            switch(state)
            {
                case 2:
                    cd -= Time.deltaTime;
                    if(cd<=0)
                    {
                        Vector2 dir = (owner.PlayerPos - (Vector2)MainBullet.transform.localPosition).normalized;
                        LineBullet bullet = GetBullet();
                        bullet.transform.localPosition = MainBullet.transform.localPosition ;
                        bullet.Dir = dir;
                        bullet.Speed = speed;
                        bullet.SelfBullet.Damage = 5;
                        shoot_times++;
                        cd = 0.05f;
                    }
                    if(shoot_times>=5)
                    {
                        timer = -1;
                    }
                    break;
            }
        }
        else
        {
            switch (state)
            {
                case 0:
                    int count = 8;
                    for (int i=0;i<count;i++)
                    {
                        Vector2 dir = (Vector2.left.Rotate(Mathf.PI / (count-1) * i)).normalized;
                        LineBullet bullet = GetBullet();
                        bullet.transform.localPosition = MainBullet.transform.localPosition + (Vector3)dir * distance;
                        bullet.Dir = dir.Rotate(-Mathf.PI/180*30);
                        bullet.Speed = speed / 10;
                        bullet.SelfBullet.Damage = 5;

                        LineBullet bullet2 = GetBullet();
                        bullet2.transform.localPosition = MainBullet.transform.localPosition + (Vector3)dir * distance;
                        bullet2.Dir = dir.Rotate(Mathf.PI / 180 * 30); ;
                        bullet2.Speed = speed / 10;
                        bullet2.SelfBullet.Damage = 5;
                    }
                    timer = 1;
                    state = 1;
                    break;
                case 1:
                    state = 2;
                    timer = 10;
                    shoot_times = 0;
                    break;
                case 2:
                    times++;
                    if (times >= 4)
                    {
                        timer = 2;
                        state = 3;
                    }
                    else
                    {
                        timer = 1f;
                        state = 0;
                    }
                    break;
                case 3:
                    StopAttack();
                    break;
            }
        }
    }
}