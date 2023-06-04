using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Vector2 DirectionToTravel;
    [SerializeField] float DistanceToTravel;
    [SerializeField] float TimeTakenToTravelDistance;

    float TimeElapsed = 0;
    Vector3 StartPosition;
    Vector3 EndPosition;
    bool MovingTowardsEnd = true;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = gameObject.transform.position;
        EndPosition = gameObject.transform.position + new Vector3(DirectionToTravel.x, 0, DirectionToTravel.y).normalized * DistanceToTravel;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovingTowardsEnd) { TimeElapsed += Time.deltaTime; }
        else { TimeElapsed -= Time.deltaTime; }

        if (TimeElapsed > TimeTakenToTravelDistance) { TimeElapsed = TimeTakenToTravelDistance; MovingTowardsEnd = false; }
        else if (TimeElapsed < 0) { TimeElapsed = 0; MovingTowardsEnd = true; }

        gameObject.transform.position = Vector3.Lerp(StartPosition, EndPosition, TimeElapsed / TimeTakenToTravelDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }    
    }
}
