using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
	[SerializeField]
	private float speed;

	private InputAction _move;

	private Vector2 _moveDirection;

	private Rigidbody2D _RB;
	[SerializeField] private SpriteRenderer _SpriteRenderer;
	[SerializeField] private Sprite _Fanboat;
	public bool dontTp;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_move = InputSystem.actions.FindAction("Move");

		_RB = GetComponent<Rigidbody2D>();
		_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
		if(StoryManager.Instance.GetProgress("BatonRouge") >= 2)
		{
			_SpriteRenderer.sprite = _Fanboat;
			_SpriteRenderer.transform.localScale = Vector3.one * 4;
			speed *= 0.8f;
		}
	}

	// Update is called once per frame
	void Update()
	{
		_moveDirection = _move.ReadValue<Vector2>().normalized;
	}

	private void FixedUpdate()
	{
		Vector3 moveVector = Vector3.zero;
		if (_moveDirection != Vector2.zero)
		{
			moveVector = new Vector3(_moveDirection.x, _moveDirection.y, 0.0f) * speed;
			if (moveVector.x != 0)
			{
				_SpriteRenderer.flipX = !(moveVector.x < 0);
			}
		}
		_RB.AddForce(moveVector,ForceMode2D.Impulse);
		//_RB.MovePosition(transform.position + (moveVector * Time.fixedDeltaTime));
	}
}
