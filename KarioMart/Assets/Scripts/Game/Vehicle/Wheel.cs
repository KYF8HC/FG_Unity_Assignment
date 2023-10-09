using KarioMart.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KarioMart.Vehicle
{
    public class Wheel : MonoBehaviour
    {
        public enum WheelPosition
        {
            FrontLeft,
            FrontRight,
            RearLeft,
            RearRight
        }

        [SerializeField] private WheelPosition wheelPosition;
        [SerializeField] private InputAction accelerateAction;
        [SerializeField] private InputAction brakeAction;
        [SerializeField] private Transform carTransform;
        [SerializeField] private Rigidbody carRigidbody;
        [SerializeField] private AnimationCurve powerCurve;
        [SerializeField] private float carTopSpeed;
        [SerializeField] private float springStrength;
        [SerializeField] private float springDamper;
        [SerializeField] private float springRestDistance;
        [SerializeField] private float maxBrakingForce = 2f;
        [SerializeField] private float speedMultiplier = 1f;
        [SerializeField] private float tireMass = 1f;
        [SerializeField] private float tireGripFactor = 1f;

        private Transform tireTransform;
        private float wheelAngle;
        private float steerAngle;
        private float steerTime = 3.92f;
        private Vector3 wheelVelocity;
        private InputHandler inputHandler;
        private PlayerInputActions playerInputActions;
        private float speedBoostTimer = 0f;
        private bool speedBoostActive = false;
        private int speedBoostDuration = 3; //seconds

        public bool gameIsPaused = false;

        private void Start()
        {
            inputHandler = InGameInitializer.Instance.GetInputHandler();
            inputHandler.OnPause += InputHandler_OnPause;
            playerInputActions = inputHandler.GetPlayerInputActions();
            tireTransform = transform;
            accelerateAction.Enable();
            brakeAction.Enable();
        }


        private void OnDestroy()
        {
            inputHandler.OnPause -= InputHandler_OnPause;
            accelerateAction.Disable();
            brakeAction.Disable();
        }

        private void Update()
        {
            if (gameIsPaused)
                return;
            UpdateWheelRotation();
            if (speedBoostActive)
            {
                speedBoostTimer += Time.deltaTime;
                speedMultiplier = 6f;
                Debug.Log("ACTIVATE FULL POWER!!");
            }

            if (speedBoostTimer % 60 >= speedBoostDuration)
            {
                Debug.Log("Power OFF!");
                speedBoostActive = false;
                speedBoostTimer = 0f;
                speedMultiplier = 1f;
            }
        }

        private void FixedUpdate()
        {
            if (gameIsPaused)
                return;
            ApplyForce();
            HandleBrake();
        }

        private void InputHandler_OnPause()
        {
            gameIsPaused = !gameIsPaused;
        }

        private void ApplyForce()
        {
            var tirePosition = tireTransform.position;
            var rayDidHit = Physics.Raycast(tirePosition, -tireTransform.up, out var tireRay, springRestDistance);
            var suspensionForce = Suspension(rayDidHit, tireRay);
            var steeringForce = Steering(rayDidHit);
            var accelForce = Acceleration(rayDidHit);
            carRigidbody.AddForceAtPosition(suspensionForce + accelForce + steeringForce, tirePosition);
        }

        private void UpdateWheelRotation()
        {
            wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);

            var yRotation = wheelAngle;
            var yRotationQuaternion = Quaternion.Euler(Vector3.up * yRotation);
            transform.localRotation = yRotationQuaternion;
        }

        private void HandleBrake()
        {
            var brakeInput = playerInputActions.Player.Brake.ReadValue<float>();
            var brakingForce = brakeInput * maxBrakingForce;
            carRigidbody.AddForce(-carRigidbody.velocity.normalized * brakingForce);
        }

        private Vector3 Steering(bool rayDidHit)
        {
            if (!rayDidHit)
                return Vector3.zero;

            var tirePosition = tireTransform.position;
            var steeringDir = tireTransform.right;
            var tireWorldVel = carRigidbody.GetPointVelocity(tirePosition);
            var steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
            var desiredVelChange = -steeringVel * tireGripFactor;
            var desiredAcceleration = desiredVelChange / Time.fixedDeltaTime;
            Debug.DrawRay(tirePosition, tireMass * desiredAcceleration * steeringDir, Color.green);
            return tireMass * desiredAcceleration * steeringDir;
        }

        private Vector3 Acceleration(bool rayDidHit)
        {
            var accelValue = playerInputActions.Player.Accel.ReadValue<float>();
            if (!rayDidHit)
                return Vector3.zero;

            if (accelValue == 0.0f)
                return Vector3.zero;

            var carDir = accelValue < 0 ? -carTransform.forward : carTransform.forward;
            var carSpeed = Vector3.Dot(carDir, carRigidbody.velocity);
            var normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
            var availableTorque = powerCurve.Evaluate(normalizedSpeed) * Mathf.Abs(accelValue);
            return availableTorque * speedMultiplier * carDir;
        }

        private Vector3 Suspension(bool rayDidHit, RaycastHit tireRay)
        {
            if (!rayDidHit)
                return Vector3.zero;

            var springDir = tireTransform.up;
            var tirePosition = tireTransform.position;
            var tireWorldVel = carRigidbody.GetPointVelocity(tirePosition);

            var offset = springRestDistance - tireRay.distance;
            var velocity = Vector3.Dot(springDir, tireWorldVel);
            var force = (offset * springStrength) - (velocity * springDamper);
            return (springDir * force);
        }

        public void ActivateSpeedBoost()
        {
            speedBoostActive = true;
        }

        public void SetSteerAngle(float angle)
        {
            steerAngle = angle;
        }

        public WheelPosition GetWheelPosition()
        {
            return wheelPosition;
        }
    }
}