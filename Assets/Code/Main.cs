using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicManager.Instance.PlayMusic("Magic");

    }
    public void Play() { 
        MusicManager.Instance.PlayMusic("Magic");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
