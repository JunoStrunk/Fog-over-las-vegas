using UnityEngine;

public class Mayornager : MonoBehaviour
{
    [SerializeField] Dialogue.DialogueEntry mamdaniDialogue;
    [SerializeField] Dialogue.DialogueEntry sliwaDialogue;
    [SerializeField] GameObject theGuys;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MamdaniWin()
    {
        mamdaniDialogue.Play();
    }

    void SliwaWin()
    {
        sliwaDialogue.Play();
    }

    public void DialogueEnding()
    {
        StoryManager.Instance.SetProgress("Voting", 3);
        PartyManager.Instance.AddPartyMember("Sliwa");
    }
}
