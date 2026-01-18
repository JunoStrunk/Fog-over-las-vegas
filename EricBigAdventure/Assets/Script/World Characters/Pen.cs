using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();

        if (PartyManager.Instance.HasPartyMember("DannyOcean"))
        {
            _interactable.interactText = "Let Danny Work";
        }

        if (StoryManager.Instance.GetProgress("Pen") > 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Penhaviour()
    {
        if (PartyManager.Instance.HasPartyMember("DannyOcean"))
        {
            _Dialogues[1].Play();
        }
        else
        {
            _Dialogues[0].Play();
        }
    }

    public void NoProgress()
    {
        _interactable.Reenable();
    }

    public void StealPen()
    {
        StoryManager.Instance.SetProgress("Pen", 1);
        //TODO: Danny works his magic
    }
}
