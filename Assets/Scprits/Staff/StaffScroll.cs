using ER.UI;
using UnityEngine;

public class StaffScroll:MonoBehaviour
{
    public RigidbodyFollowCamera camera;
    public float speed = 1f;
    private void Start()
    {
        camera.follow = false;
        camera.transform.position= new Vector3(100, 2.5f,-8);
        camera.aimPosition = new Vector2 (100, 2.5f);
    }
    private void Update()
    {
        camera.aimPosition += new Vector2(1,0) * speed * Time.deltaTime;
    }
}