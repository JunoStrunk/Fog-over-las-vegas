using UnityEngine;
using UnityEngine.SceneManagement;

public class MamdaniVote : MonoBehaviour
{
    public AudioClip voteConfirm;
	public Animator voting;
	public void Vote()
    {
        StoryManager.Instance.SetProgress("Voting", 1);
		AudioManager.Instance.PlaySound(voteConfirm);
		voting.SetTrigger("Mamdani");
	}
}
