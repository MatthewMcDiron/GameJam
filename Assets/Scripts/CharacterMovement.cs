using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody mRigidbody;

    private Vector3 _moveDirection = Vector3.zero;

    public float Speed = 5.0f;
    private float SpeedMuliplier = 1.0f;

    public float RotationSpeed = 240.0f;

    private Vector3 mExternalMovement = Vector3.zero;

    Camera MainCamera;


    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        MainCamera = Camera.main;
    }

    public void SetMovement(float h, float v)
    {
        // Calculate the forward vector
        //Debug.Log(MainCamera.transform.up);
        //Vector3 camForward_Dir = MainCamera.transform.up;
        //Vector3 move = v * camForward_Dir + h * MainCamera.transform.right;

        //if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        //move = transform.InverseTransformDirection(move);

        // Get Euler angles
        //float turnAmount = Mathf.Atan2(move.x, move.z);

        //transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

        mRigidbody.velocity = new Vector3(h, 0, v).normalized*Speed*SpeedMuliplier;
    }

    public void SetSpeedMultiplier(float _multiplier)
    {
        SpeedMuliplier = _multiplier;
    }
}
