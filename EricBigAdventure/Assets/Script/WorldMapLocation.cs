using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapLocation : MonoBehaviour
{
    public string WorldName;
    public bool CannotFire;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CannotFire)
        {
            if (collision.GetComponent<CarMovement>() != null)
            {
                MuppetLineManager._Instance.ExitMapMode(WorldName);
                SceneManager.LoadScene(WorldName);
            }
        }
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.GetComponent<CarMovement>() != null)
		{
            CannotFire = false;
		}
	}
}
