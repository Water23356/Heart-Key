using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 光标移动
/// </summary>
public class CursorMove : MonoBehaviour
{
    [SerializeField]
    private Vector2 aimPos;
    private bool move = false;
    private float speed = 15f;
    public Vector2 AimPos
    {
        get => aimPos;
        set
        {
            aimPos = value;
            move = true;
        }
    }
    public Vector2 Position
    {
        get => transform.localPosition;
    }

    void Start()
    {
        
    }
    private float rotate = 0;
    void Update()
    {
        if(move)
        {
            rotate = 0;
            transform.localEulerAngles = new Vector3(0,0,-90);
            Vector2 dir = aimPos - Position;
            float sp = dir.magnitude * speed;
            if (sp > speed) sp = speed;
            transform.localPosition += (Vector3)dir * sp* Time.deltaTime;
            if(dir.magnitude <= 2)
            {
                move = false; 
            }
        }
        else
        {
            rotate += Time.deltaTime * 180;
            if (rotate > 360)
            {
                rotate -= 360;
            }
            transform.localEulerAngles = new Vector3(rotate, 0, -90);
        }
    }
}
