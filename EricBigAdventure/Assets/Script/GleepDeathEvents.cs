using UnityEngine;

public class GleepDeathEvents : MonoBehaviour
{
	public AudioClip crowdNoise;
	public GameObject jA;

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
		PartyManager.Instance.AddPartyMemberAtGameObject("JoshAllen", jA);
	}
}
