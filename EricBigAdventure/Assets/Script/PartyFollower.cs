using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PartyFollower : MonoBehaviour
{
    [SerializeField] public GameObject _FollowTarget;
    [SerializeField] private GameObject _Sprite;
    [SerializeField] private SpriteRenderer _SpriteRenderer;

    [SerializeField] Sprite Base;
    [SerializeField] Sprite WalkH;
    [SerializeField] Sprite WalkV;
    [SerializeField] Sprite Roll;
    [SerializeField] Sprite Parry;

    public float followDistance = 2.0f;
    public float stopDistance = 1.0f;
    public float speed = 10.0f;

    public float _stepAfter = 0.33f;
    private float _stepTimer;
    public float _rotationSize = 7.0f;

    private Rigidbody2D _RB;

    private bool _following;
    private Vector3 _moveDirection;
    private float _scale = 1.0f;

    private bool rolling;
    private bool parrying;
    private bool sprinting;

    private InputAction sprint;

    public PartyFollower son;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _RB = GetComponent<Rigidbody2D>();

        InputSystem.actions.FindAction("Roll").performed += OnRoll;
        InputSystem.actions.FindAction("Parry").performed += OnParry;
        sprint = InputSystem.actions.FindAction("Sprint");
    }

    // Update is called once per frame
    void Update()
    {
        sprinting = sprint.ReadValue<float>() >= 1;
        float distance = Vector3.Distance(transform.position, _FollowTarget.transform.position);

        if (_following)
        {
            if (distance < stopDistance * _scale)
            {
                _following = false;
                _stepTimer = 0.0f;
                _Sprite.transform.eulerAngles = Vector3.zero;
                return;
            }

            else
            {
                _moveDirection = (_FollowTarget.transform.position - transform.position).normalized;
                _stepTimer += Time.deltaTime;
                if (_stepTimer > (_stepAfter / (sprinting ? 2 : 1)) && !rolling)
                {
                    _Sprite.transform.eulerAngles = new Vector3(0.0f, 0.0f, _Sprite.transform.eulerAngles.z * -1);
                    _stepTimer = 0.0f;
                }
            }
        }

        else
        {
            _following = distance > followDistance * _scale;
            if(_following && !rolling)
            {
                _Sprite.transform.eulerAngles = new Vector3(0.0f, 0.0f, _rotationSize);
            }
        }

        if(rolling)
        {
            _Sprite.transform.Rotate(new Vector3(0, 0, 1) * -720 * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if(_following)
        {
            Vector3 moveVector = _moveDirection * speed * (rolling ? 2 : 1);
            if (moveVector.x != 0)
            {
                _SpriteRenderer.flipX = moveVector.x < 0;
            }

            _RB.MovePosition(transform.position + (moveVector * Time.fixedDeltaTime));

            if(!rolling && !parrying)
            {
                if (moveVector.y > moveVector.x) _SpriteRenderer.sprite = WalkV;
                else _SpriteRenderer.sprite = WalkH;
            }
        }

        else if (!rolling && !parrying)
        {

            _SpriteRenderer.sprite = Base;
        }
    }

    public void setFollowTarget(GameObject target)
    {
        _FollowTarget = target;
    }

    public void SetScale(float scale)
    {
        _scale = scale; 
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        if (!rolling && !parrying)
        {
            rolling = true;
            _SpriteRenderer.sprite = Roll;
            StartCoroutine(Recover());
        }
    }

    private void OnParry(InputAction.CallbackContext context)
    {
        if(!rolling && !parrying)
        {
            parrying = true;
            _SpriteRenderer.sprite = Parry;
            StartCoroutine(Recover());
        }
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(1);
        parrying = false;
        rolling = false;
        _Sprite.transform.eulerAngles = Vector3.zero;
        _SpriteRenderer.sprite = Base;
    }
}
