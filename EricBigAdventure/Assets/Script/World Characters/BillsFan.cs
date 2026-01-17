using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BillsFan : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();
        if(PartyManager.Instance.HasPartyMember("JoshAllen") && StoryManager.Instance.GetProgress("Fan") > 0)
        {
            _interactable.interactText = "Distract";
        }

        if(StoryManager.Instance.GetProgress("Fan") > 1)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChumpBehavior()
    {
        if (StoryManager.Instance.GetProgress("Fan") == 0)
        {
            _Dialogues[0].Play();
        }

        else if(StoryManager.Instance.GetProgress("Fan") == 1)
        {
            if(PartyManager.Instance.HasPartyMember("JoshAllen"))
            {
                _Dialogues[2].Play();
            }

            else
            {
                _Dialogues[1].Play();
            }
        }
    }

    public void IncrementStory()
    {
        StoryManager.Instance.SetProgress("Fan", 1);
        _interactable.Reenable();
    }

    public void NoProgress()
    {
        _interactable.Reenable();
    }

    public void DeathOfFan()
    {
        StoryManager.Instance.SetProgress("Fan", 2);
        //TODO : Kill fan with football robot
    }
}
