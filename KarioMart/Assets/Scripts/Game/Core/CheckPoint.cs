using UnityEngine;

namespace KarioMart.Core
{
    public class CheckPoint : MonoBehaviour
    {
        private CheckPointTracker checkPointTracker;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(other.name);
                checkPointTracker.CarThroughCheckPoint(this, other.transform);
            }
        }

        public void SetCheckPointTracker(CheckPointTracker checkPointTracker)
        {
            this.checkPointTracker = checkPointTracker;
        }
    }
}