using Game.RoleElement;
using UnityEngine;

namespace Game.Role
{
    [RequireComponent(typeof(Faller))]
    [RequireComponent(typeof(DirectionalSound))]
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float accelerateThrottle = 30.0f;

        [SerializeField]
        private float rotateThrottle = 5.0f;

        [SerializeField]
        private float continuouseDirecitonVelocityRateLimit = 12.0f;
        public float AccelerationRateLimit
        {
            get { return this.continuouseDirecitonVelocityRateLimit; }
        }

        private Rigidbody rigidbodyCache = null;

        private Vector3 currentDirecitonVelocity = Vector3.zero;
        private Vector3 currentTorqueVelocity = Vector3.zero;

        private Vector3 continuouseDirecitonVelocityRate = Vector3.zero;
        public Vector3 AccelerationRate
        {
            get { return this.continuouseDirecitonVelocityRate; }
        }

        private void Start()
        {
            this.rigidbodyCache = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            this.continuouseDirecitonVelocityRate = Gravity.InertiaVelocity(this.currentDirecitonVelocity, this.continuouseDirecitonVelocityRate, this.continuouseDirecitonVelocityRateLimit);
            this.rigidbodyCache.AddRelativeForce(this.continuouseDirecitonVelocityRate * this.accelerateThrottle, ForceMode.Force);

            if (this.currentTorqueVelocity != Vector3.zero)
            {
                this.rigidbodyCache.AddRelativeTorque(this.currentTorqueVelocity * this.rotateThrottle, ForceMode.Force);
            }

            this.currentDirecitonVelocity = Vector3.zero;
            this.currentTorqueVelocity = Vector3.zero;
        }

        public void Retrieve()
        {
            DirectionalSound sound = this.GetComponent<DirectionalSound>();
            sound.PlayBottom();
        }

        public void AccelerateDirection(Vector3 direction)
        {
            DirectionalSound sound = this.GetComponent<DirectionalSound>();
            sound.Play(-direction);

            this.currentDirecitonVelocity += direction;

        }
        public void AccelerateTorque(Vector3 torque)
        {
            DirectionalSound sound = this.GetComponent<DirectionalSound>();
            sound.Play(-new Vector3(-torque.y, torque.x, torque.z));
            this.currentTorqueVelocity += torque;
        }
    }
}