using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _bgmSource;
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip NewYorkBGM;

    private Dictionary<string, AudioClip> _BGMMap = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Otherwise, set this object as the instance and prevent it from being destroyed on scene load
        Instance = this;
        DontDestroyOnLoad(this.gameObject);


        _BGMMap.Add("New York", NewYorkBGM);

        SceneManager.sceneLoaded += SetBGM;
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    private void SetBGM(Scene scene, LoadSceneMode mode)
    {
        _bgmSource.Pause();
        if (_BGMMap.ContainsKey(scene.name))
        {
            _bgmSource.clip = GetSceneBGM(scene.name);
            _bgmSource.Play();
        }
    }

    private AudioClip GetSceneBGM(string sceneName)
    {
        return _BGMMap[sceneName];
    }
}
