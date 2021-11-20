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
            other.gameObject.GetComponent<Kickable>().Kick();
        }
    }
}
