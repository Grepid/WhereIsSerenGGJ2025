using AudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider slider;
    private void Update()
    {
        //UpdateAudio();        
    }
    private void OnEnable()
    {
        if (AudioManager.Instance == null) return;
        slider.value = AudioManager.SoundTypeVolume(SoundType.Master);
        
    }
    public void UpdateAudio()
    {
        if (slider != null)
        {
            AudioManager.SetTypeVolume(SoundType.Master, slider.value);
            AudioManager.UpdateAllAudio();
        }
    }
}
