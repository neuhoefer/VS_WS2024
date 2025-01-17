using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using VehiclePhysics.Specialized;

public class Automation : MonoBehaviour
{
    [SerializeField] private GameObject m_destination;
    [SerializeField] private NavMeshAgent m_agent;
    [SerializeField] private VPHydraulicTrackedVehicleController m_vehicle;

    [SerializeField] private PIDController m_orientationController;
    [SerializeField] private PIDController m_distanceController;
    [SerializeField] private float m_distanceTolerance;

    private TextMeshProUGUI m_label = null;

    void Start()
    {
        m_label = GetComponentInChildren<TextMeshProUGUI>();
        GetComponent<Button>().onClick.AddListener(TaskOnClick);   
    }

    private void TaskOnClick()
    {
        if(m_label.color == Color.white)
        {
            m_agent.transform.position = m_vehicle.transform.position;
            m_agent.SetDestination(m_destination.transform.position);
            m_label.color = Color.yellow;
        }
        else if(m_label.color == Color.yellow)
        {
            m_agent.SetDestination(m_agent.transform.position);
            m_label.color = Color.white;
        }
    }

    public void SetAgentDestination(Vector3 pos)
    {
        m_destination.SetActive(true);
        m_destination.transform.position = pos;

        if(m_label.color == Color.grey)
        {
            m_label.color = Color.white;
        } 
        else if (m_label.color == Color.yellow)
        {
            m_agent.SetDestination(m_destination.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if(m_label.color == Color.yellow)
        {
            var vehiclePosition = m_vehicle.transform.position;
            var targetPosition = m_agent.transform.position;
            targetPosition.y = vehiclePosition.y;
            var forwardDirection = m_vehicle.transform.rotation * Vector3.forward;
            var targetDirection = (targetPosition - vehiclePosition).normalized;

            var currentAngle = Vector3.SignedAngle(forwardDirection, targetDirection, Vector3.up);
            var currentDistance = (targetPosition - vehiclePosition).magnitude;

            float rotationalInput = m_orientationController.UpdateAngle(Time.fixedDeltaTime, currentAngle, 0.0f);
            float translationalInput = m_distanceController.Update(Time.fixedDeltaTime, currentDistance, 0.0f);

            if (currentDistance > m_distanceTolerance)
            {
                m_vehicle.leftTrackInput = -rotationalInput - translationalInput;
                m_vehicle.rightTrackInput = rotationalInput - translationalInput;
            }
        }
    }
}
