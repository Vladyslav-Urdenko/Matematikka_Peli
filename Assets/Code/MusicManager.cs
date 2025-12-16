using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private MusicLibrary musicLibrary;
    [SerializeField]
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(string trackName, float crossfadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusicCrossFade(musicLibrary.GetClipFromName(trackName), crossfadeDuration));  
    }
    IEnumerator AnimateMusicCrossFade(AudioClip newClip, float duration = 0.5f)
    {
        float currentTime = 0f;
        float startVolume = musicSource.volume;
        // Fade out
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, currentTime / duration);
            yield return null;
        }
        musicSource.clip = newClip;
        musicSource.Play();
        currentTime = 0f;
        // Fade in
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, startVolume, currentTime / duration);
            yield return null;
        }
    }

}
