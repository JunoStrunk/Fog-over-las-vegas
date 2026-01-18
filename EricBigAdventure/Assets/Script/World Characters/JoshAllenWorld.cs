using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class JoshAllenWorld : MonoBehaviour
{
    private Interactable _interactable;

    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;

    [SerializeField] List<Dialogue.DialogueEntry> _Dialogues;

    [SerializeField] private SpriteRenderer _Sprite;

    private Vector3 _targetPosition;
    private bool _moving;
    public float speed = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StoryManager.Instance.SetProgress("Las Vegas", 1);

        _interactable = GetComponentInChildren<Interactable>();

        if (StoryManager.Instance.GetProgress("Buffalo") > 0 && StoryManager.Instance.GetProgress("Las Vegas") > 0)
        {
            _interactable.interactText = "Bribe";
        }

        if(StoryManager.Instance.GetProgress("Buffalo") > 1)
        {
            gameObject.SetActive(false);
        }

        _targetPosition = position1.position;
        _moving = true;
    }

    void Update()
    {
        if (_moving)
        {
            if (Vector3.Distance(transform.position, _targetPosition) < 0.4f)
            {
                if (_targetPosition == position1.position)
                {
                    _targetPosition = position2.position;
                    _Sprite.flipX = true;
                }
                else
                {
                    _targetPosition = position1.position;
                    _Sprite.flipX = false;
                }
            }

            Vector3 moveDir = (_targetPosition - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }

    public void Billhavior()
    {
        _moving = false;
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
        _moving = true;
    }

    public void NoProgress()
    {
        _interactable.Reenable();
        _moving = true;
    }

    public void RecruitJA()
    {
        StoryManager.Instance.SetProgress("Buffalo", 2);
        PartyManager.Instance.AddPartyMemberAtGameObject("JoshAllen", gameObject);
        _moving = true;
    }
}
