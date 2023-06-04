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
        None,
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
        Ability = (PowerupAbilities)Random.Range(1, (int)PowerupAbilities.MAX_RANGE - 1);
        //Set model for ability in mesh
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.Instance().PlaySFX(AudioManager.Instance().PowerUpSFX, "Powerup");

            if (Ability == PowerupAbilities.SpeedShoes) { Debug.Log("ACTIVATE 1"); OnSpeedShoesEntered(); }
            if (Ability == PowerupAbilities.BigBrush) { Debug.Log("ACTIVATE 2"); OnBigBrushEntered(); }
            if (Ability == PowerupAbilities.Bomb) { Debug.Log("ACTIVATE 3"); OnBombEntered(); }
            if (Ability == PowerupAbilities.Freeze) { Debug.Log("ACTIVATE 4"); OnFreezeEntered(); }
            if (Ability == PowerupAbilities.Clone) { Debug.Log("ACTIVATE 5"); OnCloneEntered(); }
            Destroy(gameObject);
        }
    }

    private void OnSpeedShoesEntered()
    {
        if (PlayerController.Instance() != null)
        {
            PlayerController.Instance().GivePlayerPowerUp(PowerupAbilities.SpeedShoes, 8f);
        }
    }

    private void OnBombEntered()
    {
        if (PlayerController.Instance() != null)
        {
            PlayerController.Instance().GivePlayerPowerUp(PowerupAbilities.Bomb, 0f);
        }
    }

    private void OnBigBrushEntered()
    {
        if (PlayerController.Instance() != null)
        {
            PlayerController.Instance().GivePlayerPowerUp(PowerupAbilities.BigBrush, 6f);
        }
    }

    private void OnFreezeEntered()
    {
        EnemyController.Freeze(6.0f);
    }

    private void OnCloneEntered()
    {
        if (PlayerController.Instance() != null)
        {
            PlayerController.Instance().GivePlayerPowerUp(PowerupAbilities.Clone, 6f);
        }
    }
}
