using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{

    Rigidbody mRigidbody;
    [SerializeField] float DistanceToTravel;
    [SerializeField] float TimeToTravelBeforeExplosion;
    [SerializeField] float ExplosionTimeTotal;

    Vector2 mDirection;
    float TravelTimeElapsed = 0;
    float ExplosionTimeElapsed = 0;

    Vector3 InitalScale;
    [SerializeField] float MaxScaleMultipler;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitalScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (TravelTimeElapsed < TimeToTravelBeforeExplosion)
        {
            TravelTimeElapsed += Time.deltaTime;
            mRigidbody.velocity = new Vector3(mDirection.x, 0, mDirection.y).normalized * DistanceToTravel / TimeToTravelBeforeExplosion;
            Debug.Log("Moving");
        }
        else
        {
            mRigidbody.velocity = Vector3.zero;
            ExplodeBomb();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environemt")
        {
            Destroy(gameObject);
        }
    }

    public void SetRollingDirection(Vector2 _direction)
    {
        mDirection = _direction;
    }

    private void ExplodeBomb()
    {
        if (ExplosionTimeElapsed < ExplosionTimeTotal)
        {
            ExplosionTimeElapsed += Time.deltaTime;
            gameObject.transform.localScale = InitalScale * MaxScaleMultipler * ExplosionTimeElapsed / ExplosionTimeTotal;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
