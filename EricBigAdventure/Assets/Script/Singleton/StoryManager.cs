using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class StoryManager : MonoBehaviour
{
    // A public static reference to the single instance of the class
    public static StoryManager Instance { get; private set; }

    private Dictionary<string, uint> _storyProgress = new Dictionary<string, uint>();

    private void Awake()
    {
        // If an instance already exists, destroy this new one
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Otherwise, set this object as the instance and prevent it from being destroyed on scene load
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        InputSystem.actions.FindAction("Quit").performed += OnQuit;
    }

    public uint GetProgress(string name)
    {
        if(!_storyProgress.ContainsKey(name))
        {
            _storyProgress.Add(name, 0);
        }

        return _storyProgress[name];
    }

    public void SetProgress(string name, uint value)
    {
        _storyProgress[name] = value;
    }
    private void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}