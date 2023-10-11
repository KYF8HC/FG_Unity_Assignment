using System;
using UnityEngine;
using UnityEngine.AI;

namespace KarioMart.Core
{
    public class AICar : MonoBehaviour
    {
        private NavMeshAgent carAgent;
        [SerializeField] private CheckPointTracker checkPointTracker;
        private float wayPointTolerance = 10f;

        private void Awake()
        {
            carAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MoveTo(GetWaypoint().position);
        }

        private void MoveTo(Vector3 destination)
        {
            carAgent.destination = destination;
        }
        private Transform GetWaypoint()
        {
            return checkPointTracker.GetCheckPointTransform(transform);
        }

        public void SetCheckPointTracker(CheckPointTracker checkPointTracker)
        {
            this.checkPointTracker = checkPointTracker;
        }
    }
}