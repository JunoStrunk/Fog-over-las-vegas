using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuppetLineManager : MonoBehaviour
{
    public bool inMap;
    private Vector2 LastLoggedPosition;
    public GameObject Car;
    public float DistToNewPoint;
    public GameObject LinePrefab;

    private List<Vector2> LoggedPos;
    private List<Quaternion> LoggedRot;

    public static MuppetLineManager _Instance;
    private Vector3 CarLastLoc;
    public String LastFired;
    private bool firstTime;
	public void EnterMapMode()
	{
		StartCoroutine(EnterMapModeRoutine());
	}

	private IEnumerator EnterMapModeRoutine()
	{
		AsyncOperation loadOp = SceneManager.LoadSceneAsync("WorldMap");

		while (!loadOp.isDone)
			yield return null;

		Car = FindAnyObjectByType<CarMovement>().gameObject;
        if (!firstTime)
        {
            Car.GetComponent<CarMovement>().dontTp = true;
        }
        firstTime = false;
		Car.transform.position = CarLastLoc;
		inMap = true;
		LastLoggedPosition = Car.transform.position;
		SpawnAllPoints();
	}

	public void ExitMapMode(String wml)
    {
        LastFired = wml;
        CarLastLoc = Car.transform.position;
        inMap = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_Instance == null)
        {
            _Instance = this;
            LoggedPos = new List<Vector2>();
            LoggedRot = new List<Quaternion>();
            CarLastLoc = new Vector3(0, 0, -2);
            firstTime = true;

			EnterMapMode();
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(this);
        }
    }

    // Update is called once per frameddd
    void Update()
    {
        if (inMap)
        {
            if (Car == null)
            {
                Car = GameObject.FindAnyObjectByType<CarMovement>().gameObject;
            }
            if (DistToNewPoint < (Vector2.Distance(Car.transform.position, LastLoggedPosition)))
            {
                GameObject currPoint = Instantiate(LinePrefab);
                currPoint.transform.position = new Vector3(Car.transform.position.x, Car.transform.position.y, Car.transform.position.z + 1);
                LastLoggedPosition = Car.transform.position;

                Vector2 velocity = Car.GetComponent<Rigidbody2D>().linearVelocity;

                currPoint.transform.rotation = Quaternion.Euler(0, 0, (float)Math.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);

                LoggedPos.Add(currPoint.transform.position);
                LoggedRot.Add(currPoint.transform.rotation);
            }
        }
    }

    private void SpawnAllPoints()
    {
        for (int i = 0; i < LoggedPos.Count; i++)
        {
			GameObject currPoint = Instantiate(LinePrefab);
            currPoint.transform.position = new Vector3(LoggedPos[i].x, LoggedPos[i].y,-1);
            currPoint.transform.rotation = LoggedRot[i];
        }
    }
}
