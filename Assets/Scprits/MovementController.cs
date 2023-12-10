using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]//�¼ӵģ���ɾ
public class MovementController : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction = Vector2.down;//ԭ����private
    public float speed = 5f;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    public AnimateSpriteRenderer spriteRendererLeft;
    public AnimateSpriteRenderer spriteRendererRight;
    private AnimateSpriteRenderer activeSpriteRenderer;
    public LayerMask obstacleLayer;//�Զ����ӽ�
    public Vector2 nextDirection { get; private set; }//�Զ����ӽ�


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererRight;
    }
    private void Update()
    {
        if (this.nextDirection != Vector2.zero)//ת���˿��
        {
            SetDirection(this.nextDirection);//ת���˿��
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }

    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }
    public void SetDirection(Vector2 newDirection, AnimateSpriteRenderer spriteRenderer)
    {
        direction = newDirection;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);//��С�˲�������ǽ������ס��ת���˿��
        return hit.collider != null;
    }
    public void SetDirection(Vector2 direction, bool forced = false)//���������¶���Ϊ���ںϳԶ�������淨�ű�������������������ôȫ��bug��
    {
        if (forced || !Occupied(direction))//ת���˿��
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }
    public void Start()
    {
       // ResetState();
    }
}
