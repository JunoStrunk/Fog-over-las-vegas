using UnityEngine;

public class DistanceScale : MonoBehaviour
{
    private GameObject _SpriteContainer;
    private PartyFollower _Follower;
    [SerializeField]
    public float minScale = 0.9f;

    [SerializeField]
    public float maxScale = 1.1f;

    public float lowBound = -4.0f;
    public float highBound = 8.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _SpriteContainer = transform.GetChild(0).gameObject;
        if(transform.GetComponentInParent<PartyFollower>() != null)
        {
            _Follower = transform.GetComponentInParent<PartyFollower>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float scaleFactor = Mathf.Lerp(maxScale, minScale, ((transform.position.y - lowBound) / (highBound - lowBound)));
        _SpriteContainer.transform.localScale = Vector3.one * scaleFactor;
        if(_Follower != null)
        {
            _Follower.SetScale(scaleFactor);
        }
    }
}
