using UnityEngine;

namespace KarioMart.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 offset = new Vector3(0f, 8f, -16.5f);
        private Vector3 rotation = new Vector3(25f, 0f, 0f);

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            var desiredPosition = target.TransformPoint(offset);
            transform.position = desiredPosition;
            transform.rotation = Quaternion.Euler(rotation);

            transform.LookAt(target);
        }

        public void SetFollowTarget(Transform target)
        {
            this.target = target;
        }
    }
}