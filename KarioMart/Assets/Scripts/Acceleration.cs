using UnityEngine;
using UnityEngine.InputSystem;

public class Acceleration : MonoBehaviour
{
    [SerializeField] private InputAction accelerateAction;
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private Transform carTransform;
    [SerializeField] private float carTopSpeed = 10f;
    [SerializeField] private AnimationCurve powerCurve;
    private Transform tireTransform;

    
    private void Awake()
    {
        tireTransform = transform;
        accelerateAction.Enable();
    }

    private void OnDestroy()
    {
        accelerateAction.Disable();
    }

    private void Update()
    {
        float accelInput = accelerateAction.ReadValue<float>();
        Debug.Log(accelInput);
        bool rayDidHit = Physics.Raycast(tireTransform.position, -tireTransform.up,
            (tireTransform.position.y / 2) + .1f);
        Debug.DrawRay(tireTransform.position, ((tireTransform.position.y / 2) + .1f) * -tireTransform.up);

        // acceleration/breaking
        if (rayDidHit)
        {
            //world-space direction of the spring force
            Vector3 accelDir = tireTransform.forward;
            //acceleration torque
            if (accelInput > 0.0f)
            {
                //forward speed of the car(in the direction of driving)
                float carSpeed = Vector3.Dot(carTransform.forward, carRigidbody.velocity);
                //normalized car speed
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
                //available torque
                float availableTorque = powerCurve.Evaluate(normalizedSpeed) * accelInput;

                carRigidbody.AddForceAtPosition(accelDir * availableTorque, tireTransform.position);
            }
            if (accelInput < 0.0f)
            {
                //forward speed of the car(in the direction of driving)
                float carSpeed = Vector3.Dot(-carTransform.forward, carRigidbody.velocity);
                //normalized car speed
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
                //available torque
                float availableTorque = powerCurve.Evaluate(normalizedSpeed) * accelInput;

                carRigidbody.AddForceAtPosition(accelDir * availableTorque, tireTransform.position);
            }
        }
    }
}