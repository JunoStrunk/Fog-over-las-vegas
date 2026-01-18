using UnityEngine;
using UnityEngine.Video;

public class EndVideoManager : MonoBehaviour
{
	private void Start()
	{
		GetComponent<VideoPlayer>().loopPointReached += OnVideoEnd;
	}
	private void OnVideoEnd(VideoPlayer source)
	{
		Application.Quit();
	}
}
