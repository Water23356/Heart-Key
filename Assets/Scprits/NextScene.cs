using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public GameObject Nextscene;
    [SerializeField] private bool nextKeys = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Nextscene.SetActive(true);
            nextKeys = false;
            Time.timeScale = 0;


        }
    }

    public void nextscene()
    {
        SceneManager.LoadScene(1);
    }

    public void Return()
    {
        Nextscene.SetActive(false);
        nextKeys = true;
        Time.timeScale = 1;
    }
}
