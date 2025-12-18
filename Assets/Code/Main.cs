using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Main : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider audioSlider;  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicManager.Instance.PlayMusic("Magic");
        LoadVolume();

    }
    public void Play() { 
        MusicManager.Instance.PlayMusic("Magic");
        SceneManager.LoadScene("Levels");
    }
    public void Quit() {
        Application.Quit();
    }   
    // Update is called once per frame
    
    public void UploadMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UploadAudioVolume(float volume) {         
        audioMixer.SetFloat("SoundsVolume", volume);
    }   

    public void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SoundsVolume", out float soundsVolume);
        PlayerPrefs.SetFloat("SoundsVolume", soundsVolume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        audioSlider.value = PlayerPrefs.GetFloat("SoundsVolume");
    }
    void Update()
    {
        
        
    }
}
