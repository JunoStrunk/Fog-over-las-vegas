using UnityEngine;

public class BenoitBlanc : MonoBehaviour
{
    private float timer;
    private SpriteRenderer _Sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(StoryManager.Instance.GetProgress("BatonRouge") < 2)
        {
            gameObject.SetActive(false);
        }
        _Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            _Sprite.flipX = !_Sprite.flipX;
            timer = 0;
        }
    }
}
