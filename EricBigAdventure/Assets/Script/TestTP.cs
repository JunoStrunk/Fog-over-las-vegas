using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerMovement>() != null)
		{
			MuppetLineManager._Instance.EnterMapMode();
		}

	}
}
