using UnityEngine;

public class DeadBillsFan : MonoBehaviour
{
    public Dialogue.DialogueEntry lines;
    private Interactable _interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactable = GetComponentInChildren<Interactable>();

        if (StoryManager.Instance.GetProgress("Fan") < 2)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLines()
    {
        lines.Play();
    }

    public void Nothing()
    { 
        _interactable.Reenable();
    }
}
