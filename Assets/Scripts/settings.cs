using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class settings : MonoBehaviour {


    GameObject GO;
    //bool isOn;
    //public Toggle myToggle;

    public AudioMixer mainMixer;
    float volumeSound;
    float volumeMusic;

    GameObject musicSlider;
    GameObject soundSlider;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        musicSlider = GameObject.Find("musicSlider");
        soundSlider = GameObject.Find("soundSlider");
    }
    public void SetMainVolume(float mainVolume)
    {
        mainMixer.SetFloat("mainVolume", mainVolume);
        volumeSound = mainVolume;
        
    }
    public void SetMusicVolume(float musicVolume)
    {
        mainMixer.SetFloat("musicVolume", musicVolume);
        volumeMusic = musicVolume;
    }
    public void SetQuality(bool toggled)
    {
        GO = GameObject.Find("qualityToggle");
        if (GO.GetComponent<Toggle>().isOn == true)
        {
            QualitySettings.SetQualityLevel(0);
        }
        else
        {
            QualitySettings.SetQualityLevel(1);
        }
    }
    public void turnOff()
    {
        
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            gameObject.GetComponentInChildren<Image>().enabled = false;
            if (gameObject.name.Contains("music"))
            {
                mainMixer.SetFloat("musicVolume", -80);
                musicSlider.GetComponent<Slider>().enabled = false;
                musicSlider.GetComponent<Slider>().value = -80;
            }
            else if (gameObject.name.Contains("sound"))
            {
                mainMixer.SetFloat("mainVolume", -80);
                soundSlider.GetComponent<Slider>().enabled = false;
                soundSlider.GetComponent<Slider>().value = -80;
            }
        }
        else
        {
            gameObject.GetComponentInChildren<Image>().enabled = true;
            if (gameObject.name.Contains("music"))
            {
                mainMixer.SetFloat("musicVolume", volumeMusic);
                musicSlider.GetComponent<Slider>().enabled = true;
                musicSlider.GetComponent<Slider>().value = volumeMusic;
            }
            else if (gameObject.name.Contains("sound"))
            {
                mainMixer.SetFloat("mainVolume", volumeSound);
                soundSlider.GetComponent<Slider>().enabled = true;
                soundSlider.GetComponent<Slider>().value = volumeSound;
            }
        }

    }

}

