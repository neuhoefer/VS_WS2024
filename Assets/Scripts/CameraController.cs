using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Vector3 m_offset;

    private void FixedUpdate()
    {
        transform.position = m_target.TransformPoint(m_offset);
        Vector3 direction = m_target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
