using Extensions;
using UnityEngine;

public class KickTrigger : MonoBehaviour
{
    public LayerMask hitMaskKick;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (hitMaskKick.HasLayer(other.gameObject.layer))
        {
            Kickable kickable = other.gameObject.GetComponent<Kickable>();
            if (kickable != null) kickable.Kick();
        }
    }
}
