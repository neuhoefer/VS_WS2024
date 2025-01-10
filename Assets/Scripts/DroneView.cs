using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DroneView : MonoBehaviour
{
    [SerializeField] private Transform m_spot;
    [SerializeField] private Camera m_droneCamera;
    [SerializeField] private MeshCollider m_groundCollider;
    [SerializeField] private NavMeshAgent m_agent;

    private RawImage m_rawImage = null;
    private RectTransform m_rectTransform = null;
    private Vector2 m_droneViewMousePos = new Vector2();

    void Start()
    {
        m_rawImage = GetComponent<RawImage>();
        m_rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rectTransform, Input.mousePosition, null, out m_droneViewMousePos);
            //print("Rect coordinates: " + m_droneViewMousePos.x + " || " + m_droneViewMousePos.y);

            m_droneViewMousePos.x = (m_droneViewMousePos.x / m_rectTransform.rect.width) + m_rectTransform.pivot.x;
            m_droneViewMousePos.y = (m_droneViewMousePos.y / m_rectTransform.rect.height) + m_rectTransform.pivot.y;

            m_droneViewMousePos.x += m_rawImage.uvRect.x;
            m_droneViewMousePos.x *= m_rawImage.uvRect.width;

            if (m_droneViewMousePos.x > 0.0f && m_droneViewMousePos.x < 1.0f && m_droneViewMousePos.y > 0.0f && m_droneViewMousePos.y < 1.0f)
            {
                Ray ray = m_droneCamera.ViewportPointToRay(m_droneViewMousePos);
                m_groundCollider.Raycast(ray, out RaycastHit raycastHit, 20.0f);
                m_spot.position = raycastHit.point;
                m_agent.SetDestination(m_spot.position);
            }
        }
    }
}
