using UnityEngine;
using UnityEngine.UI;

public class BalloonEricManagement : MonoBehaviour
{
    public RenderTexture otherVid;
    public RenderTexture myVid;
    public RawImage myVidImage;
    public Interactable turnThisBackOn;

    public void startVid()
    {
        myVidImage.texture = myVid;
	}
    public void endVideo()
    {
        myVidImage.texture = otherVid;
        turnThisBackOn.Reenable();
    }

    
}
