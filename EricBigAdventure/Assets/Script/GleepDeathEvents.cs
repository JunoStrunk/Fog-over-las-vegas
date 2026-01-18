using System.Collections;
using UnityEngine;

public class GleepDeathEvents : MonoBehaviour
{
	public AudioClip crowdNoise;
	public GameObject jA;
	public BatonRougeMayor mayor;

    private void Start()
    {
		//PartyManager.Instance.AddPartyMember("JoshAllen");
		//PartyManager.Instance.AddPartyMember("Gleep");
		if(StoryManager.Instance.GetProgress("BatonRouge") >=2 )
		{
			gameObject.SetActive(false);
		}
	}
    public void CrowdNoise()
    {
		GetComponent<AudioSource>().PlayOneShot(crowdNoise);
	}

	public void End()
	{
		PartyManager.Instance.AddPartyMemberAtGameObject("JoshAllen", jA, null);
		StartCoroutine(MomentOfSilence());
	}

	IEnumerator MomentOfSilence()
	{
		yield return new WaitForSeconds(0.8f);
        mayor.PlayEndingDialogue();

    }
}
