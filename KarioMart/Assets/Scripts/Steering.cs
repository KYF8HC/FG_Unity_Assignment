using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private float tireGripFactor;
    [SerializeField] private float tireMass = .1f;
    private Transform tireTransform;

    private void Awake()
    {
        tireTransform = transform;
    }

    private void Update()
    {
        bool rayDidHit = Physics.Raycast(tireTransform.position, -tireTransform.up,
            tireTransform.position.y);
        Debug.DrawRay(tireTransform.position, ((tireTransform.position.y / 2) + .1f) * -tireTransform.up);

        // steering force
        if (rayDidHit)
        {
            //world-space direction of the spring force
            Vector3 steeringDir = tireTransform.right;
            //world-space velocity of suspension
            Vector3 tireWorldVel = carRigidbody.GetPointVelocity(tireTransform.position);

            //what it's the tire's velocity in the steering direction?
            //note that steeringDir in a unit vector, so this returns a magnitude of tireWorldVel
            //as projected onto steeringDir
            float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

            //the change in velocity that we're looking for is -steeringVel * gripFactor
            //gripFactor is in range 0-1, 0 means no grip, 1 means full grip
            float desiredVelChange = -steeringVel * tireGripFactor;

            //turn change in velocity into an accelartion(accelartion = change in vel/time)
            //this will produce the acceleration necessary to change the velocity by desiredVelChange in 1 physics step
            float desiredAccel = desiredVelChange / Time.fixedDeltaTime;
            // Force = Mass * acceleration, so multiply by the mass of the tire and apply as force
            carRigidbody.AddForceAtPosition( tireMass * desiredAccel* steeringDir, tireTransform.position);
        }
    }
}
