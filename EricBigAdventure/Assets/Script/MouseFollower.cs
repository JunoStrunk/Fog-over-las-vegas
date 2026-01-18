using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private InputAction _move;

    private Vector2 _moveDirection;

    private Rigidbody2D _RB;

    public bool canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _move = InputSystem.actions.FindAction("Look");

        _RB = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>().normalized;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
    }

    /*
    private void FixedUpdate()
    {
        Vector3 moveVector = Vector3.zero;
        if (_moveDirection != Vector2.zero)
        {
            moveVector = new Vector3(_moveDirection.x, _moveDirection.y, 0.0f) * speed;
        }

        if (canMove) _RB.MovePosition(transform.position + (moveVector * Time.fixedDeltaTime));
    }
    */
}
