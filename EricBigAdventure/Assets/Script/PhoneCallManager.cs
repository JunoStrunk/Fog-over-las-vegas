using UnityEngine;

public class PhoneCallManager : MonoBehaviour
{
    public PlayerMovement ericRef;

    [SerializeField] public Dialogue.DialogueEntry entry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entry.Play();
        //ericRef.PhoneCall();
    }

    public void EndCall()
    {
        MuppetLineManager._Instance.EnterMapMode();
    }
}
