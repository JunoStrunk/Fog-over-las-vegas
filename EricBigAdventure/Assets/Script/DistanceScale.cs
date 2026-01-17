using UnityEngine;

public class DistanceScale : MonoBehaviour
{
    private GameObject _SpriteContainer;

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
    }

    // Update is called once per frame
    void Update()
    {
        float scaleFactor = Mathf.Lerp(maxScale, minScale, ((transform.position.y - lowBound) / (highBound - lowBound)));
        _SpriteContainer.transform.localScale = Vector3.one * scaleFactor;
    }
}
