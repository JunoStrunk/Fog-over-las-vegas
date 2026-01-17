using UnityEngine;

public class PartyFollower : MonoBehaviour
{
    [SerializeField] private GameObject _FollowTarget;
    [SerializeField] private GameObject _Sprite;
    public float followDistance;
    public float stopDistance;
    public float speed;

    private bool _following;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _FollowTarget.transform.position);

        if (_following)
        {
            if(distance < stopDistance)
            {
                _following = false;
                return;
            }
        }
        if( > followDistance)
        {

        }
    }

    public void setFollowTarget(GameObject target)
    {
        _FollowTarget = target;
    }

}
