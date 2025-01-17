
using UnityEngine;
using VehiclePhysics;
using VehiclePhysics.Specialized;

public class Loadmeter : MonoBehaviour
{
    [SerializeField] private VPHydraulicTrackedVehicleController m_vehicleController;
    [SerializeField] private Transform m_needle;

    private int[] m_vehicleData;

    private const float MAX_LOAD = 100.0f;
    private const float MAX_LOAD_ANGLE = -90.0f;
    private const float ZERO_LOAD_ANGLE = 90.0f;

    void Start()
    {
        m_vehicleData = m_vehicleController.data.Get(VehiclePhysics.Channel.Vehicle);
    }

    void Update()
    {
        float load = m_vehicleData[VehicleData.EngineLoad] / 10.0f;
        float needleAngle = ZERO_LOAD_ANGLE - load / MAX_LOAD * (ZERO_LOAD_ANGLE - MAX_LOAD_ANGLE);
        m_needle.eulerAngles = new Vector3(0, 0, needleAngle);
    }
}
