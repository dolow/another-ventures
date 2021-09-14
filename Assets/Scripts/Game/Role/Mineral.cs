using UnityEngine;

namespace Game.Role
{
    public class Mineral : MonoBehaviour
    {
        public delegate void Retrieved(Mineral self);

        public Retrieved OnRetrieved = (Mineral self) => { };

        private void OnTriggerEnter(Collider collision)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                this.OnRetrieved(this);
            }
        }
    }
}