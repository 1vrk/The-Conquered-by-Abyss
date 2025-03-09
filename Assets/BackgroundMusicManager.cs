using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance; 

    public AudioClip menuMusic; 
    public AudioClip gameMusic; 
    public AudioClip settingsMusic; 

    private AudioSource audioSource;

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
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "AuthScene 1":
                PlayMusic(menuMusic);
                break;
            case "MM":
                PlayMusic(menuMusic);
                break;
            case "Records":
                PlayMusic(menuMusic);
                break;
            case "Game":
                PlayMusic(gameMusic);
                break;
            case "SettingsScene":
                PlayMusic(menuMusic); 
                break;
            default:
                PlayMusic(gameMusic);
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip) 
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}