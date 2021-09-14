using UnityEngine;

namespace Game.RoleElement
{
    public class Faller : MonoBehaviour
    {
        public void Fall(Vector3 velocity)
        {
            Rigidbody rigidbody = this.GetComponent<Rigidbody>();
            rigidbody.AddForce(velocity, ForceMode.Force);
        }
    }
}