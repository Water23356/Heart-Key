using ER;
using UnityEngine;

public class StartConfigure:MonoBehaviour
{
    private void Start()
    {
        SceneManager.Instance.AddScene(new ScrollSceneConfigure());
    }
}