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

    [SerializeField] private TMP_Text _Subtitle;

    void Start()
    {
        _InteractionDriver = FindAnyObjectByType<InteractionDriver>();
        _Styler = GetComponent<SubtitleStyles>();
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
        PlayLine();
    }

    private void PlayLine()
    {
        if (_currentDialogue.currentIndex < _currentDialogue.GetLength())
        {
            DialogueLine currentLine = _currentDialogue.GetNextLine();
            SetSubtitle(currentLine.text, currentLine.style);

            if(currentLine.animation != "")
            {

            }

            if (currentLine.audio != null)
            {
                StartCoroutine(LineDelay(currentLine.audio.length));
            }

            else
            {
                StartCoroutine(LineDelay(0.3f));
            }
        }

        else
        {
            if(_dialogueFinishedCallback != null)
            {
                _dialogueFinishedCallback.Invoke();
            }

            _Subtitle.text = "";
            _currentDialogue = null;
            _dialogueFinishedCallback = null;
        }
    }

    private IEnumerator LineDelay(float length)
    {
        yield return new WaitForSeconds(length);
        PlayLine();
    }
}
