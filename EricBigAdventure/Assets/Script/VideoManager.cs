using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public Camera Player;
    public Camera Video;

    public UnityEvent HandleEnd;
    public UnityEvent HandleStart;

    private void Start()
    {
        GetComponent<VideoPlayer>().loopPointReached += OnVideoEnd;
    }

    public void StartVid()
    {
        HandleStart.Invoke();
        Player.enabled = false;
        Video.enabled = true;
		GetComponent<VideoPlayer>().Play();
	}

    private void OnVideoEnd(VideoPlayer source)
    {
		Player.enabled = true;
		Video.enabled = false;
		HandleEnd.Invoke();
    }
}
