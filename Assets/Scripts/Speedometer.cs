using UnityEngine;
using VehiclePhysics.Specialized;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private VPHydraulicTrackedVehicleController m_vehicleController;
    [SerializeField] private Transform m_needle;

    private const float MAX_SPEED = 3.5f;
    private const float MAX_SPEED_ANGLE = -90.0f;

    void Update()
    {
        float speed = m_vehicleController.speed * 3.6f;
        float needAngle = speed / MAX_SPEED * MAX_SPEED_ANGLE;
        m_needle.eulerAngles = new Vector3(0, 0, needAngle);
    }
}
