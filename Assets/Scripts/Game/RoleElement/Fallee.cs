using UnityEngine;

namespace Game.RoleElement
{
    public class Fallee : MonoBehaviour
    {
        public void LetFall(Faller[] fallers, float baseSpeed)
        {
            for (int i = 0; i < fallers.Length; i++)
            {
                Faller faller = fallers[i];
                if (Gravity.ShouldAffect(this.transform.position, faller.transform.position, baseSpeed))
                {
                    Vector3 velocity = Gravity.GravityVelocity(this.transform.position, faller.transform.position, baseSpeed);
                    faller.Fall(velocity * baseSpeed * 0.1f);
                }
            }
        }
    }
}