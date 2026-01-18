using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BillsFan : MonoBehaviour
{
    private Interactable _interactable;
    public AudioClip foxTheme;
    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;

    [SerializeField] private Sprite dead;
    [SerializeField] private GameObject balloon;
    private bool ballooning = false;
    [SerializeField] private Sprite awestruck;
    private SpriteRenderer _Spriter;
    private Animator _Animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Spriter = GetComponent<SpriteRenderer>();
        _Animator = GetComponent<Animator>();
        _Animator.enabled = false;

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
        if(ballooning)
        {
            balloon.transform.position += (new Vector3(0, 1, 0) * 0.15f * Time.deltaTime);
        }
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
        _Animator.enabled = true;
        GetComponent<Animator>().SetTrigger("Kill");

        _interactable.interactText = "Mourn";
        StartCoroutine(WaitToMourn());
    }

    public void Audio()
    {
        GetComponent<AudioSource>().PlayOneShot(foxTheme);
    }

    System.Collections.IEnumerator WaitToMourn()
    {
        yield return new WaitForSeconds(3);

        StoryManager.Instance.SetProgress("Fan", 2);

        _interactable.Reenable();

    }

    public void IstDasJoshAllen()
    {
        ballooning = true;
        _Spriter.sprite = awestruck;
        _Spriter.flipX = true;
        StartCoroutine(BalloonWait());
    }

    System.Collections.IEnumerator BalloonWait()
    {
        yield return new WaitForSeconds(10.9f);
        balloon.SetActive(false);
        ballooning = false;
    }
}
