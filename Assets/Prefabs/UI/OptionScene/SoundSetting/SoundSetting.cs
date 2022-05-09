using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public AudioMixer m_audio_mixer;

    public Slider m_master_audio_slider;
    public Slider m_VFX_audio_slider;
    public Slider m_BGM_audio_slider;
    public void MasterAudioControl()
    {
        float sound = m_master_audio_slider.value;

        if (sound == -40f) m_audio_mixer.SetFloat("", -80);
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
