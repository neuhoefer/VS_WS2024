using UnityEngine;

namespace VehiclePhysics.Specialized
{

    public class MiniExcavatorControlInput : VehicleBehaviour
    {
        [Range(0.01f, 2.0f)]
        public float movementRate = 1.0f;

        [Space(5)]
        public KeyCode swingLeft = KeyCode.Keypad0;
        public KeyCode swingRight = KeyCode.KeypadPeriod;
        public KeyCode boomSwingLeft = KeyCode.Keypad1;
        public KeyCode boomSwingRight = KeyCode.Keypad3;
        public KeyCode boomUp = KeyCode.Keypad2;
        public KeyCode boomDown = KeyCode.Keypad5;
        public KeyCode stickUp = KeyCode.Keypad8;
        public KeyCode stickDown = KeyCode.KeypadDivide;
        public KeyCode bucketUp = KeyCode.Keypad9;
        public KeyCode bucketDown = KeyCode.Keypad7;

        MiniExcavatorControl m_miniExcavator;


        public override int GetUpdateOrder()
        {
            // Should update before ExcavatorControl
            return -1;
        }


        public override void OnEnableVehicle()
        {
            m_miniExcavator = vehicle.GetComponentInChildren<MiniExcavatorControl>();
        }


        public override void OnDisableVehicle()
        {
            m_miniExcavator.swingInput = 0.0f;
            m_miniExcavator.boomSwingInput = 0.0f;
            m_miniExcavator.boomInput = 0.0f;
            m_miniExcavator.stickInput = 0.0f;
            m_miniExcavator.bucketInput = 0.0f;
        }


        public override void FixedUpdateVehicle()
        {
            float swing = 0.0f;
            if (Input.GetKey(swingLeft)) swing -= 1.0f;
            if (Input.GetKey(swingRight)) swing += 1.0f;

            float boomSwing = 0.0f;
            if (Input.GetKey(boomSwingLeft)) boomSwing -= 1.0f;
            if (Input.GetKey(boomSwingRight)) boomSwing += 1.0f;

            float boom = 0.0f;
            if (Input.GetKey(boomUp)) boom += 1.0f;
            if (Input.GetKey(boomDown)) boom -= 1.0f;

            float stick = 0.0f;
            if (Input.GetKey(stickUp)) stick += 1.0f;
            if (Input.GetKey(stickDown)) stick -= 1.0f;

            float bucket = 0.0f;
            if (Input.GetKey(bucketUp)) bucket += 1.0f;
            if (Input.GetKey(bucketDown)) bucket -= 1.0f;

            m_miniExcavator.swingInput = swing * movementRate;
            m_miniExcavator.boomSwingInput = boomSwing * movementRate;
            m_miniExcavator.boomInput = boom * movementRate;
            m_miniExcavator.stickInput = stick * movementRate;
            m_miniExcavator.bucketInput = bucket * movementRate;
        }
    }

}
