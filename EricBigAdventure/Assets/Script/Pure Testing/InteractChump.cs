using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InteractChump : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChumpBehavior()
    {
        if(StoryManager.Instance.GetProgress("NewYork") == 0)
        {
            _Dialogues[0].Play();
        }

        else
        {
            _Dialogues[1].Play();
        }
    }

    public void IncrementStory()
    {
        StoryManager.Instance.SetProgress("NewYork", 1);
        _interactable.Reenable();
    }
}
