using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleScreenEnter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem.actions.FindAction("Enter").performed += StartNextScene;
    }

    private void StartNextScene(InputAction.CallbackContext context)
    {
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
			SceneManager.LoadScene("PhoneCall");
		}
	}
}
