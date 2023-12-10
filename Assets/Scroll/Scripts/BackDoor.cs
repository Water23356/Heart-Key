
using ER.Control;
using ER;
using UnityEngine;

public class BackDoor : MonoBehaviour
{
    private PlayerController player;
    public SpriteRenderer image_back;
    public SpriteRenderer image_color;
    public SceneHeart sceneHeart;
    [SerializeField]
    private ScrollSystem system;
    private bool over = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (over) return;
        string tag = collision.tag;
        if (tag == GameText.TAG_PLAYER)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log($"接触玩家:{player != null}");
            EnterNextScene();
            over = true;
        }
    }
    private void EnterNextScene()
    {
        MonoAnchor anchor = (MonoAnchor)AM.GetAnchor("CanvasTransition");
        SceneTransition st = anchor.GetComponent<SceneTransition>();
        st.EnterAnimation();
        ControlManager.Instance.UnregisterPower(player);
        Invoke("SetPosition", 1f);
        Invoke("OverLoad", 2f);
    }
    private void SetPosition()
    {
        player.transform.position = new Vector3(290, 0, 0);
        image_color.color = Color.black;
        image_back.gameObject.SetActive(false);
        sceneHeart.enabled = false;
    }

    private void OverLoad()
    {
        MonoAnchor anchor = (MonoAnchor)AM.GetAnchor("CanvasTransition");
        SceneTransition st = anchor.GetComponent<SceneTransition>();
        st.ExitAnimation();
    }
}