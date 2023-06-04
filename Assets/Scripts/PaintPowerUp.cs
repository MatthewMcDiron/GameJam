using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPowerUp : MonoBehaviour
{
    float TimeElapsedSinceSpawn = 0;
    [SerializeField] float DespawnTimeLength = 10.0f;
    [SerializeField] MeshRenderer PowerupMesh;

    public enum PowerupAbilities
    { 
        SpeedShoes,
        Bomb,
        BigBrush,
        Freeze,
        Clone,
        MAX_RANGE
    }

    PowerupAbilities Ability;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomAbility();       
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeElapsedSinceSpawn < DespawnTimeLength)
        {
            TimeElapsedSinceSpawn += Time.deltaTime;
            GiveDespawnWarning();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GiveDespawnWarning()
    {
        float PercentageOfTimeElapsed = TimeElapsedSinceSpawn / DespawnTimeLength;
        if ((PercentageOfTimeElapsed > 0.7 && PercentageOfTimeElapsed < 0.8)
            || (PercentageOfTimeElapsed > 0.85 && PercentageOfTimeElapsed < 0.9)
            || (PercentageOfTimeElapsed > 0.93 && PercentageOfTimeElapsed < 0.94)
            || (PercentageOfTimeElapsed > 0.95 && PercentageOfTimeElapsed < 0.96)
            || (PercentageOfTimeElapsed > 0.97 && PercentageOfTimeElapsed < 0.98)
            || (PercentageOfTimeElapsed > 0.985 && PercentageOfTimeElapsed < 0.99)
            || (PercentageOfTimeElapsed > 0.992 && PercentageOfTimeElapsed < 0.995)
            )
        {
            PowerupMesh.enabled = false;
        }
        else
        {
            PowerupMesh.enabled = true;
        }
    }

    public void SetRandomAbility()
    {
        Ability = (PowerupAbilities)Random.Range(0, (int)PowerupAbilities.MAX_RANGE - 1);
        //Set model for ability
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Ability == PowerupAbilities.SpeedShoes) { OnSpeedShoesEntered(); }
            if (Ability == PowerupAbilities.BigBrush) { OnBigBrushEntered(); }
            if (Ability == PowerupAbilities.Bomb) { OnBombEntered(); }
            if (Ability == PowerupAbilities.Freeze) { OnFreezeEntered(); }
            if (Ability == PowerupAbilities.Clone) { OnCloneEntered(); }
            Debug.Log("ACTIVATE POWERUP");
            Destroy(gameObject);
        }
    }

    private void OnSpeedShoesEntered()
    {
        //Need player character
    }

    private void OnBombEntered()
    {
        //Need player character
    }

    private void OnBigBrushEntered()
    {
        //Increase player collision size
    }

    private void OnFreezeEntered()
    {
        //Freeze enemies
    }

    private void OnCloneEntered()
    {
        //Need player character
    }
}
