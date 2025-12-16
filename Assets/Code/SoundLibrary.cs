using UnityEngine;

[System.Serializable]
public struct SoundEffect
{
    public string groupID;
    public AudioClip [] clips;
}
public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip GetClipFromName(string name)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.groupID == name)
            {
                int randomIndex = Random.Range(0, soundEffect.clips.Length);
                return soundEffect.clips[randomIndex];
            }
        }
        Debug.LogWarning($"Sound group ID '{name}' not found.");
        return null;
    }
}
