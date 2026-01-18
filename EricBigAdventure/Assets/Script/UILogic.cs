using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static Dialogue;

public class UILogic : MonoBehaviour
{
    private InteractionDriver _InteractionDriver;
    private SubtitleStyles _Styler;
    private Dialogue _currentDialogue;
    private UnityEvent _dialogueFinishedCallback;
    private PlayerMovement _playerMovement;
    private DialogueLine _currentLine;

    [SerializeField] private TMP_Text _Subtitle;

    void Awake()
    {
        _InteractionDriver = FindAnyObjectByType<InteractionDriver>();
        _Styler = GetComponent<SubtitleStyles>();
        _playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSubtitle(string text, string style)
    {
        ApplyStyle(style);
        _Subtitle.text = text;
    }

    public void RemoveSubtitle(string text)
    {
        if(_Subtitle.text == text)
        {
            _Subtitle.text = "";
        }
    }
    
    public void ApplyStyle(string styleKey)
    {
        SubtitleStyles.style style = _Styler.GetStyle(styleKey);
        _Subtitle.font = style.font;
        _Subtitle.faceColor = style.fontColor;
        _Subtitle.outlineColor = style.outlineColor;
    }

    public void StartDialogue(Dialogue dialogue, UnityEvent callback)
    {
        _currentDialogue = dialogue;
        _dialogueFinishedCallback = callback;
        _playerMovement.canMove = false;
        PlayLine();
    }

    private void PlayLine()
    {
        if (_currentDialogue.currentIndex < _currentDialogue.GetLength())
        {
            _currentLine = _currentDialogue.GetNextLine();
            SetSubtitle(_currentLine.text, _currentLine.style);

            if(_currentLine.animation != "")
            {
                if(_currentLine.animation == "talk")
                {
                    _playerMovement.isTalking = true;
                }

                else if(_currentLine.animation == "hand")
                {
                    _playerMovement.GiveItem();
                }
                else if(_currentLine.animation == "phone")
                {
                    _playerMovement.PhoneCall();
                }
            }

            if (_currentLine.audio != null)
            {
                AudioManager.Instance.PlaySound( _currentLine.audio );
                StartCoroutine(LineDelay(_currentLine.audio.length));
            }

            else
            {
                StartCoroutine(LineDelay(3f));
            }
        }

        else
        {
            if(_dialogueFinishedCallback != null)
            {
                _dialogueFinishedCallback.Invoke();
            }

            _Subtitle.text = "";
            _playerMovement.canMove = true;
            _currentDialogue = null;
            _dialogueFinishedCallback = null;
        }
    }

    private IEnumerator LineDelay(float length)
    {
        yield return new WaitForSeconds(length);
        if(_currentLine.animation != "")
        {
            if(_currentLine.animation == "talk")
            {
                _playerMovement.isTalking = false;
            }
        }
        PlayLine();
    }
}
