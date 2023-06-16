using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    //오디오믹서 참조
    public AudioMixer m_audio_mixer;

    //전체볼륨
    public Slider m_master_audio_slider;

    //효과음
    public Slider m_VFX_audio_slider;

    //배경음악
    public Slider m_BGM_audio_slider;
    public void MasterAudioControl()
    {
        float sound = m_master_audio_slider.value; //값 저장

        if (sound == -40f) m_audio_mixer.SetFloat("", -80); //맥스값설정, 너무작으면 오류남
        else m_audio_mixer.SetFloat("MasterVolumn", sound);
    }
    public void VFXAudioControl()
    {
        float sound = m_VFX_audio_slider.value;

        if (sound == -40f) m_audio_mixer.SetFloat("", -80);
        else m_audio_mixer.SetFloat("SFXVolumn", sound);
    }
    public void BGMAudioControl()
    {
        float sound = m_BGM_audio_slider.value;

        if (sound == -40f) m_audio_mixer.SetFloat("", -80);
        else m_audio_mixer.SetFloat("BGMVolumn", sound);
    }
}
