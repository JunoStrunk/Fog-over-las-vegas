using UnityEngine;
using UnityEngine.SceneManagement;

public class SliwaVote : MonoBehaviour
{
    public void Vote()
    {
        StoryManager.Instance.SetProgress("Voting", 2);
        SceneManager.LoadScene("New York");
    }
}
