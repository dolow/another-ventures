using Game.RoleElement;
using UnityEngine;

namespace Game.Role
{
    [RequireComponent(typeof(Fallee))]
    [RequireComponent(typeof(Rigidbody))]
    public class Planet : MonoBehaviour
    {
        [SerializeField]
        private Sun sun = null;

        [SerializeField]
        private float revolutionSpeed = 0.01f;

        [SerializeField]
        private float rotationSpeed = 0.1f;

        private Fallee falleeCache = null;
        private Rigidbody rigidbodyCache = null;

        private void Start()
        {
            this.falleeCache = this.GetComponent<Fallee>();
            this.rigidbodyCache = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            this.transform.RotateAround(this.sun.transform.position, Vector3.up, this.revolutionSpeed);
            Vector3 euler = this.transform.rotation.eulerAngles;
            euler.y += this.rotationSpeed;
            this.transform.rotation = Quaternion.Euler(euler);

            Faller[] fallers = GameObject.FindObjectsOfType<Faller>();
            this.falleeCache.LetFall(fallers, this.rigidbodyCache.mass);
        }
    }
}