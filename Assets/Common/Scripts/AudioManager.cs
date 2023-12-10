using ER;
using System;
using System.Linq;
using UnityEngine;
[Serializable]
public struct AudioInfo
{
    public string name;
    public AudioClip clip;
}

/// <summary>
/// 音效管理器
/// </summary>
public class AudioManager:MonoSingletonAutoCreate<AudioManager>
{
    [SerializeField]
    private AudioInfo[] audios;
    [SerializeField]
    private ObjectPool pool;
    /// <summary>
    /// 创建一个临时音乐播放器
    /// </summary>
    /// <returns></returns>
    public AudioPlayer CreateTempPlayer(string clipName)
    {
        AudioPlayer player = (AudioPlayer)pool.GetObject();
        AudioClip audio = GetClip(clipName);
        if(audio != null)
        {
            player.SetClip(audio);
        }
        return player;
    }
    /// <summary>
    /// 判断是否存在指定音频片段
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public bool Contains(string clipName)
    {
        foreach(var audio in audios)
        {
            if(audio.name == clipName)
            {
                return true;
            }
        }
        return false;
    }
    public AudioClip GetClip(string clipName)
    {
        foreach (var audio in audios)
        {
            if (audio.name == clipName)
            {
                return audio.clip;
            }
        }
        return null;
    }
}