using ER;
using UnityEngine;

public class BackStartTitle:MonoBehaviour
{
    [SerializeField]
    private SceneTransition transition;
    private void Start()
    {
        SceneManager.Instance.LoadScene(new StartConfigure(), transition,true);
    }
}