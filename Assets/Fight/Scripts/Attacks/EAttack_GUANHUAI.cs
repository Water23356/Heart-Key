using System;
using UnityEngine;

public class EAttack_GUANHUAI:MonoBehaviour,IEnemyAttack
{
    [SerializeField]
    private Bullet[] Attacker;

    private float timer = 0f;
    private Action callback;
    private EnemyAttack owner;
    private float speed = 3000f;//弹幕速度
    private int times = 0;
    private int state = 0;//0: 攻击空白等待 1:弹幕等待 2:发射等待
    public void StartAttack(EnemyAttack _owner, Action _callback)
    {
        owner = _owner;
        callback = _callback;
        timer = 1.5f;
        times = 0;
        state = 0;
        enabled = true;
        gameObject.SetActive(true);
    }

    public void StopAttack()
    {
        gameObject.SetActive(false);
        enabled = false;
        for(int i=0;i<Attacker.Length;i++)
        {
            Attacker[i].gameObject.SetActive(false);
        }
        callback?.Invoke();
    }
    private void Awake()
    {
        for(int i=0;i<Attacker.Length;i++)
        {
            Attacker[i].Damage = 5;
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            switch (state)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    for (int i = 0; i < Attacker.Length; i++)
                    {
                        Attacker[i].transform.localPosition -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
                    }
                    if (Attacker[0].transform.localPosition.y < -500)
                        timer = -1;
                    break;
            }
        }
        else
        {
            switch (state)
            {
                case 0:
                    for(int i=0;i<Attacker.Length;i++)
                    {
                        Attacker[i].gameObject.SetActive(true);
                        Attacker[i].gameObject.tag = GameText.TAG_BULLET;
                        Attacker[i].transform.localPosition = new Vector3(ER.RandomNumber.RangeF(-320, 320), 180, 0);
                    }
                    timer = 1.5f;
                    state = 1;
                    break;
                case 1:
                    state = 2;
                    timer = 5f;
                    break;
                case 2:
                    timer = 0.5f;
                    state = 0;
                    times++;
                    if(times>=4)
                    {
                        StopAttack();
                    }
                    break;
            }
        }
    }
}