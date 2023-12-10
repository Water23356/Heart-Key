using ER;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EAttack_QIANGZHIAI : MonoBehaviour, IEnemyAttack
{

    private Action callback;
    private EnemyAttack owner;

    private float timer = 0;//计时器
    private float times = 0;//攻击次数
    private float wait_time = 1f;//攻击前空白等待时间
    private int state = 0;//攻击状态: 0:攻击前等待 1:行动 2:射弹
    private float speed = 300f;
    [SerializeField]
    private ObjectPool JuBulletPool;

    private List<BulletBoss> shooted = new List<BulletBoss>();
    public List<RandomBullet> shooted_rd = new List<RandomBullet>();
    public void StartAttack(EnemyAttack _owner, Action _callback)
    {
        state = 0;
        owner = _owner;
        callback = _callback;
        timer = wait_time;
        times = 0;
        gameObject.SetActive(true);
        enabled = true;
    }
    public void StopAttack()
    {
        gameObject.SetActive(false);
        enabled = false;
        for (int i = 0; i < shooted.Count; i++)
        {
            if (shooted[i] != null && shooted[i].gameObject.activeSelf)
            {
                shooted[i].Destroy();
            }
        }
        shooted.Clear();
        for (int i = 0; i < shooted_rd.Count; i++)
        {
            if (shooted_rd[i] != null && shooted_rd[i].gameObject.activeSelf)
            {
                shooted_rd[i].Destroy();
            }
        }
        shooted_rd.Clear();
        callback?.Invoke();
    }
    private float cd = 0;

    private BulletBoss GetBullet()
    {
        BulletBoss lb = (BulletBoss)JuBulletPool.GetObject(true);
        lb.transform.SetParent(transform);
        lb.transform.localPosition = new Vector3(0,400,0);
        lb.Dir = (owner.PlayerPos - (Vector2)lb.transform.localPosition).normalized;
        lb.SelfBullet.Damage = 8;
        lb.Speed = speed;
        lb.owner = this;
        shooted.Add(lb);
        return lb;
    }
    private void Update()
    {
        
        if(timer>0)
        {
            timer -= Time.deltaTime;
            switch(state)
            {
                case 1:
                    cd -= Time.deltaTime;
                    if(cd<=0)
                    {
                        GetBullet();
                        cd = 2f;
                    }
                    break;
            }
        }
        else
        {
            switch (state)
            {
                case 0:
                    state = 1;
                    timer = 20f;
                    break;
                case 1:
                    StopAttack();
                    break;
            }
        }
    }
}