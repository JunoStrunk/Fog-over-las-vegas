using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheFog : MonoBehaviour
{
    [SerializeField] private Dialogue.DialogueEntry FanboatClearsFog;
    [SerializeField] private Transform _fanboatPosition;
    [SerializeField] private GameObject _Fanboat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		//PartyManager.Instance.AddPartyMember("BatonRouge");

		if (PartyManager.Instance.HasPartyMember("BatonRouge"))
        {
            FanboatClearsFog.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fanboat()
    {
        StoryManager.Instance.SetProgress("Las Vegas", 2);

        //TODO: audio clip
        GetComponent<Animator>().SetTrigger("ClearFog");
    }

    public void FanboatDelay()
    {
        SceneManager.LoadScene("Las Vegas");
    }

    public AudioClip Screech;
    public AudioClip Fan;
    public Animator fog;
    public void FanNoise()
    {
        GetComponent<AudioSource>().PlayOneShot(Fan);
    }

    public void BrakeScreech()
    {
		GetComponent<AudioSource>().PlayOneShot(Screech);
	}

    public void MoveTheFog()
    {
        fog.SetTrigger("MoveFog");
    }
}
