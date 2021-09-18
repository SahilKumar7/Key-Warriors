using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : View
{
    // move music scroll bar
    public void OnMusicVolumeChange(float volume){
        PlayerPrefs.SetFloat(Const.Music, volume);
        Debug.Log(PlayerPrefs.GetFloat(Const.Music));
    }

    // move sound scroll bar
    public void OnSoundVolumeChange(float volume){
        PlayerPrefs.SetFloat(Const.Sound, volume);
        Debug.Log(PlayerPrefs.GetFloat(Const.Sound));
    }

    public override void Show()
    {
        base.Show();
        // initialize Setting Panel
    }
}
