using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheFog : MonoBehaviour
{
    [SerializeField] private Dialogue.DialogueEntry FanboatClearsFog;
    [SerializeField] private Transform _fanboatPosition;
    [SerializeField] private GameObject _Fanboat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PartyManager.Instance.HasPartyMember("BatonRouge"))
        {
            FanboatClearsFog.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fanboat()
    {
        GameObject daFanboat = Instantiate(_Fanboat);
        daFanboat.transform.position = _fanboatPosition.position;
        StoryManager.Instance.SetProgress("Las Vegas", 2);

        //TODO: audio clip
        StartCoroutine(FanboatDelay());
    }

    private IEnumerator FanboatDelay()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Las Vegas");
    }
}
