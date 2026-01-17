using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public int currentIndex;
    private UILogic _uiBehaviour;

    [System.Serializable]
    public struct DialogueLine
    {
        public string text;
        public AudioClip audio;
        public string style;
        public string animation;
    }

    [System.Serializable]
    public struct DialogueEntry
    {
        public Dialogue dialogue;
        public UnityEvent callback;

        public void Play()
        {
            dialogue.PlayDialogue(callback);
        }
    }


    [SerializeField] private List<DialogueLine> lines;

    public void PlayDialogue(UnityEvent callback)
    {
        currentIndex = 0;
        _uiBehaviour = FindAnyObjectByType<UILogic>();

        _uiBehaviour.StartDialogue(this, callback);
    }

    public DialogueLine GetNextLine()
    {
        return lines[currentIndex++]; 
    }

    public int GetLength()
    {
        return lines.Count; 
    }
}
