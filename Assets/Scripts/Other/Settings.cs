using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
using TMPro;
using UnityEngine.Audio;


public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup audio_mixer;
    public static int previous_scene_index;

    public TMP_Dropdown resolution_dropdown;
    public TMP_Dropdown quality_dropdown;
    public Toggle fullscreen_toggle;

    public Toggle volume_music_toggle;
    public Slider volume_music_slider;
    public float volume_music;

    public Toggle volume_effects_toggle;
    public Slider volume_effects_slider;
    public float volume_effects;
   

    Resolution[] resolutions;

    public void Start()
    {
        resolution_dropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        int current_resolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                current_resolution = i;
            }
        }

        resolution_dropdown.AddOptions(options);
        resolution_dropdown.RefreshShownValue();


        LoadSettings(current_resolution);

   
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(isFullScreen));
    }

    public void SetResolution(int resolution_index)
    {
        Resolution resolution = resolutions[resolution_index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int quality_index)
    {
        QualitySettings.SetQualityLevel(quality_index);
    }

    public void BackSetting()
    {
        SceneManager.LoadScene(5);
    }


    public void SetVolumeMusic()
    {
     
        volume_music = volume_music_slider.value;

        if (volume_music == 0)
        {
            audio_mixer.audioMixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            audio_mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume_music));
        }

        VolumeValueMusic();
    }

    public void TurnOnOffVolumeMusic()
    {
 
        if (volume_music_toggle.isOn)
        {
            volume_music = 1; 
        }
        else
        {
            volume_music = 0; 
        }


        volume_music_slider.value = volume_music;


        VolumeValueMusic();
    }

    public void VolumeValueMusic()
    {
        
        if (volume_music == 0)
        {
            audio_mixer.audioMixer.SetFloat("MusicVolume", -80); 
        }
        else
        {
            audio_mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume_music)); 
        }
    }

    public void SetVolumeEffects()
    {
        volume_effects = volume_effects_slider.value; 

        if (volume_effects == 0)
        {
            audio_mixer.audioMixer.SetFloat("EffectsVolume", -80);
        }
        else
        {
            audio_mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, volume_effects));
        }

        VolumeValueEffects();
    }

    public void TurnOnOffVolumeEffects()
    {
        if (volume_effects_toggle.isOn)
        {
            volume_effects = 1;
        }
        else
        {
            volume_effects = 0;
        }

        volume_effects_slider.value = volume_effects; 

        VolumeValueEffects();
    }

    public void VolumeValueEffects()
    {
        if (volume_effects == 0)
        {
            audio_mixer.audioMixer.SetFloat("EffectsVolume", -80);
        }
        else
        {
            audio_mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, volume_effects));
        }
    }


    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingsPreference", quality_dropdown.value);
        PlayerPrefs.SetInt("ResulutionPreference", resolution_dropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumeMusicPreference", volume_music_slider.value);
        PlayerPrefs.SetFloat("VolumeEffectsPreference", volume_effects_slider.value);
    }

    public void LoadSettings(int current_resolution_index)
    {
        if (PlayerPrefs.HasKey("QualitySettingsPreference"))
            quality_dropdown.value = PlayerPrefs.GetInt("QualitySettingsPreference");
        else
            quality_dropdown.value = 2;


        if (PlayerPrefs.HasKey("ResulutionPreference"))
            resolution_dropdown.value = PlayerPrefs.GetInt("ResulutionPreference");
        else
            resolution_dropdown.value = current_resolution_index;


        if (PlayerPrefs.HasKey("FullscreenPreference"))
        {
            bool isFullscreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            Screen.fullScreen = isFullscreen;
            fullscreen_toggle.isOn = isFullscreen;
        }
        else
        {
            Screen.fullScreen = true;
            fullscreen_toggle.isOn = true;
        }

        if (PlayerPrefs.HasKey("VolumeMusicPreference"))
        {
            volume_music = PlayerPrefs.GetFloat("VolumeMusicPreference");
            volume_music_slider.value = volume_music; 
            audio_mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume_music));
        }

        if (PlayerPrefs.HasKey("VolumeEffectsPreference"))
        {
            volume_effects = PlayerPrefs.GetFloat("VolumeEffectsPreference");
            volume_effects_slider.value = volume_effects; 
            audio_mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, volume_effects));
        }

    }

   
}
