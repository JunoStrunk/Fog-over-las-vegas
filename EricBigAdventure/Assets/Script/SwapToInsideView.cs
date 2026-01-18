using UnityEngine;
using UnityEngine.InputSystem;

public class SwapToInsideView : MonoBehaviour
{
	private InputAction jumpAction;

	public Camera playerCam;
	public Camera InsideCam;
	public bool inside;

	public GameObject Gleep;
	public GameObject JoshAllen;
	public GameObject Sliwa;
	public GameObject Clooney;
	public GameObject BatonRouge;

    private void OnEnable()
    {
		jumpAction = InputSystem.actions.FindAction("Jump");
		jumpAction.performed += OnJump;
	}

    private void OnDisable()
    {
        jumpAction.performed -= OnJump;
    }
    private void OnJump(InputAction.CallbackContext context)
	{
		Jump();
	}

	void Jump()
	{
		if(inside)
		{
			InsideCam.enabled = false;
			playerCam.enabled = true;
			inside = false;
		}
		else
		{
			InsideCam.enabled = true;
			playerCam.enabled = false;
			inside = true;

			if(PartyManager.Instance.HasPartyMember("Gleep") && PartyManager.Instance.HadGleepBefore)
				{

			}







		}
	}
}
