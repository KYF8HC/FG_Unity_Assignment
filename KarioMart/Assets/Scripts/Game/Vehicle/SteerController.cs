using System;
using KarioMart.Core;
using UnityEngine;

namespace KarioMart.Vehicle
{
    public class SteerController : MonoBehaviour
    {
        [SerializeField] private float wheelBase;
        [SerializeField] private float rearTrack;
        [SerializeField] private float turnRadius;

        private Rigidbody carRigidbody;
        private float ackermannAngleLeft;
        private float ackermannAngleRight;
        private Wheel frontLeftWheel;
        private Wheel frontRightWheel;
        private InputHandler inputHandler;
        private PlayerInputActions playerInputActions;

        private void Start()
        {
            carRigidbody = GetComponent<Rigidbody>();
            foreach (Transform child in transform)
            {
                var wheel = child.GetComponent<Wheel>();
                if (!wheel)
                    continue;
                switch (wheel.GetWheelPosition())
                {
                    case Wheel.WheelPosition.FrontRight:
                        frontRightWheel = wheel;
                        continue;
                    case Wheel.WheelPosition.FrontLeft:
                        frontLeftWheel = wheel;
                        continue;
                }
            }

            inputHandler = InGameInitializer.Instance.GetInputHandler();
            inputHandler.OnPause += InputHandler_OnPause;
            playerInputActions = inputHandler.GetPlayerInputActions();
        }

        private void OnDestroy()
        {
            inputHandler.OnPause -= InputHandler_OnPause;
        }

        private void InputHandler_OnPause()
        {
            carRigidbody.isKinematic = !carRigidbody.isKinematic;
        }

        private void Update()
        {
            var steerInput = playerInputActions.Player.Steer.ReadValue<float>();
            var resetFlipInput = playerInputActions.Player.ResetFlip.ReadValue<float>();
            if (resetFlipInput > 0)
            {
                transform.localRotation = Quaternion.identity;
            }

            if (steerInput < 0)
            {
                ackermannAngleLeft =
                    Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
                ackermannAngleRight =
                    Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            }
            else if (steerInput > 0)
            {
                ackermannAngleLeft =
                    Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
                ackermannAngleRight =
                    Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            }
            else
            {
                ackermannAngleLeft = 0;
                ackermannAngleRight = 0;
            }

            frontLeftWheel.SetSteerAngle(ackermannAngleLeft);
            frontRightWheel.SetSteerAngle(ackermannAngleRight);
        }
    }
}