using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

	public Sprite tombstone;

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


			Gleep.SetActive(false);
			JoshAllen.SetActive(false);
			Sliwa.SetActive(false);
			Clooney.SetActive(false);
			BatonRouge.SetActive(false);


			if (PartyManager.Instance.HasPartyMember("Gleep"))
			{
				Gleep.SetActive(true);
			}
			if(!PartyManager.Instance.HasPartyMember("Gleep") && PartyManager.Instance.HadGleepBefore)
			{
				Gleep.GetComponent<UnityEngine.UI.Image>().sprite = tombstone;
				Gleep.SetActive(true);
			}
			if (PartyManager.Instance.HasPartyMember("JoshAllen"))
			{
				JoshAllen.SetActive(true);
			}
			if (PartyManager.Instance.HasPartyMember("Sliwa"))
			{
				Sliwa.SetActive(true);
			}
			if (PartyManager.Instance.HasPartyMember("BatonRouge"))
			{
				BatonRouge.SetActive(true);
			}
			if (PartyManager.Instance.HasPartyMember("DannyOcean"))
			{
				Clooney.SetActive(true);
			}

		}
	}
}
