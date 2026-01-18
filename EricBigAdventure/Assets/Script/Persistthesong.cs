using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Persistthesong : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static Persistthesong Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.activeSceneChanged += newScene;
            GetComponent<VideoPlayer>().enabled = true;
		}
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void newScene(Scene scene1, Scene scene2)
    {
        //Debug.LogWarning(scene2.name);
        
        if (scene2.name == "WorldMap")
        {
            GetComponent<VideoPlayer>().Play();
		}
        else
        {
			GetComponent<VideoPlayer>().Pause();
		}
    }
    // Update is called once per frame
    void Update()
    {
    }
}
