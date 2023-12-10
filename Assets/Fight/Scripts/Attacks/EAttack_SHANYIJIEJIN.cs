using ER;
using System;
using UnityEngine;

public class EAttack_SHANYIJIEJIN : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private Bullet[] bulltes;
    private float timer = 0f;
    private Action callback;
    private EnemyAttack owner;
    private int times = 0;
    private int state = 0;
    //0: 空白等待 1:射弹等待 2 等待
    private float distance = 150;
    private Vector2 aimPos = Vector2.zero;

    public void StartAttack(EnemyAttack _owner, Action _callback)
    {
        owner = _owner;
        callback = _callback;
        times = 0;
        timer = 0.5f;
        state = 0;
        enabled = true;
        gameObject.SetActive(true);
        for (int i = 0; i < bulltes.Length; i++)
        {
            bulltes[i].GetComponent<Attack_SHANYIJIEJIN_Remote>().owner = owner;
        }
    }

    public void StopAttack()
    {
        gameObject.SetActive(false);
        enabled = false;
        callback?.Invoke();
        for (int i = 0; i < bulltes.Length; i++)
        {
            bulltes[i].gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < bulltes.Length; i++)
        {
            bulltes[i].Damage = 1;
        }
    }
    private float prograss = 0;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
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
                        bulltes[i].transform.localPosition = owner.PlayerPos + new Vector2(0, 1).Rotate(MathF.PI / 4 * i) * distance;
                    }
                    state = 1;
                    timer = 3f;
                    times++;
                    break;
                case 1:
                    StopAttack();
                    break;
            }
        }
    }
}