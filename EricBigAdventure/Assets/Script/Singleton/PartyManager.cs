using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    [System.Serializable]
    struct SceneScaling
    {
        public string name;
        public float min;
        public float max;
        public float lowBound;
        public float highBound;
    }
    public static PartyManager Instance { get; private set; }

    private HashSet<string> _Party = new HashSet<string>();
    private Dictionary<string, SceneScaling> _ScaleMap = new Dictionary<string, SceneScaling>();
    [SerializeField] private List<SceneScaling> _ScaleList;

    private Dictionary<string, GameObject> _partyMembers = new Dictionary<string, GameObject>();

    private GameObject _currentFollowTarget;

    [SerializeField] private GameObject _JoshAllen;
    [SerializeField] private GameObject _Gleep;
    [SerializeField] private GameObject _DannyOcean;
    [SerializeField] private GameObject _Sliwa;
    [SerializeField] private GameObject _BatonRouge;

    public bool HadGleepBefore;
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

        foreach(SceneScaling scaling in _ScaleList)
        {
            _ScaleMap.Add(scaling.name, scaling);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void InitializeParty(Scene scene, LoadSceneMode mode)
    {
        _partyMembers.Clear();

        if (scene.name != "WorldMap" && scene.name != "Voting")
        {
            _currentFollowTarget = FindAnyObjectByType<PlayerMovement>().gameObject;
            foreach(string partyMember in _Party)
            {
                _partyMembers[partyMember] = CreatePartyFollower(ResolvePartyMember(partyMember));
            }
            
            if(_ScaleMap.ContainsKey(scene.name))
            {
                SceneScaling scale = _ScaleMap[scene.name]; 
                foreach(DistanceScale scaler in FindObjectsByType<DistanceScale>(FindObjectsSortMode.None))
                {
                    scaler.minScale = scale.min;
                    scaler.maxScale = scale.max;
                    scaler.lowBound = scale.lowBound;
                    scaler.highBound = scale.highBound;
                }
            }
        }
    }

    public GameObject CreatePartyFollower(GameObject follower)
    {
        GameObject newFollower = Instantiate(follower).gameObject;
        newFollower.transform.position = _currentFollowTarget.transform.position;
        newFollower.GetComponent<PartyFollower>().setFollowTarget(_currentFollowTarget);

        if(_currentFollowTarget.GetComponent<PartyFollower>() != null)
        {
            _currentFollowTarget.GetComponent<PartyFollower>().son = newFollower.GetComponent<PartyFollower>();
        }

        _currentFollowTarget = newFollower;
        return newFollower;
    }

    public void AddPartyMember(string newMember)
    {
        _Party.Add(newMember);
        _partyMembers[newMember] = CreatePartyFollower(ResolvePartyMember(newMember));
    }

    public void AddPartyMemberAtGameObject(string newMember, GameObject positioner)
    {
        _Party.Add(newMember);
        GameObject follower = ResolvePartyMember(newMember);
        GameObject newFollower = Instantiate(follower).gameObject;
        newFollower.transform.position = positioner.transform.position;
        newFollower.GetComponent<PartyFollower>().setFollowTarget(_currentFollowTarget);

        if (_currentFollowTarget.GetComponent<PartyFollower>() != null)
        {
            _currentFollowTarget.GetComponent<PartyFollower>().son = newFollower.GetComponent<PartyFollower>();
        }

        _currentFollowTarget = newFollower;

        _partyMembers[newMember] = newFollower;

        if (_ScaleMap.ContainsKey(SceneManager.GetActiveScene().name))
        {
            SceneScaling scale = _ScaleMap[SceneManager.GetActiveScene().name];
            DistanceScale scaler = newFollower.GetComponentInChildren<DistanceScale>();
            scaler.minScale = scale.min;
            scaler.maxScale = scale.max;
            scaler.lowBound = scale.lowBound;
            scaler.highBound = scale.highBound;
        }

        if(newMember == "Gleep")
        {
            HadGleepBefore = true;
		}

        positioner.SetActive(false);
    }

    private GameObject ResolvePartyMember(string memberAlias)
    {
        GameObject newMember = _Gleep;
        switch (memberAlias)
        {
            case "Gleep":
                newMember = _Gleep;
                break;
            case "JoshAllen":
                newMember = _JoshAllen;
                break;
            case "Sliwa":
                newMember = _Sliwa;
                break;
            case "BatonRouge":
                newMember = _BatonRouge;
                break;
            case "DannyOcean":
                newMember = _DannyOcean;
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

    public void RemovePartyMember(string alias)
    {
        GameObject toRemove = _partyMembers[alias];
        _Party.Remove(alias);

        if (toRemove.GetComponent<PartyFollower>().son != null)
        {
            toRemove.GetComponent<PartyFollower>().son.setFollowTarget(toRemove.GetComponent<PartyFollower>()._FollowTarget);
        }

        toRemove.SetActive(false);
    }
}
