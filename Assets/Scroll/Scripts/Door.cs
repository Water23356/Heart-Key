using ER;
using ER.Control;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class Door:MonoBehaviour
{
    private PlayerController player;
    public SpriteRenderer image_back;
    public SpriteRenderer image_color;
    public SceneHeart sceneHeart;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == GameText.TAG_PLAYER)
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log($"接触玩家:{player != null}");
            player.interactive += EnterNextScene;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == GameText.TAG_PLAYER)
        {
            player.interactive -= EnterNextScene;
        }
    }

    private void EnterNextScene()
    {
        MonoAnchor anchor = (MonoAnchor) AM.GetAnchor("CanvasTransition");
        SceneTransition st= anchor.GetComponent<SceneTransition>();
        st.EnterAnimation();
        ControlManager.Instance.UnregisterPower(player);
        Invoke("SetPosition",1f);
        Invoke("OverLoad", 2f);
    }
    private void SetPosition()
    {
        player.transform.position = new Vector3(100,0,0);
        image_color.color = Color.white;
        image_back.gameObject.SetActive(true);
        sceneHeart.enabled = true;
    }

    private void OverLoad()
    {
        MonoAnchor anchor = (MonoAnchor)AM.GetAnchor("CanvasTransition");
        SceneTransition st = anchor.GetComponent<SceneTransition>();
        st.ExitAnimation();
    }
}