using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class JoshAllenWorld : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();

        if (StoryManager.Instance.GetProgress("Buffalo") > 0 && StoryManager.Instance.GetProgress("Las Vegas") > 0)
        {
            _interactable.interactText = "Bribe";
        }

        if(StoryManager.Instance.GetProgress("Buffalo") > 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Billhavior()
    {
        if (StoryManager.Instance.GetProgress("Buffalo") == 0)
        {
            _Dialogues[0].Play();
        }

        else if(StoryManager.Instance.GetProgress("Buffalo") == 1)
        {
            if(StoryManager.Instance.GetProgress("Las Vegas") == 1)
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
        StoryManager.Instance.SetProgress("Buffalo", 1);
        _interactable.Reenable();
    }

    public void NoProgress()
    {
        _interactable.Reenable();
    }

    public void RecruitJA()
    {
        StoryManager.Instance.SetProgress("Buffalo", 2);
        PartyManager.Instance.AddPartyMemberAtGameObject("JoshAllen", gameObject);
    }
}
