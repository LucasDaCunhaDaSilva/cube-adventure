using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private const string MUSIC_VOLUME_KEY = "vol_music";
    private const string SFX_VOLUME_KEY = "vol_sfx";
    private const string UI_VOLUME_KEY = "vol_ui";


    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    private void SetVolume(string key, float value, bool sfx = true)
    {    
        float linearToDecibel(float value) { return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20; }
    
        mixer.SetFloat(key, linearToDecibel(value));
        PlayerPrefs.SetFloat(key, value);
        if(sfx) UISoundManager.Instance.OnSliderChange();
    }

    private void Start()
    {
        float musicVol = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.75f);
        SetVolume(MUSIC_VOLUME_KEY, musicVol, false);
        
        musicSlider.value = musicVol;
        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener((vol) =>  SetVolume(MUSIC_VOLUME_KEY, vol));


        float sfxVol = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.75f);
        SetVolume(SFX_VOLUME_KEY, sfxVol, false);

        sfxSlider.value = sfxVol;
        sfxSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.AddListener((vol) =>  SetVolume(SFX_VOLUME_KEY, vol) );


        float uiVol = PlayerPrefs.GetFloat(UI_VOLUME_KEY, 0.75f);
        SetVolume(UI_VOLUME_KEY, uiVol, false);

        uiSlider.value = uiVol;
        uiSlider.onValueChanged.RemoveAllListeners();
        uiSlider.onValueChanged.AddListener((vol) => SetVolume(UI_VOLUME_KEY, vol) );
    }

}
