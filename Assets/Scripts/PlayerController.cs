using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController inst;

    private CharacterMovement mMovement;
    private Animator mAnimator;
    [SerializeField] BoxCollider BrushCollider;
    [SerializeField] GameObject BombItemPrefab;
    private Vector3 InitalBrushColliderSize;

    private Vector2 LastDirection = Vector2.zero;

    PaintPowerUp.PowerupAbilities CurrentPowerup = PaintPowerUp.PowerupAbilities.None;
    float TimeElapsedSincePowerUp;
    float TimeTotalToUsePowerUp;

    private void Awake()
    {
        if (inst == null) { inst = this; }
        else { Destroy(gameObject); }

        mAnimator = GetComponent<Animator>();
        mMovement = GetComponent<CharacterMovement>();
    }

    public static PlayerController Instance() { return inst; }

    private void Start()
    {
        if (BrushCollider != null)
        {
            InitalBrushColliderSize = BrushCollider.size;
        }
    }

    public void GivePlayerPowerUp(PaintPowerUp.PowerupAbilities _ability, float _timeToUsePowerup)
    {
        CurrentPowerup = _ability;
        TimeElapsedSincePowerUp = 0;
        TimeTotalToUsePowerUp = _timeToUsePowerup;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePowerUp();
        if (PaintLevel.Instance() != null)
        {
            if (!PaintLevel.Instance().IsLevelComplete() && !PaintLevel.Instance().IsLevelPaused())
            {
                // Get Input for axis
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                mMovement.SetMovement(h, v);
                if (h != 0 || v != 0)
                {
                    LastDirection = new Vector2(h, v);
                }
            }
            else
            {
                mMovement.SetMovement(0, 0);
            }
        }
    }

    public PaintPowerUp.PowerupAbilities GetCurrentPowerup()
    {
        return CurrentPowerup;
    }

    private void UpdatePowerUp()
    {
        if (TimeElapsedSincePowerUp < TimeTotalToUsePowerUp && CurrentPowerup != PaintPowerUp.PowerupAbilities.None)
        {
            TimeElapsedSincePowerUp += Time.deltaTime;
        }
        else { CurrentPowerup = PaintPowerUp.PowerupAbilities.None; }

        //Speed shoes
        if (CurrentPowerup == PaintPowerUp.PowerupAbilities.SpeedShoes) { mMovement.SetSpeedMultiplier(1.5f); }
        else { mMovement.SetSpeedMultiplier(1.0f); }

        /*
        //Bombs
        if (CurrentPowerup == PaintPowerUp.PowerupAbilities.Bomb && Input.GetKeyDown(KeyCode.F)) 
        {

            Debug.Log(LastDirection);
            CurrentPowerup = PaintPowerUp.PowerupAbilities.None;
            GameObject bomb = Instantiate(BombItemPrefab);
            bomb.transform.position = gameObject.transform.position + new Vector3(LastDirection.x, 0, LastDirection.y).normalized*2;

            BombItem bombScript = bomb.GetComponent<BombItem>();
            bombScript.SetRollingDirection(new Vector3(LastDirection.x, 0, LastDirection.y).normalized);
        } 
        */

        //Big brush
        if (BrushCollider != null)
        {
            if (CurrentPowerup == PaintPowerUp.PowerupAbilities.BigBrush) { BrushCollider.size = InitalBrushColliderSize * 2.0f; }
            else { BrushCollider.size = InitalBrushColliderSize; }
        }

        //
    }
}