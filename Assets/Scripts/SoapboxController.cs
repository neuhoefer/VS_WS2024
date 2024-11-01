using System;
using UnityEngine;

public class SoapboxController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private bool m_isBraking;

    [SerializeField] private float m_motorForce;
    [SerializeField] private float m_breakForce;
    [SerializeField] private float m_maxSteeringAngle;

    [SerializeField] private WheelCollider m_frontLeftCollider;
    [SerializeField] private WheelCollider m_frontRightCollider;
    [SerializeField] private WheelCollider m_rearLeftCollider;
    [SerializeField] private WheelCollider m_rearRightCollider;

    [SerializeField] private Transform m_frontLeftWheel;
    [SerializeField] private Transform m_frontRightWheel;
    [SerializeField] private Transform m_rearLeftWheel;
    [SerializeField] private Transform m_rearRightWheel;

    private void FixedUpdate()
    {
        GetInput();
        ApplyForces();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        m_verticalInput = Input.GetAxis("Vertical");
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_isBraking = Input.GetKey(KeyCode.Space);
    }

    private void ApplyForces()
    {
        m_frontLeftCollider.motorTorque = m_verticalInput * m_motorForce;
        m_frontRightCollider.motorTorque = m_verticalInput * m_motorForce;

        float currentBrakeForce = m_isBraking ? m_breakForce : 0.0f;
        m_frontLeftCollider.brakeTorque = currentBrakeForce;
        m_frontRightCollider.brakeTorque = currentBrakeForce;
        m_rearLeftCollider.brakeTorque = currentBrakeForce;
        m_rearRightCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        m_frontLeftCollider.steerAngle = m_horizontalInput * m_maxSteeringAngle;
        m_frontRightCollider.steerAngle = m_horizontalInput * m_maxSteeringAngle;
    }

    private void UpdateWheels()
    {
        UpdateWheel(m_frontLeftCollider, m_frontLeftWheel);
        UpdateWheel(m_frontRightCollider, m_frontRightWheel);
        UpdateWheel(m_rearLeftCollider, m_rearLeftWheel);
        UpdateWheel(m_rearRightCollider, m_rearRightWheel);
    }

    private void UpdateWheel(WheelCollider collider, Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheel.position = pos;
        wheel.rotation = rot;

        wheel.transform.Rotate(Vector3.forward, 90.0f, Space.Self);
    }
}
