using System.Drawing.Drawing2D;
using UnityEngine;
using VehiclePhysics.Specialized;

[RequireComponent (typeof(ParticleSystem))]

public class MiniExcavatorExhaust : MonoBehaviour
{
    [SerializeField] VPHydraulicTrackedVehicleControllerInput m_vehicleControllerInput;

    [SerializeField] private float m_alphaAtIdle = 30.0f;
    [SerializeField] private float m_alphaAtPeak = 170.0f;

    private ParticleSystem m_particleSystem;

    void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(m_vehicleControllerInput.ignitionKey == -1)
        {
            m_particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        else
        {
            if(m_particleSystem.isStopped)
            {
                m_particleSystem.Play(true);
            }

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
}
