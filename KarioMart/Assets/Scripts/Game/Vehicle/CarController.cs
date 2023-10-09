using System.Collections.Generic;
using KarioMart.Core;
using UnityEngine;

namespace KarioMart.Vehicle
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float wheelBase;
        [SerializeField] private float rearTrack;
        [SerializeField] private float turnRadius;

        private Rigidbody carRigidbody;
        private float ackermannAngleLeft;
        private float ackermannAngleRight;
        private List<Wheel> wheels;
        private Wheel frontLeftWheel;
        private Wheel frontRightWheel;
        private InputHandler inputHandler;
        private PlayerInputActions playerInputActions;
        private CheckPointTracker checkPointTracker;
        private Transform lastCheckPointTransform;
        private void Start()
        {
            wheels = new List<Wheel>();
            carRigidbody = GetComponent<Rigidbody>();
            foreach (Transform child in transform)
            {
                var wheel = child.GetComponent<Wheel>();
                if (!wheel)
                    continue;
                wheels.Add(wheel);
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
            checkPointTracker.OnCarPassThroughCheckPoint -= CheckPointTracker_OnCarPassThroughCheckPoint;
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
                transform.position = lastCheckPointTransform.position;
                transform.localRotation = lastCheckPointTransform.localRotation;
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
        
        public void ActivateSpeedBoost()
        {
            foreach (var wheel in wheels)
            {
                wheel.ActivateSpeedBoost();
            }
        }

        public void SetCheckPointTracker(CheckPointTracker checkPointTracker)
        {
            this.checkPointTracker = checkPointTracker;
            this.checkPointTracker.OnCarPassThroughCheckPoint += CheckPointTracker_OnCarPassThroughCheckPoint;
        }

        private void CheckPointTracker_OnCarPassThroughCheckPoint(CheckPoint checkpoint)
        {
            lastCheckPointTransform = checkpoint.transform;
        }
    }
}