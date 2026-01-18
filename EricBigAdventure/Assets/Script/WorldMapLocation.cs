using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapLocation : MonoBehaviour
{
    public string WorldName;
    public bool CannotFire;

    public AudioClip ericNeedsMoreMayors;
    public void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.GetComponent<CarMovement>() != null)
            {
                if (collision.GetComponent<CarMovement>().dontTp)
                {
                    return;
                }

                if(WorldName == "LosAngeles")
                {
                    if(PartyManager.Instance.HasPartyMember("Sliwa"))
                    {
                        WorldName = "LalaLand";
				    }
                    else
                    {
                        AudioManager.Instance.PlaySound(ericNeedsMoreMayors);
                        return;
                    }
                }

                MuppetLineManager._Instance.ExitMapMode(WorldName);
                if(WorldName == "Las Vegas" && StoryManager.Instance.GetProgress("Las Vegas") == 1)
                {
                    WorldName = "Vegas In Fog";
                }

				SceneManager.LoadScene(WorldName);
            }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<CarMovement>().dontTp = false;
	}
}
