using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GleepWorld : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    [SerializeField] private AudioClip GleepFanfare;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();

        if (StoryManager.Instance.GetProgress("Roswell") > 0 && StoryManager.Instance.GetProgress("Fan") > 1)
        {
            _interactable.interactText = "Reunite";
        }

        if (StoryManager.Instance.GetProgress("Roswell") > 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Gleephavior()
    {
        if (StoryManager.Instance.GetProgress("Roswell") == 0)
        {
            _Dialogues[0].Play();
        }

        else if (StoryManager.Instance.GetProgress("Roswell") == 1)
        {
            if (StoryManager.Instance.GetProgress("Fan") > 1)
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
        StoryManager.Instance.SetProgress("Roswell", 1);

        if (StoryManager.Instance.GetProgress("Roswell") > 0 && StoryManager.Instance.GetProgress("Fan") > 1)
        {
            _interactable.interactText = "Reunite";
        }

        _interactable.Reenable();
    }

    public void NoProgress()
    {
        _interactable.Reenable();
    }

    public void RecruitJA()
    {
        StoryManager.Instance.SetProgress("Roswell", 2);
        PartyManager.Instance.AddPartyMemberAtGameObject("Gleep", gameObject, GleepFanfare);
    }
}
