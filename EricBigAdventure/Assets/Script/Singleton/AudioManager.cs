using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _bgmSource;
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip NewYorkBGM;
    [SerializeField] private AudioClip BuffaloBGM;
	[SerializeField] private AudioClip BatonRougeBGM;
	[SerializeField] private AudioClip HappyRoswellBGM;
	[SerializeField] private AudioClip PenLandBGM;
	[SerializeField] private AudioClip SadRoswellBGM;
	[SerializeField] private AudioClip HappyVegasBGM;
	[SerializeField] private AudioClip ScaryVegasBGM;
	[SerializeField] private AudioClip VotingBGM;

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
        _BGMMap.Add("Buffalo", BuffaloBGM);
		_BGMMap.Add("Baton Rouge", BatonRougeBGM);
		_BGMMap.Add("Happy Roswell", HappyRoswellBGM);
		_BGMMap.Add("Sad Roswell", SadRoswellBGM);
		_BGMMap.Add("Pen Land", PenLandBGM);
		_BGMMap.Add("Las Vegas", HappyVegasBGM);
		_BGMMap.Add("Vegas In Fog", ScaryVegasBGM);
        _BGMMap.Add("Voting", VotingBGM);

		SceneManager.sceneLoaded += SetBGM;
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    private void SetBGM(Scene scene, LoadSceneMode mode)
    {
        _bgmSource.Pause();
        if(scene.name == "Roswell")
        {
            if(StoryManager.Instance.GetProgress("BatonRouge") < 2)
            {
				_bgmSource.clip = GetSceneBGM("Happy Roswell");
				_bgmSource.Play();
			}
            else
            {
				_bgmSource.clip = GetSceneBGM("Sad Roswell");
				_bgmSource.Play();
			}

        }
        else if (_BGMMap.ContainsKey(scene.name))
        {
            _bgmSource.clip = GetSceneBGM(scene.name);
            _bgmSource.Play();
        }
    }

    private AudioClip GetSceneBGM(string sceneName)
    {
        return _BGMMap[sceneName];
    }
    
    public void PauseBGM()
    {
        _bgmSource.Pause();
    }
}
