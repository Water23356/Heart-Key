using ER;
using System;
using UnityEditor;
using UnityEngine;

public class EAttack_YONGBAO : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private Bullet[] bulltes;
    private float timer = 0f;
    private Action callback;
    private EnemyAttack owner;
    private int times = 0;
    private int state = 0;
    //0: 空白等待 1:射弹前等待 2:弹道等待
    private float distance = 150;
    private Vector2 aimPos = Vector2.zero;

    public void StartAttack(EnemyAttack _owner, Action _callback)
    {
        owner = _owner;
        callback = _callback;
        times = 0;
        timer = 1;
        enabled = true;
        gameObject.SetActive(true); ;
    }

    public void StopAttack()
    {
        gameObject.SetActive(false);
        enabled = false;
        callback?.Invoke();
    }

    private void Awake()
    {
        for(int i=0;i<bulltes.Length;i++)
        {
            bulltes[i].Damage = 4;
        }
    }
    private float prograss = 0;
    private void Update()
    {
        timer -= Time.deltaTime*1f;
        if (timer > 0)
        {
            switch (state)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    prograss+=Time.deltaTime;
                    for(int i=0;i<bulltes.Length;i++)
                    {
                        bulltes[i].transform.localPosition += (Vector3)(aimPos - (Vector2)bulltes[i].transform.localPosition) * prograss;
                    }
                    if(prograss >= 1)
                    {
                        timer =- 1;
                    }
                    break;
            }
        }
        else
        {
            switch (state)
            {
                case 0:
                    aimPos = owner.PlayerPos;
                    for (int i = 0; i < bulltes.Length; i++)
                    {
                        bulltes[i].gameObject.SetActive(true);
                        bulltes[i].gameObject.tag = GameText.TAG_BULLET;
                        bulltes[i].transform.localPosition = owner.PlayerPos + new Vector2(0, 1).Rotate(MathF.PI/3*i) * distance;
                    }
                    state = 1;
                    timer = 1f;
                    times++;
                    break;
                case 1:
                    prograss = 0;
                    state = 2;
                    timer = 1f;
                    break;
                case 2:
                    if(times>=4)
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
