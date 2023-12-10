using ER;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class StartTitle : MonoBehaviour
{
    [SerializeField]
    private CursorMove cursor;
    private int index;
    [SerializeField]
    private SceneTransition transition;
    [SerializeField]
    private AudioSource audio;

    public AudioClip clip;
    public int OptionIndex
    {
        get => index;
        set
        {
            index = value;
            cursor.AimPos = new Vector2(-500,-40 - index  * 90);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Up"))
        {
            OptionIndex = Math.Max(0, index - 1);
            audio.PlayOneShot(clip);
        }
        if(Input.GetButtonDown("Down"))
        {
            OptionIndex = Math.Min(1, index + 1);
            audio.PlayOneShot(clip);
        }
        if(Input.GetButtonDown("Submit"))
        {
            switch(OptionIndex)
            {
                case 0:
                    ER.SceneManager.Instance.LoadScene(new ScrollSceneConfigure(), transition, true);
                    enabled = false;
                    break;
                case 1:
                    Application.Quit();
                    break;

            }
        }
    }
}
