using UnityEngine;
using UnityEngine.SceneManagement;

public class MamdaniVote : MonoBehaviour
{
    public AudioClip voteConfirm;
    public void Vote()
    {
        StoryManager.Instance.SetProgress("Voting", 1);
		AudioManager.Instance.PlaySound(voteConfirm);
		SceneManager.LoadScene("New York");
    }
}
