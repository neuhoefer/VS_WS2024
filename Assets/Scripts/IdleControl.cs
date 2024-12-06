using UnityEngine;
using UnityEngine.UI;
using VehiclePhysics.Specialized;

public class IdleControl : MonoBehaviour
{

    [SerializeField] VPHydraulicTrackedVehicleControllerInput m_vehicleControllerInput;
    [SerializeField] ParticleSystem m_particleSystem;

    [SerializeField] private float m_alphaAtIdle = 30.0f;
    [SerializeField] private float m_alphaAtPeak = 170.0f;

    void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(TaskOnValueChanged);
        SetParticleSystem();
    }

    private void TaskOnValueChanged(float value)
    {
        m_vehicleControllerInput.idleControlInput = value;
        SetParticleSystem();
    }

    private void SetParticleSystem()
    {
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        GradientColorKey[] colorKeys = new GradientColorKey[2];

        float alpha = m_alphaAtIdle + m_vehicleControllerInput.idleControlInput * (m_alphaAtPeak - m_alphaAtIdle);

        alphaKeys[0].alpha = alpha / 255.0f;          // Abhängig von Drehzahl!
        alphaKeys[0].time = 0.0f;
        colorKeys[0].color = Color.black;
        colorKeys[0].time = 0.0f;

        alphaKeys[1].alpha = 0.0f;
        alphaKeys[1].time = 1.0f;
        colorKeys[1].color = Color.gray;
        colorKeys[1].time = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = m_particleSystem.colorOverLifetime;
        colorOverLifetimeModule.color = gradient;
    }
}
