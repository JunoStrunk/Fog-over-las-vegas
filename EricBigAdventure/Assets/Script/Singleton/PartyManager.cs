using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    private HashSet<string> _Party = new HashSet<string>();

    private GameObject _currentFollowTarget;

    [SerializeField] private PartyFollower _JoshAllen;
    [SerializeField] private PartyFollower _Gleep;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        SceneManager.sceneLoaded += InitializeParty;

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Otherwise, set this object as the instance and prevent it from being destroyed on scene load
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void InitializeParty(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "WorldMap")
        {
            _currentFollowTarget = FindAnyObjectByType<PlayerMovement>().gameObject;
            foreach(string partyMember in _Party)
            {
                CreatePartyFollower(ResolvePartyMember(partyMember));
            }            
        }
    }

    public void CreatePartyFollower(PartyFollower follower)
    {
        GameObject newFollower = Instantiate(follower).gameObject;
        newFollower.transform.position = _currentFollowTarget.transform.position;
        newFollower.GetComponent<PartyFollower>().setFollowTarget(_currentFollowTarget);
        _currentFollowTarget = newFollower;
    }

    public void AddPartyMember(string newMember)
    {
        _Party.Add(newMember);
        CreatePartyFollower(ResolvePartyMember(newMember));
    }

    private PartyFollower ResolvePartyMember(string memberAlias)
    {
        PartyFollower newMember = _Gleep;
        switch (memberAlias)
        {
            case "Gleep":
                newMember = _Gleep;
                break;
            case "JoshAllen":
                newMember = _JoshAllen;
                break;
            default:
                break;
        }
        return newMember;
    }

    public bool HasPartyMember(string alias)
    {
        return _Party.Contains(alias);
    }
}
