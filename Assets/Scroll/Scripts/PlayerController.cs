using ER.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 横版角色控制器
/// </summary>
public class PlayerController : MonoControlPanel
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// 交互委托
    /// </summary>
    public event Action interactive;

    protected override void Awake()
    {
        base.Awake();
        ControlManager.Instance.RegisterPower(this);
    }

    private void Update()
    {
        if(IsEnable)
        {
            float x = UnityEngine.Input.GetAxis("Horizontal");
            transform.position += new Vector3(x, 0, 0) * speed * Time.deltaTime;
            if (x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (x > 0)
            {
                spriteRenderer.flipX = false;
            }
            if (x != 0)
            {
                animator.SetBool("move", true);
            }
            else
            {
                animator.SetBool("move", false);
            }
            if (Input.GetButtonDown("Submit"))
            {
                interactive?.Invoke();
            }
        }
    }
}
