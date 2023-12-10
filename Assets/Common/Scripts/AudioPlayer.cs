using ER;
using UnityEngine;
/// <summary>
/// 单体音效播放器
/// </summary>
public class AudioPlayer : Water
{
    [SerializeField]
    private AudioSource source;
    public void Play()
    {
        source.Play();
    }
    public void SetClip(AudioClip clip)
    {
        source.clip = clip;
    }

    public override void ResetState()
    {
        
    }

    protected override void OnHide()
    {
        
    }
}