using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eventone : MonoBehaviour
{
    public GameObject eventone;//检测碰撞的名字
    [SerializeField] private bool eventoneKeys = true;
    private void OnTriggerEnter2D(Collider2D collider)//one是这个碰撞的名字
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            eventone.SetActive(true);
            eventoneKeys = false;
            Time.timeScale = 0;


        }
    }
    public void theeventone()//挂脚本
    {
        SceneManager.LoadScene(1);//跳转到战斗画面1
    }
}
