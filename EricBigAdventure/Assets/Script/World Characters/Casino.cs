using UnityEngine;
using UnityEngine.SceneManagement;

public class Casino : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(StoryManager.Instance.GetProgress("Las Vegas") > 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GOToDaCasino()
    {
        if (StoryManager.Instance.GetProgress("Las Vegas") == 0)
        {
            StoryManager.Instance.SetProgress("Las Vegas", 1);
            //TODO: Video
            SceneManager.LoadScene("Vegas In Fog");
        }
    }
}
