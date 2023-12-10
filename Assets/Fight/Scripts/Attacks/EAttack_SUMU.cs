using System;
using UnityEditor;
using UnityEngine;
/// <summary>
/// Boss_1 攻击_ 肃穆
/// </summary>
public class EAttack_SUMU : MonoBehaviour, IEnemyAttack
{
    private Action callback;
    [SerializeField]
    private Transform warning;//预警框
    [SerializeField]
    private Bullet attacker;//攻击区

    private EnemyAttack owner;

    private float timer = 0;//计时器
    private float times = 0;//攻击次数
    private float wait_time = 1f;//攻击前空白等待时间
    private int state = 0;//攻击状态: 0:攻击前等待 1:预警线等待 2:攻击过程&攻击等待
    private float speed = 2500f;//剑下落的速度
    [SerializeField]
    private float minPosY = 100;
    private float maxPos = 450;
    private float warningPos = -150;

    private void Awake()
    {
        attacker.Damage = 5;
    }

    public void StartAttack(EnemyAttack _owner,Action _callback)
    {
        owner = _owner;
        callback = _callback;
        times = 0;
        timer = wait_time;
        state = 0;
        enabled = true;
        gameObject.SetActive(true);
    }

    public void StopAttack()
    {
        gameObject.SetActive(false);
        enabled = false;
        callback?.Invoke();
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if(state == 2 && attacker.transform.localPosition.y >= minPosY)
            {
                attacker.transform.localPosition -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
            }
        }
        else
        {

            switch (state)
            {
                case 0:
                    state = 1;
                    timer = 2f;//警示2s
                    warning.localPosition = new Vector3(owner.PlayerPos.x, warningPos, 0);//设置预警线位置
                    warning.gameObject.SetActive(true);

                    break;
                case 1://预警等待->进入攻击
                    state = 2;
                    timer = 1f;
                    attacker.tag = GameText.TAG_BULLET;//重置擦弹标签
                    attacker.transform.localPosition = new Vector3(warning.localPosition.x, maxPos, 0);
                    warning.gameObject.SetActive(false);
                    attacker.gameObject.SetActive(true);
                    break;
                case 2://退出攻击
                    times++;
                    attacker.gameObject.SetActive(false);
                    if (times >= 3)
                    {
                        StopAttack();
                    }
                    else
                    {
                        state = 0;
                    }

                    break;
            }
        }
    }
        
    
}