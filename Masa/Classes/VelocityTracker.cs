using UnityEngine;

namespace Masa.Classes
{
    class VelocityTracker : MonoBehaviour
    {
        private static Vector3 OldPosition;
        private static Vector3 NewPosition;
        private static Vector3 Velocity;

        void Update()
        {
            OldPosition = NewPosition;
            NewPosition = transform.position;
            Velocity = (NewPosition - OldPosition) / Time.deltaTime;
        }

        public Vector3 GetVelocity()
        {
            return Velocity;
        }
    }
}
