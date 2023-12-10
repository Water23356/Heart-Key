using System;
using UnityEditor;
using UnityEngine;

public class EAttack_YUEHU : MonoBehaviour,IEnemyAttack
{
    [SerializeField]
    private GameObject Defence;
    [SerializeField]
    private Bullet Sword;

    private float timer = 0f;
    private Action callback;
    private EnemyAttack owner;
    private float speed = 300f;//剑挥动速度

    private int state = 0;//0: 盾牌等待 1:剑等待 
    public void StartAttack(EnemyAttack _owner, Action _callback)
    {
        owner = _owner; 
        callback = _callback;
        timer = 2.5f;
        state = 0;
        Defence.transform.localPosition = new Vector2(ER.RandomNumber.RangeF(-280,280), ER.RandomNumber.RangeF(-170,170));//随机生成盾牌位置
        Defence.SetActive(true);
        enabled = true;
        gameObject.SetActive(true);
    }

    public void StopAttack()
    {
        gameObject.SetActive(false);
        enabled = false;
        callback?.Invoke();
    }
    private void Awake()
    {
        Sword.Damage = 5;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer>0)
        {
            switch(state)
            {
                case 0:
                    break;
                case 1:
                    Sword.transform.localEulerAngles += new Vector3(0,0, Time.deltaTime * speed);
                    if(Sword.transform.localEulerAngles.z >= 90 && Sword.transform.localEulerAngles.z<160)
                    {
                        Debug.Log("终止挥动");
                        Sword.transform.localEulerAngles = new Vector3(0, 0, 90);
                        timer = -1;
                    }
                    break;
            }
        }
        else
        {
            switch(state)
            {
                case 0:
                    state = 1;
                    Sword.transform.localEulerAngles = new Vector3(0,0,-90);
                    Sword.gameObject.SetActive(true);
                    Sword.gameObject.tag = GameText.TAG_BULLET;
                    timer = 5f;
                    break;
                case 1:
                    Sword.gameObject.SetActive(false);
                    StopAttack();
                    break;
            }
        }
    }
}