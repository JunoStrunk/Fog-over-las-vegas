using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DannyOceanWorld : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();

        if (StoryManager.Instance.GetProgress("Las Vegas") == 1)
        {
            _interactable.interactText = "Recruit";
        }

        if (StoryManager.Instance.GetProgress("Las Vegas") > 2)
        {
            gameObject.SetActive(false);
        }
    }

    public void DannyBehavior()
    {
        if (StoryManager.Instance.GetProgress("Las Vegas") == 0)
        {
            _Dialogues[0].Play();
        }

        else if (StoryManager.Instance.GetProgress("Las Vegas") == 2)
        {
            _Dialogues[1].Play();
        }
    }

    public void NoProgress()
    {
        _interactable.Reenable();
    }

    public void DannyInParty()
    {
        StoryManager.Instance.SetProgress("Las Vegas", 3);

        PartyManager.Instance.AddPartyMemberAtGameObject("DannyOcean", gameObject);
    }
}
