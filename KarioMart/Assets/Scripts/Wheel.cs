using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Start()
    {
        tireTransform = transform;
        accelerateAction.Enable();
        brakeAction.Enable();
    }

    private void OnDestroy()
    {
        accelerateAction.Disable();
        brakeAction.Disable();
    }

    private void Update()
    {
        UpdateWheelRotation();
    }

    private void FixedUpdate()
    {
        var tirePosition = tireTransform.position;
        var rayDidHit = Physics.Raycast(tirePosition, -tireTransform.up, out var tireRay, springRestDistance);
        Debug.DrawRay(tirePosition, -tireTransform.up, Color.blue);
        Vector3 suspensionForce = Suspension(rayDidHit, tireRay);
        Vector3 steeringForce = Steering(rayDidHit);
        Vector3 accelForce = Acceleration(rayDidHit);
        carRigidbody.AddForceAtPosition(suspensionForce + accelForce + steeringForce, tirePosition);
        HandleBrake();
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
        var brakeInput = brakeAction.ReadValue<float>();
        var brakingForce = brakeInput * maxBrakingForce;
        carRigidbody.AddForce(-carRigidbody.velocity.normalized * brakingForce);
    }

    private Vector3 Steering(bool rayDidHit)
    {
        if (!rayDidHit)
        {
            return Vector3.zero;
        }
        var tirePosition = tireTransform.position;
        Vector3 steeringDir = tireTransform.right;
        Vector3 tireWorldVel = carRigidbody.GetPointVelocity(tirePosition);
        float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
        float desiredVelChange = -steeringVel * tireGripFactor;
        float desiredAcceleration = desiredVelChange / Time.fixedDeltaTime;
        Debug.DrawRay(tirePosition, tireMass * desiredAcceleration * steeringDir, Color.green);
        return tireMass * desiredAcceleration * steeringDir;
    }

    private Vector3 Acceleration(bool rayDidHit)
    {
        var accelValue = accelerateAction.ReadValue<float>();
        if (!rayDidHit)
        {
            return Vector3.zero;
        }

        if (accelValue == 0.0f)
        {
            return Vector3.zero;
        }

        var carDir = accelValue < 0 ? -carTransform.forward : carTransform.forward;
        var carSpeed = Vector3.Dot(carDir, carRigidbody.velocity);
        var normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
        var availableTorque = powerCurve.Evaluate(normalizedSpeed) * Mathf.Abs(accelValue);
        return availableTorque * speedMultiplier * carDir;
    }

    private Vector3 Suspension(bool rayDidHit, RaycastHit tireRay)
    {
        if (!rayDidHit)
        {
            return Vector3.zero;
        }

        var springDir = tireTransform.up;
        var tirePosition = tireTransform.position;
        var tireWorldVel = carRigidbody.GetPointVelocity(tirePosition);

        var offset = springRestDistance - tireRay.distance;
        var velocity = Vector3.Dot(springDir, tireWorldVel);
        var force = (offset * springStrength) - (velocity * springDamper);
        return (springDir * force);
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