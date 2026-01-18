using UnityEngine;
using UnityEngine.UI;

public class HeistScript : MonoBehaviour
{
    public GameObject danny;
    public SpriteRenderer pen;

    private void Start()
    {
		PartyManager.Instance.AddPartyMember("DannyOcean");
	}
    public void PenSteal()
    {
        pen.enabled = false;
    }

    public void ReAddDanny()
    {
		PartyManager.Instance.AddPartyMemberAtGameObject("DannyOcean", danny);
	}
}
