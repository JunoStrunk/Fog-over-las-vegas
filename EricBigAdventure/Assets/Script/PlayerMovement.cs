using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private InputAction _move;

    private Vector2 _moveDirection;

    private Rigidbody2D _RB;
    private SpriteRenderer _SpriteRenderer;

    public Animator MyAnimator;

    public bool canMove = true;

    private bool isRolling = false;

    private bool isParrying = false;

    private bool isSixSevening = false;

    public bool isTalking = false;

    private bool isIteming = false;

    private bool isCalling = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _move = InputSystem.actions.FindAction("Move");
        InputSystem.actions.FindAction("Roll").performed += OnRoll;
		InputSystem.actions.FindAction("Parry").performed += OnParry;
        InputSystem.actions.FindAction("SixSeven").performed += OnSixSeven;

		_RB = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>().normalized;

        if(isTalking)
        {
            MyAnimator.SetInteger("StateNum", 4);
        }
        else if(!isRolling && !isParrying && !isSixSevening && !isIteming && !isTalking && !isCalling)
        {
			Vector3 moveVector = Vector3.zero;
			if (_moveDirection != Vector2.zero)
			{
				moveVector = new Vector3(_moveDirection.x, _moveDirection.y, 0.0f) * speed;
				if (moveVector.x != 0)
				{
					if (InputSystem.actions.FindAction("Sprint").ReadValue<float>() >= 1.0f)
					{
						MyAnimator.SetInteger("StateNum", 3);
					}
                    else
                    {
						MyAnimator.SetInteger("StateNum", 1);
					}
				}
				else
				{
					MyAnimator.SetInteger("StateNum", 2);
				}
			}
			else
			{
				MyAnimator.SetInteger("StateNum", 0);
			}
		}
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = Vector3.zero;
        if (_moveDirection != Vector2.zero)
        {
            moveVector = new Vector3(_moveDirection.x, _moveDirection.y, 0.0f) * speed;
            if(moveVector.x != 0)
            {
				_SpriteRenderer.flipX = moveVector.x < 0;
            }
        }
        if (canMove)
		{
            if(isRolling)
            {
				_RB.MovePosition(transform.position + (moveVector * Time.fixedDeltaTime * 2));
			}
            else if (MyAnimator.GetInteger("StateNum") == 3)
            {
				_RB.MovePosition(transform.position + (moveVector * Time.fixedDeltaTime * 0.5f));
			}
            else
            {
                _RB.MovePosition(transform.position + (moveVector * Time.fixedDeltaTime));
            }
		}
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        if(!isParrying && !isRolling && !isSixSevening && !isIteming && !isTalking && !isCalling)
        {
			isRolling = true;
			MyAnimator.SetInteger("StateNum", 10);
			MyAnimator.SetTrigger("RollTrigger");
			StartCoroutine(RollDelaySeconds(1));
		}
	}

    private void OnParry(InputAction.CallbackContext context)
    {
        if(!isParrying && !isRolling && !isSixSevening && !isIteming && !isTalking && !isCalling)
        {
            canMove = false;
			isParrying = true;
			MyAnimator.SetInteger("StateNum", 10);
			MyAnimator.SetTrigger("ParryTrigger");
			StartCoroutine(ParryDelaySeconds(1));
		}
    }

	private void OnSixSeven(InputAction.CallbackContext context)
	{
		if (!isParrying && !isRolling && !isSixSevening && !isIteming && !isTalking && !isCalling)
		{
            canMove = false;
			isSixSevening = true;
			MyAnimator.SetInteger("StateNum", 10);
			MyAnimator.SetTrigger("SixSevenTrigger");
			StartCoroutine(SixSevenDelaySeconds(1));
		}
	}

    public void GiveItem()
    {
        canMove = false;
        isIteming = true;
        MyAnimator.SetInteger("StateNum", 10);
        MyAnimator.SetTrigger("ItemTrigger");
        StartCoroutine(ItemDelaySeconds(1));
    }

	public void PhoneCall()
	{
		canMove = false;
		isCalling = true;
		MyAnimator.SetInteger("StateNum", 10);
		MyAnimator.SetTrigger("PhoneTrigger");
		StartCoroutine(CallDelaySeconds(1));
	}

	private IEnumerator RollDelaySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isRolling = false;
    }

    private IEnumerator ParryDelaySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isParrying = false;
        canMove = true;
    }

    private IEnumerator SixSevenDelaySeconds(float seconds)
    {
		yield return new WaitForSeconds(seconds);
		isSixSevening = false;
        canMove = true;
	}

    private IEnumerator ItemDelaySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isIteming = false;
        canMove = true;
    }

	private IEnumerator CallDelaySeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		isCalling = false;
		canMove = true;
	}
}
