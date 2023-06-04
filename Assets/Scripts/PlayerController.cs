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
    private Vector3 InitalBrushColliderSize;

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
            }
            else
            {
                mMovement.SetMovement(0, 0);
            }
        }

        // Interact with the item


        // Execute action with item
        // if (mCurrentItem != null && Input.GetMouseButtonDown(0))
        {
            // Dont execute click if mouse pointer is over uGUI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // TODO: Logic which action to execute has to come from the particular item
                //_animator.SetTrigger("attack_1");
            }
        }
    }

    private void UpdatePowerUp()
    {
        if (TimeElapsedSincePowerUp < TimeTotalToUsePowerUp && CurrentPowerup != PaintPowerUp.PowerupAbilities.None)
        {
            TimeElapsedSincePowerUp += Time.deltaTime;
        }
        else if (CurrentPowerup != PaintPowerUp.PowerupAbilities.Bomb) { CurrentPowerup = PaintPowerUp.PowerupAbilities.None; }

        //Speed shoes
        if (CurrentPowerup == PaintPowerUp.PowerupAbilities.SpeedShoes) { mMovement.SetSpeedMultiplier(1.5f); }
        else { mMovement.SetSpeedMultiplier(1.0f); }

        //Bombs
        if (CurrentPowerup == PaintPowerUp.PowerupAbilities.Bomb && Input.GetKeyDown(KeyCode.F)) 
        { CurrentPowerup = PaintPowerUp.PowerupAbilities.None; } //Use bomb 

        //Big brush
        if (BrushCollider != null)
        {
            if (CurrentPowerup == PaintPowerUp.PowerupAbilities.BigBrush) { BrushCollider.size = InitalBrushColliderSize * 2.0f; }
            else { BrushCollider.size = InitalBrushColliderSize; }
        }


    }
}