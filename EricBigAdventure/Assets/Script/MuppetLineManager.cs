using System;
using UnityEngine;

public class MuppetLineManager : MonoBehaviour
{
    public bool inMap;
    private Vector2 LastLoggedPosition;
    public GameObject Car;
    public float DistToNewPoint;
    public GameObject LinePrefab;

    public void EnterMapMode()
    {
        Car = GameObject.FindAnyObjectByType<CarMovement>().gameObject;
        inMap = true;
        LastLoggedPosition = Car.transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        EnterMapMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (DistToNewPoint < (Vector2.Distance(Car.transform.position, LastLoggedPosition)))
        {
            GameObject currPoint = Instantiate(LinePrefab);
            currPoint.transform.position = new Vector3(Car.transform.position.x,Car.transform.position.y,Car.transform.position.z+1);
            LastLoggedPosition = Car.transform.position;

            Vector2 velocity = Car.GetComponent<Rigidbody2D>().linearVelocity;

            currPoint.transform.rotation = Quaternion.Euler(0, 0, (float)Math.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
        }
    }
}
