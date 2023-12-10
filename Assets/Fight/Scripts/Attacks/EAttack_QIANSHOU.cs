using ER;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EAttack_QIANSHOU:MonoBehaviour,IEnemyAttack
{
    private Action callback;
    private EnemyAttack owner;

    private float timer = 0;//计时器
    private float times = 0;//攻击次数
    private float wait_time = 1f;//攻击前空白等待时间
    private int state = 0;//攻击状态: 0:攻击前等待 1:行动 2:射弹
    [SerializeField]
    private Bullet MainBullet;
    [SerializeField]
    private Bullet MainBullet2;
    [SerializeField]
    private ObjectPool BulletPool;
    private List<LineBullet> shooted = new List<LineBullet>();
    [SerializeField]
    private float speed = 1000f;

    private void Awake()
    {
        MainBullet.Damage = 12;
        MainBullet2.Damage = 12;
    }
    public void StartAttack(EnemyAttack _owner, Action _callback)
    {
        state = 0;
        owner = _owner;
        callback = _callback;
        timer = wait_time;
        times = 0;
        //MainBullet.gameObject.SetActive(true);
        //MainBullet2.gameObject.SetActive(true);
        MainBullet.transform.localEulerAngles = Vector3.zero;
        MainBullet2.transform.localEulerAngles = Vector3.zero;
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
        callback?.Invoke();
    }

    private LineBullet GetBullet()
    {
        LineBullet lb = (LineBullet)BulletPool.GetObject(true);
        lb.transform.SetParent(transform);
        shooted.Add(lb);
        return lb;
    }

    private int shoot_times = 0;
    private Vector2 startPos_1 = new Vector3(-340, 180);
    private Vector2 startPos_2 = new Vector3(340, -180);
    private Vector2 move_dir_1 = Vector2.right;
    private Vector2 move_dir_2 = Vector2.left;
    private float cd = 0f;
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            switch (state)
            {
                case 1:
                    MainBullet.transform.localPosition += (Vector3)move_dir_1 * speed*Time.deltaTime;
                    MainBullet2.transform.localPosition += (Vector3)move_dir_2 * speed * Time.deltaTime;
                    cd -= Time.deltaTime;
                    if(cd<=0)
                    {
                        LineBullet lb = GetBullet();
                        lb.transform.localPosition = MainBullet.transform.localPosition;
                        lb.Dir = (owner.PlayerPos - (Vector2)lb.transform.localPosition);
                        lb.Speed = 0;
                        lb.overTime = 15f;
                        lb.SelfBullet.Damage = 6;

                        lb = GetBullet();
                        lb.transform.localPosition = MainBullet2.transform.localPosition;
                        lb.Dir = (owner.PlayerPos - (Vector2)lb.transform.localPosition);
                        lb.Speed = 0;
                        lb.overTime = 15f;
                        lb.SelfBullet.Damage = 6;
                        cd = 0.05f;
                    }
                    break;
                case 2:
                    break;
            }
        }
        else
        {
            switch (state)
            {
                case 0:
                    state = 1;
                    timer = 1f;
                    MainBullet.transform.localPosition = startPos_1;
                    MainBullet2.transform.localPosition = startPos_2;
                    MainBullet.gameObject.SetActive(true);
                    MainBullet2.gameObject.SetActive(true);
                    break;
                case 1:
                    state = 2;
                    timer = 1f;
                    for (int i = 0; i < shooted.Count; i++)
                    {
                        if (shooted[i] != null)
                        {
                            shooted[i].Speed = speed * 2;
                        }
                    }
                    MainBullet.gameObject.SetActive(false);
                    MainBullet2.gameObject.SetActive(false);
                    break;
                case 2:
                    times++;
                    if (times >= 4)
                    {
                        StopAttack();
                    }
                    else
                    {
                        timer = 0.8f;
                        state = 0;
                        move_dir_1 *= -1;
                        move_dir_2 *= -1;
                        startPos_1 = new Vector2(startPos_1.x * -1, startPos_1.y) ;
                        startPos_2 = new Vector2(startPos_2.x * -1, startPos_2.y);
                        MainBullet.transform.localEulerAngles += new Vector3(0,0,180);
                        MainBullet2.transform.localEulerAngles += new Vector3(0, 0, 180);
                    }
                    break;
            }
        }
    }
}