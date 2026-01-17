using UnityEngine;

public class DistanceScale : MonoBehaviour
{
    private GameObject _SpriteContainer;

    [SerializeField]
    private float minScale = 0.9f;

    [SerializeField]
    private float maxScale = 1.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _SpriteContainer = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float scaleFactor = Mathf.Lerp(maxScale, minScale, ((transform.position.y + 3.0f) / 11.0f));
        _SpriteContainer.transform.localScale = Vector3.one * scaleFactor;
    }
}
