using UnityEngine;

public class Mayornager : MonoBehaviour
{
    [SerializeField] Dialogue.DialogueEntry mamdaniDialogue;
    [SerializeField] Dialogue.DialogueEntry sliwaDialogue;
    [SerializeField] GameObject theGuys;
    [SerializeField] AudioClip sliwaFanfare;
    [SerializeField] GameObject kindlyMamdani;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StoryManager.Instance.GetProgress("Voting") != 0)
        {
            theGuys.SetActive(false);
        } 

        if(StoryManager.Instance.GetProgress("Voting") == 1)
        {
            MamdaniWin();
        }

        else if(StoryManager.Instance.GetProgress("Voting") == 2)
        {
            SliwaWin();
        }

        if(StoryManager.Instance.GetProgress("Voting") == 1 || StoryManager.Instance.GetProgress("Voting") == 3)
        {
            kindlyMamdani.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MamdaniWin()
    {
        mamdaniDialogue.Play();
        StoryManager.Instance.SetProgress("Voting", 3);
    }

    void SliwaWin()
    {
        sliwaDialogue.Play();
        StoryManager.Instance.SetProgress("Voting", 4);
    }

    public void DialogueEnding()
    {
        PartyManager.Instance.AddPartyMember("Sliwa", sliwaFanfare);
    }
}
