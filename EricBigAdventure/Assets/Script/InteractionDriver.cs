using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDriver : MonoBehaviour
{
    private Interactable _currentTarget;
    private InputAction _interactAction;
    private string _interactionText;
    [SerializeField] private string _InteractKey;
    [SerializeField] private string _DefaultText;
    [SerializeField] private SpriteRenderer parentSprite;
    [SerializeField] private bool _rotateParent = false;

    private UILogic _UI;
    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
        _interactAction.performed += OnInteract;
        _UI = FindAnyObjectByType<UILogic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterInteractable(Interactable obj)
    {
        _currentTarget = obj;
        if(_currentTarget.interactText != "")
        {
            _interactionText = _InteractKey + ": " + _currentTarget.interactText;
        }
        else
        {
            _interactionText = _InteractKey + ": " + _DefaultText;
        }

        _UI.SetSubtitle(_interactionText, "Gameplay");
    }

    public void UnregisterInteractable(Interactable obj)
    {
        _currentTarget = null;
        _UI.RemoveSubtitle(_interactionText);
        _interactionText = "";
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(_currentTarget != null)
        {
            if(_rotateParent)
            {
                
                if(_currentTarget.transform.parent.position.x < transform.parent.parent.parent.position.x)
                {
                    parentSprite.flipX = true;
                }
            }
            _currentTarget.Interact();
        }
    }

    public string GetInteractionText()
    {
        return _interactionText;
    }
}
