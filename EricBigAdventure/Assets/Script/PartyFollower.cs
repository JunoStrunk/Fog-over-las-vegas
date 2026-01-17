using UnityEngine;
using UnityEngine.EventSystems;

public class PartyFollower : MonoBehaviour
{
    [SerializeField] public GameObject _FollowTarget;
    [SerializeField] private GameObject _Sprite;
    [SerializeField] private SpriteRenderer _SpriteRenderer;

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

    public PartyFollower son;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
                if (_stepTimer > _stepAfter)
                {
                    _Sprite.transform.eulerAngles = new Vector3(0.0f, 0.0f, _Sprite.transform.eulerAngles.z * -1);
                    _stepTimer = 0.0f;
                }
            }
        }

        else
        {
            _following = distance > followDistance * _scale;
            if(_following)
            {
                _Sprite.transform.eulerAngles = new Vector3(0.0f, 0.0f, _rotationSize);
            }
        }

    }

    void FixedUpdate()
    {
        if(_following)
        {
            Vector3 moveVector = _moveDirection * speed;
            if (moveVector.x != 0)
            {
                _SpriteRenderer.flipX = moveVector.x < 0;
            }

            _RB.MovePosition(transform.position + (moveVector * Time.fixedDeltaTime));
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

}
