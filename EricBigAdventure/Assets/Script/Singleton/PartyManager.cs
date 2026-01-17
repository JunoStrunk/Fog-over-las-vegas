using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
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

    private GameObject _currentFollowTarget;

    [SerializeField] private GameObject _JoshAllen;
    [SerializeField] private GameObject _Gleep;
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
        if (scene.name != "WorldMap")
        {
            _currentFollowTarget = FindAnyObjectByType<PlayerMovement>().gameObject;
            foreach(string partyMember in _Party)
            {
                CreatePartyFollower(ResolvePartyMember(partyMember));
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

    public void CreatePartyFollower(GameObject follower)
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
