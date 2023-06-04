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

    float FreezeTimeElapsed = 0;
    static float FreezeTimeTotal = 0;
    static bool isFrozen = false;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = gameObject.transform.position;
        EndPosition = gameObject.transform.position + new Vector3(DirectionToTravel.x, 0, DirectionToTravel.y).normalized * DistanceToTravel;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            if (FreezeTimeElapsed < FreezeTimeTotal)
            {
                FreezeTimeElapsed += Time.deltaTime;
            }
            else
            {
                FreezeTimeElapsed = 0;
                isFrozen = false;
            }
        }

        if (!isFrozen)
        {
            if (MovingTowardsEnd) { TimeElapsed += Time.deltaTime; }
            else { TimeElapsed -= Time.deltaTime; }

            if (TimeElapsed > TimeTakenToTravelDistance) { TimeElapsed = TimeTakenToTravelDistance; MovingTowardsEnd = false; }
            else if (TimeElapsed < 0) { TimeElapsed = 0; MovingTowardsEnd = true; }

            gameObject.transform.position = Vector3.Lerp(StartPosition, EndPosition, TimeElapsed / TimeTakenToTravelDistance);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }    
    }

    public static void Freeze(float _freezeTimeTotal)
    {
        isFrozen = true;
        FreezeTimeTotal = _freezeTimeTotal;
    }
}
