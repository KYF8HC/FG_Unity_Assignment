using UnityEngine;

public class Suspension : MonoBehaviour
{
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private float suspensionRestDistance;
    [SerializeField] private float springTravel;
    [SerializeField] private float springStrength;
    [SerializeField] private float springDamper;
    [SerializeField] private float wheelRadius;

    private float minDistance;
    private float maxDistance;
    private float springDistance;
    private Transform tireTransform;

    private void Awake()
    {
        tireTransform = transform;
        minDistance = suspensionRestDistance - springTravel;
        maxDistance = suspensionRestDistance + springTravel;
    }

    private void FixedUpdate()
    {
        
        RaycastHit tireRay;
        bool rayDidHit = Physics.Raycast(tireTransform.position, -tireTransform.up, out tireRay, maxDistance + wheelRadius);
        
        Debug.DrawRay(tireTransform.position, (maxDistance + wheelRadius) * -tireTransform.up);
        
        // Suspension spring force calculation.
        if (rayDidHit)
        {
            float lastLength = springDistance;
            springDistance = tireRay.distance - wheelRadius;
            springDistance = Mathf.Clamp(springDistance, minDistance, maxDistance);
            float vel = (lastLength - springDistance) / Time.fixedDeltaTime;
            Vector3 springDir = tireTransform.up;
            float offset = suspensionRestDistance - springDistance;
            float force = (offset * springStrength) + (vel * springDamper);
            Debug.DrawRay(tireTransform.position, springDir * force, Color.green);
            carRigidbody.AddForceAtPosition(springDir * force, tireRay.point);
        }
    }
}