using Extensions;
using UnityEngine;

namespace Buffs
{
    public abstract class Buff : MonoBehaviour
    {
        public LayerMask playerMask;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (playerMask.HasLayer(other.gameObject.layer))
            {

                Pickup();
            }
        }

        protected virtual void Pickup()
        {

            Debug.Log("Buff picked");


        }
    }
}
