using UnityEngine;

public class BoatBob : MonoBehaviour
{
    private float _time;
    private float _initialY;

    public float amplitude;
    public float period;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _initialY = transform.position.y;
        if (StoryManager.Instance.GetProgress("BatonRouge") >= 2)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, _initialY + amplitude * Mathf.Sin(_time * period), transform.position.z);
    }
}
