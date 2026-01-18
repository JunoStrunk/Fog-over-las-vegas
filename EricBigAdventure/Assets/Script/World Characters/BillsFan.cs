using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BillsFan : MonoBehaviour
{
    private Interactable _interactable;
    public AudioClip foxTheme;
    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;

    [SerializeField] private Sprite dead;
    private SpriteRenderer _Spriter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Spriter = GetComponent<SpriteRenderer>();

        _interactable = GetComponentInChildren<Interactable>();
        if(PartyManager.Instance.HasPartyMember("JoshAllen") && StoryManager.Instance.GetProgress("Fan") > 0)
        {
            _interactable.interactText = "Distract";
        }

        if(StoryManager.Instance.GetProgress("Fan") > 1)
        {
            _Spriter.sprite = dead;
            _interactable.interactText = "Mourn";
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

        else
        {
            _Dialogues[3].Play();
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

        //Kill fan with football robot
        GetComponent<Animator>().SetTrigger("Kill");

        _interactable.interactText = "Mourn";
        _interactable.Reenable();
    }

    public void Audio()
    {
        GetComponent<AudioSource>().PlayOneShot(foxTheme);
    }
}
