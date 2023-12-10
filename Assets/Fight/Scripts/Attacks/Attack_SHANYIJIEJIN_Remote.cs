using Unity.VisualScripting;
using UnityEngine;

public class Attack_SHANYIJIEJIN_Remote:MonoBehaviour
{
    public EnemyAttack owner;
    private float distance = 100f;
    private float speed = 2000f;
    private void Update()
    {
        Vector2 dis = (Vector2)transform.localPosition - owner.PlayerPos;
        if (dis.magnitude < distance)
        {
            transform.localPosition += new Vector3(dis.normalized.x, dis.normalized.y,0) * speed * Time.deltaTime;
        }
    }
}