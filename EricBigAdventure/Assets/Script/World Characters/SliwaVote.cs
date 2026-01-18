using UnityEngine;
using UnityEngine.SceneManagement;

public class SliwaVote : MonoBehaviour
{
    public AudioClip voteConfirm;
    public Animator voting;
    public void Vote()
    {
        StoryManager.Instance.SetProgress("Voting", 2);
        AudioManager.Instance.PlaySound(voteConfirm);
        voting.SetTrigger("Sliwa");
    }

    
}
