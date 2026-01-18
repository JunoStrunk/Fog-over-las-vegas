using UnityEngine;

public class Maine : MonoBehaviour
{
    public Dialogue.DialogueEntry entry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(StoryManager.Instance.GetProgress("Maine") != 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Photo()
    {
        entry.Play();
        StoryManager.Instance.SetProgress("Maine", 1);
    }

    public void NoProgress()
    {

    }
}
