using UnityEngine;
using UnityEngine.SceneManagement;

public class MamdaniVote : MonoBehaviour
{
    public void Vote()
    {
        StoryManager.Instance.SetProgress("Voting", 1);
        SceneManager.LoadScene("New York");
    }
}
