using UnityEngine;

public class SceneTransition : ER.SceneTransition
{
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public override void EnterTransition()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("open");
        timer = 0;
        opening = true;
    }

    public override void ExitTransition()
    {
        animator.SetTrigger("close");
    }
    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }

    private float timer = 0;
    private bool state = false;
    private bool opening = false;
    private void Update()
    {
        if (opening)
        {
            timer += Time.deltaTime;
            if (timer >= 1f && !state)
            {
                callback.Invoke();
                state = true;
            }
            Debug.Log("prograss:" + Progress);
            if (timer > 2f && Progress >= 1f)
            {
                ExitTransition();
                opening = false;
            }
        }
    }

    public void EnterAnimation()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("open");
    }
    public void ExitAnimation()
    {
        animator.SetTrigger("close");
    }
}