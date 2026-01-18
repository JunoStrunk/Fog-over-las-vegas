using UnityEngine;
using UnityEngine.SceneManagement;

public class SliwaVote : MonoBehaviour
{
    public AudioClip voteConfirm;
    public void Vote()
    {
        StoryManager.Instance.SetProgress("Voting", 2);
        AudioManager.Instance.PlaySound(voteConfirm);
        SceneManager.LoadScene("New York");
    }
}
