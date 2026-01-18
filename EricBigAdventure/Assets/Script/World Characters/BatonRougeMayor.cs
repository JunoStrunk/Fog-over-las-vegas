using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BatonRougeMayor : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    [SerializeField] GameObject _Fanboat;

    public Animator JAandGleep;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();

        if(StoryManager.Instance.GetProgress("BatonRouge") > 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void MayorBehavior()
    {
        if(StoryManager.Instance.GetProgress("BatonRouge") == 0)
        {
            _Dialogues[0].Play();
        }

        else if (StoryManager.Instance.GetProgress("BatonRouge") == 1)
        {
            if (PartyManager.Instance.HasPartyMember("JoshAllen") && PartyManager.Instance.HasPartyMember("Gleep"))
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
        StoryManager.Instance.SetProgress("BatonRouge", 1);
        _interactable.Reenable();
    }

    public void NoProgress()
    {
        _interactable.Reenable();
    }

    public void DeathOfGleep()
    {
        PartyManager.Instance.RemovePartyMember("Gleep");

        StoryManager.Instance.SetProgress("BatonRouge", 2);
        PartyManager.Instance.AddPartyMemberAtGameObject("BatonRouge", gameObject);
        _Fanboat.SetActive(false);

		//TODO : WTF is going on with Josh Allen and Gleep
		PartyManager.Instance.RemovePartyMember("JoshAllen");
		JAandGleep.SetTrigger("Saviour");
    }
}
