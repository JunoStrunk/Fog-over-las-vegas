using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Collider2D _hitbox;

    public string interactText;

    [SerializeField] private UnityEvent _InteractionCallback;
    void Start()
    {
        _hitbox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InteractionDriver>() != null)
        {
            other.gameObject.GetComponent<InteractionDriver>().RegisterInteractable(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InteractionDriver>() != null)
        {
            other.gameObject.GetComponent<InteractionDriver>().UnregisterInteractable(this);
        }
    }

    public void Interact()
    {
        _InteractionCallback.Invoke();
        _hitbox.enabled = false;
    }

    public void Reenable()
    {
        _hitbox.enabled = true;
    }
}
