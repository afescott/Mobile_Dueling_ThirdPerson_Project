using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Freelock : MonoBehaviour
{
    

        public CinemachineFreeLook freeLook;
       

        //private InputMotor _stickInput;

        //public InputMotor stickInput
        //{
        //    get
        //    {
        //        if (_stickInput == null && stickSettings != null)
        //            _stickInput = new InputMotor(stickSettings);
        //        return _stickInput;
        //    }
        //}

        //public InputSettings stickSettings;

        private float prevTimeSinceStartup;

        protected void Update()
        {
            var dt = Time.realtimeSinceStartup - prevTimeSinceStartup;

            if (!Application.isPlaying)
                UpdateAxisInput(dt, true);

            prevTimeSinceStartup = Time.realtimeSinceStartup;
        }

        protected void FixedUpdate()
        {
            UpdateAxisInput(Time.fixedDeltaTime);
        }

        public void UpdateAxisInput(float dt, bool manualUpdate = false)
        {


            for (int i = 0; i < Input.touchCount; i++)
            {

                var input = Input.touches[i];

                //stickInput.Tick(InputManager.input.stickRight);

                freeLook.m_XAxis.m_InputAxisValue = input.position.x; // * gain;
                freeLook.m_YAxis.m_InputAxisValue = input.position.y; // * gain;
        }

          

            if (manualUpdate)
            {
                freeLook.m_XAxis.Update(dt);
                freeLook.m_YAxis.Update(dt);

                freeLook.UpdateCameraState(Vector3.up, dt);
            }
        }
    
}
