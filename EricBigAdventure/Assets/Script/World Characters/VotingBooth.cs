using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VotingBooth : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();

        if (StoryManager.Instance.GetProgress("Pen") > 0)
        {
            _interactable.interactText = "Perform Civic Duty";
        }

        if (StoryManager.Instance.GetProgress("Voting") > 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Penhaviour()
    {
        if (StoryManager.Instance.GetProgress("Pen") > 0)
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

    public void Vote()
    {
        SceneManager.LoadScene("Voting");
    }
}
